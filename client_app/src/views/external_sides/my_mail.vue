<template>
  <div class="p-6 bg-gray-50 min-h-screen" dir="rtl">
    
    <div class="mb-6 flex flex-wrap items-center justify-between gap-4 bg-white p-4 rounded-lg shadow-sm">
      <h2 class="text-xl font-bold text-gray-800 border-r-4 border-green-500 pr-3">قائمة البريد المضاف</h2>
      
      <div class="flex items-center gap-4">
        <label class="text-sm font-semibold text-gray-600">فلترة بالتاريخ:</label>
        <input 
          v-model="filterDate" 
          type="date" 
          class="border border-gray-300 rounded-md p-2 text-sm focus:border-green-500 outline-none"
        />
        <button @click="resetFilter" class="text-xs text-red-500 hover:underline">إعادة تعيين</button>
      </div>
    </div>

    <div class="bg-white rounded-lg shadow overflow-hidden">
      <table class="w-full text-right border-collapse">
        <thead>
          <tr class="bg-gray-100 border-b">
            <th class="p-4 text-sm font-bold text-gray-700"> الرقم الإشاري</th>
            <th class="p-4 text-sm font-bold text-gray-700">ملخص الموضوع</th>
            <th class="p-4 text-sm font-bold text-gray-700">التاريخ</th>
           
            <th class="p-4 text-sm font-bold text-gray-700">الإجراءات</th>
          </tr>
        </thead>
        <tbody>
          <tr 
            v-for="mail in paginatedMails" 
            :key="mail.id" 
            class="border-b hover:bg-green-50 transition-colors"
          >
            <td class="p-4 text-sm text-gray-600">{{ mail.reference_number }}</td>
            <td class="p-4 text-sm text-gray-800 font-medium">{{ truncateText(mail.summary) }}</td>
            <td class="p-4 text-sm text-gray-600">{{ mail.date }}</td>
       
            <td class="p-4">
              <button 
                @click="goToEdit(mail.id)"
                class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-1 rounded text-xs transition-all"
              >
                تفاصيل الرسالة
              </button>
            </td>
          </tr>
          <tr v-if="filteredMails.length === 0">
            <td colspan="5" class="p-10 text-center text-gray-400">لا توجد رسائل بريد مطابقة للبحث</td>
          </tr>
        </tbody>
      </table>

      <div class="p-4 flex justify-between items-center bg-gray-50">
        <span class="text-sm text-gray-600">
          عرض {{ displayFrom }} إلى {{ displayTo }} من أصل {{ filteredMails.length }}
        </span>
        
        <div class="flex gap-2">
          <button 
            @click="currentPage--" 
            :disabled="currentPage === 1"
            class="px-3 py-1 border rounded bg-white hover:bg-gray-100 disabled:opacity-50"
          >
            السابق
          </button>
          
          <button 
            @click="currentPage++" 
            :disabled="currentPage >= totalPages"
            class="px-3 py-1 border rounded bg-white hover:bg-gray-100 disabled:opacity-50"
          >
            التالي
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      filterDate: '',
      currentPage: 1,
      pageSize: 10,
      // بيانات تجريبية (سيتم جلبها من الـ API لاحقاً)
      mails: [
        { id: 1, reference_number: '101/2026', summary: 'بخصوص صيانة أجهزة الحاسوب في القسم المالي', date: '2026-04-10', classificationName: 'عاجل' },
        { id: 2, reference_number: '102/2026', summary: 'طلب إجازة سنوية للموظفين', date: '2026-04-12', classificationName: 'عادي' },
        // ... أضف المزيد من البيانات هنا
      ]
    };
  },
  computed: {
    // فلترة البيانات بناءً على التاريخ
    filteredMails() {
      if (!this.filterDate) return this.mails;
      return this.mails.filter(m => m.date === this.filterDate);
    },
    // حساب إجمالي الصفحات
    totalPages() {
      return Math.ceil(this.filteredMails.length / this.pageSize);
    },
    // اقتطاع البيانات حسب الصفحة الحالية (Pagination Logic)
    paginatedMails() {
      const start = (this.currentPage - 1) * this.pageSize;
      const end = start + this.pageSize;
      return this.filteredMails.slice(start, end);
    },
    displayFrom() { return (this.currentPage - 1) * this.pageSize + 1; },
    displayTo() { 
      const to = this.currentPage * this.pageSize;
      return to > this.filteredMails.length ? this.filteredMails.length : to;
    }
  },
  methods: {
    truncateText(text) {
      return text.length > 50 ? text.substring(0, 50) + '...' : text;
    },
    resetFilter() {
      this.filterDate = '';
      this.currentPage = 1;
    },
    goToEdit(id) {
      // توجيه المستخدم لصفحة التعديل مع إرسال المعرف (ID)
      console.log("الانتقال لتعديل البريد رقم:", id);
      // this.$router.push({ name: 'EditMail', params: { id: id } });
    }
  },
  watch: {
    // إذا تغير الفلتر، نرجع للصفحة الأولى تلقائياً
    filterDate() {
      this.currentPage = 1;
    }
  }
};
</script>