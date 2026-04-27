<template>
  <div class="min-h-screen bg-gray-100 flex items-center justify-center p-4" dir="rtl">
    <div class="max-w-md w-full bg-white rounded-xl shadow-lg overflow-hidden">
      
       <div>
          <img
            class="mx-auto h-36 w-auto object-cover"
            src="../../assets/img/logo-aca.png"
            alt="logo"
          />
          <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
            منظومة التراسل الخارجي
          </h2>
        </div>
      <form @submit.prevent="handleLogin" class="p-8 space-y-5">
        
        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1">اسم المستخدم</label>
          <div class="relative">
            <input 
              v-model="loginData.username"
              type="text" 
              required
              class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-green-400 focus:border-green-500 outline-none transition-all text-sm"
              placeholder="أدخل اسم المستخدم"
            />
          </div>
        </div>

        <div>
          <div class="flex justify-between items-center mb-1">
            <label class="block text-sm font-semibold text-gray-700">كلمة المرور</label>
            <router-link 
              to="/forgot-password" 
              class="text-xs text-green-600 hover:underline font-medium"
            >
              نسيت كلمة المرور؟
            </router-link>
          </div>
          <input 
            v-model="loginData.password"
            type="password" 
            required
            class="w-full px-4 py-3 rounded-lg border border-gray-300 focus:ring-2 focus:ring-green-400 focus:border-green-500 outline-none transition-all text-sm"
            placeholder="••••••••"
          />
        </div>

   
        <button 
          type="submit" 
          :disabled="loading"
          class="w-full bg-green-600 hover:bg-green-700 text-white font-bold py-3 rounded-lg shadow-md transition-all flex items-center justify-center space-x-2"
        >
          <span v-if="loading" class="animate-spin ml-2">⏳</span>
          <span>{{ loading ? 'جاري الدخول...' : 'دخول' }}</span>
        </button>

      </form>

      <div class="bg-gray-50 px-8 py-4 text-center border-t border-gray-100">
        <p class="text-xs text-gray-500">
          بإستخدامك للمنظومة أنت توافق على سياسة الخصوصية
        </p>
      </div>

    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      loginData: {
        username: '',
        password: '',
        rememberMe: false
      }
    };
  },
  methods: {
    async handleLogin() {
      this.loading = true;
      try {
        // هنا تضع كود الـ API الخاص بك
        console.log("بيانات تسجيل الدخول:", this.loginData);
        
        // مثال لمحاكاة الطلب
        setTimeout(() => {
          this.loading = false;
          // this.$router.push('/dashboard');
        }, 1500);
        
      } catch (error) {
        this.loading = false;
        alert("خطأ في تسجيل الدخول، يرجى التأكد من البيانات");
      }
    }
  }
};
</script>

<style scoped>
/* يمكنك إضافة تنسيقات مخصصة هنا إذا لزم الأمر */
</style>