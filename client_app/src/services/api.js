import axios from "axios";
import router from "../router";

/**
 * الرابط الأساسي للسيرفر (Back-End).
 * تم ضبطه على المنفذ 82 بناءً على إعدادات النشر الخاصة بك.
 */
//const API_BASE = 'http://mail.aca.ly:82';
/**
 * تم ضبطه على إعدادات اتطوير الخاصة بك.
 */
 const API_BASE = "http://localhost:58316";

// نسخة Axios للطلبات العامة التي تحتاج توكن وتحتوي على Interceptors
const api = axios.create({
  baseURL: API_BASE,
  withCredentials: false, // ضروري لإرسال الكوكيز وطلبات الـ CORS الآمنة
  headers: { "Content-Type": "application/json" },
});

// نسخة Axios بسيطة لعملية الـ Refresh فقط لتجنب الدخول في حلقة مفرغة
const plainAxios = axios.create({
  baseURL: API_BASE,
  withCredentials: false,
});

let isRefreshing = false;
let subscribers = [];

// وظائف مساعدة لإدارة الطوابير أثناء تجديد التوكن
function subscribeTokenRefresh(cb) {
  subscribers.push(cb);
}

function onRefreshed(token) {
  subscribers.forEach((cb) => cb(token));
  subscribers = [];
}

// وظائف إدارة التوكن في localStorage
export function getAccessToken() {
  return localStorage.getItem("access_token");
}

export function setAccessToken(atoken, rtoken) {
  if (atoken && rtoken) {
    localStorage.setItem("access_token", atoken);
    localStorage.setItem("refresh_token", rtoken);
  } else {
    localStorage.removeItem("access_token");
    localStorage.removeItem("refresh_token");
  }
}

/**
 * وظيفة طلب تجديد التوكن من السيرفر
 */
export async function refreshAccessToken() {
  const refreshToken = localStorage.getItem("refresh_token");
  if (!refreshToken) throw new Error("No refresh token available");

  // يرسل الطلب إلى AuthController/Refresh في الباك-إند (منفذ 96)
  return plainAxios.post("/api/Auth/Refresh", { refreshToken });
}

/**
 * Request Interceptor:
 * يضيف التوكن تلقائياً في الـ Header قبل خروج أي طلب من المتصفح
 */
api.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

/**
 * Response Interceptor:
 * يراقب الردود، وإذا وجد خطأ 401 (انتهت الصلاحية) يقوم بالتجديد تلقائياً
 */
api.interceptors.response.use(
  (res) => res,
  async (error) => {
    const originalReq = error.config;

    // إذا كان الخطأ 401 (غير مصرح) ولم يتم محاولة التجديد مسبقاً لهذا الطلب
    if (
      error.response &&
      error.response.status === 401 &&
      !originalReq._retry
    ) {
      originalReq._retry = true;

      // إذا كانت هناك عملية تجديد قيد التنفيذ حالياً، انتظرها
      if (isRefreshing) {
        return new Promise((resolve) => {
          subscribeTokenRefresh((newToken) => {
            originalReq.headers.Authorization = `Bearer ${newToken}`;
            resolve(api(originalReq));
          });
        });
      }

      isRefreshing = true;
      try {
        // محاولة الحصول على توكن جديد من Redis عبر الباك-إند
        const res = await refreshAccessToken();

        // دعم المسميات بـ CamelCase أو PascalCase حسب رد السيرفر
        const newAccess = res.data.accessToken || res.data.AccessToken;
        const newRefresh = res.data.refreshToken || res.data.RefreshToken;

        setAccessToken(newAccess, newRefresh);
        onRefreshed(newAccess);

        // إعادة تنفيذ الطلب الأصلي بالتوكن الجديد
        originalReq.headers.Authorization = `Bearer ${newAccess}`;
        return api(originalReq);
      } catch (e) {
        // إذا فشل التجديد (مثلاً الـ Refresh Token انتهى)، سجل الخروج
        subscribers = [];
        setAccessToken(null, null);
        if (router.currentRoute.path !== "/") router.push("/");
        return Promise.reject(e);
      } finally {
        isRefreshing = false;
      }
    }
    return Promise.reject(error);
  }
);

export default api;
