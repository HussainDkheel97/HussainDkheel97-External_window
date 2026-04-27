<template>
  <div class="min-h-screen bg-gray-50 flex font-sans" dir="rtl">
    <div class="flex-1 w-0 overflow-y-auto pb-10">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        
        <header class="py-8 flex justify-between items-center border-b border-gray-200 mb-8">
          <div>
            <h3 class="text-2xl font-extrabold text-gray-900 tracking-tight">معلومات البريد</h3>
         
          </div>
          <div class="flex space-x-reverse space-x-3">
             </div>
        </header>

        <main class="grid grid-cols-1 lg:grid-cols-12 gap-8">
          
          <section class="lg:col-span-8 space-y-6">
            <div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-6 transition-all hover:shadow-md">
              <div class="grid grid-cols-1 sm:grid-cols-6 gap-6">
                

                    <!-- v-model="summary" -->
                <div class="sm:col-span-6 group">
                  <label for="summary" class="block text-sm font-bold text-gray-700 group-focus-within:text-green-600 transition-colors">الملخص</label>
                  <textarea
                    tabindex="1"
                
                    id="summary"
                    rows="4"
                    class="mt-2 block w-full text-sm rounded-xl border-gray-200 bg-gray-50 p-4 outline-none focus:ring-4 focus:ring-green-100 focus:border-green-500 focus:bg-white transition-all resize-none shadow-sm"
                    placeholder="اكتب ملخص البريد هنا..."
                  ></textarea>
                </div>


                      <!-- v-model="entity_reference_number" -->
                <div class="sm:col-span-3 group">
                  <label for="entity_reference_number" class="block text-sm font-bold text-gray-700 group-focus-within:text-green-600 transition-colors">رقم إشارة الجهة</label>
                  <input
                   
                    type="number"
                    id="entity_reference_number"
                    class="mt-2 block w-full h-12 rounded-xl border-gray-200 bg-gray-50 px-4 outline-none focus:ring-4 focus:ring-green-100 focus:border-green-500 focus:bg-white transition-all shadow-sm"
                  />
                </div>

                <div class="sm:col-span-3 group">
                  <label for="date" class="block text-sm font-bold text-gray-700 group-focus-within:text-green-600 transition-colors">التاريخ</label>
                  <input
                    v-model="releaseDate"
                    type="date"
                    id="date"
                    class="mt-2 block w-full h-12 rounded-xl border-gray-200 bg-gray-50 px-4 outline-none focus:ring-4 focus:ring-green-100 focus:border-green-500 focus:bg-white transition-all shadow-sm cursor-pointer"
                  />
                </div>

<!-- v-model="classification" -->
                <div class="sm:col-span-6 group">
                  <label for="classification" class="block text-sm font-bold text-gray-700 group-focus-within:text-green-600 transition-colors">التصنيف</label>
                  <select
                    
                    id="classification"
                    class="mt-2 block w-full h-12 rounded-xl border-gray-200 bg-gray-50 px-4 outline-none focus:ring-4 focus:ring-green-100 focus:border-green-500 focus:bg-white transition-all shadow-sm appearance-none cursor-pointer"
                  >
                    <option value="0">إختر التصنيف</option>
                    <!-- <option v-for="c in classifications" :key="c.id" :value="c.id">{{ c.name }}</option> -->
                  </select>
                </div>
              </div>
            </div>

 <!-- v-if="summary && classification" @click="clear_page" -->
            <div class="flex items-center justify-end gap-4 bg-white p-4 rounded-2xl border border-gray-100 shadow-sm">
              <button class="px-6 py-2.5 rounded-xl border border-gray-200 text-gray-500 font-bold hover:bg-gray-50 transition-all">جديد</button>
              
                <!-- v-if="updataButton && roles.includes('8')" -->
              <button 
              
                @click="updateMail"
                class="px-8 py-2.5 bg-blue-600 text-white font-bold rounded-xl shadow-lg shadow-blue-100 hover:bg-blue-700 hover:-translate-y-0.5 transition-all active:scale-95"
              >
                تعديل البيانات
              </button>

   <!-- v-if="sendButton"
                @click="sendMail" -->
              <button 
             
                class="px-10 py-2.5 bg-green-600 text-white font-bold rounded-xl shadow-lg shadow-green-100 hover:bg-green-700 hover:-translate-y-0.5 transition-all active:scale-95 flex items-center gap-2"
              >
                <span>إرسال البريد</span>
                <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 rotate-180" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 19l9 2-9-18-9 18 9-2zm0 0v-8" />
                </svg>
              </button>
            </div>
          </section>

          <section class="lg:col-span-4 space-y-6">
            <div class="bg-white rounded-2xl shadow-sm border border-gray-100 p-6 overflow-hidden flex flex-col h-full min-h-[500px]">
              <div class="flex items-center justify-between mb-6">
                <h3 class="text-sm font-bold text-gray-800">المستندات المرفقة</h3>
                <span class="bg-green-100 text-green-700 text-xs px-2 py-1 rounded-full font-bold">{{ total_of_doc }} ملف</span>
              </div>

              <div class="mb-6">
                <label for="fileInput" class="group flex flex-col items-center justify-center w-full h-32 border-2 border-dashed border-gray-200 rounded-2xl bg-gray-50 cursor-pointer hover:bg-green-50 hover:border-green-300 transition-all">
                  <div class="flex flex-col items-center justify-center pt-5 pb-6">
                    <svg class="w-8 h-8 mb-3 text-gray-400 group-hover:text-green-500 transition-colors" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M7 16a4 4 0 01-.88-7.903A5 5 0 1115.9 6L16 6a5 5 0 011 9.9M15 13l-3-3m0 0l-3 3m3-3v12" /></svg>
                    <p class="text-xs text-gray-500">انقر لاختيار المستندات</p>
                  </div>
                  <!-- @change="try_handleFilesChange" -->
                  <input type="file" id="fileInput" accept=".jpg" multiple  class="hidden" />
                </label>
                
                <!-- v-if="try_selectedFiles.length && !isUploading" @click="try_upload" -->
                <button  class="w-full mt-3 bg-blue-600 text-white py-2 rounded-xl font-bold shadow-md hover:bg-blue-700 transition-all">رفع الصور المختارة</button>
              </div>

<!-- v-if="image_of_doc" -->
              <div  class="relative flex-1 bg-gray-900 rounded-xl overflow-hidden group shadow-inner">
                <!-- :src="image_of_doc" -->
                <img  class="w-full h-full object-contain opacity-90 group-hover:opacity-100 transition-opacity" />
                
                <div class="absolute bottom-4 left-0 right-0 flex justify-center gap-2 opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                  <!-- @click="show_current_image_for_bigger_screen" -->
                  <button class="p-2 bg-white/20 backdrop-blur-md rounded-lg text-white hover:bg-white/40"><svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0zM10 7v3m0 0v3m0-3h3m-3 0H7" stroke-width="2"/></svg></button>
                <!-- v-if="roles.includes('6')" @click="prepare_delete_document" -->
                  <button  class="p-2 bg-red-500/80 backdrop-blur-md rounded-lg text-white hover:bg-red-600"><svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-4v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" stroke-width="2"/></svg></button>
                </div>
              </div>

               <!-- v-if="image_of_doc" -->

              <div class="flex items-center justify-between mt-4 bg-gray-50 p-2 rounded-xl">
               <!-- @click="GetAllDocN('prev')" :disabled="doc_number <= 1" -->
                <button  class="p-2 disabled:opacity-30 hover:bg-white rounded-lg transition-all text-gray-600">
                  <svg class="h-5 w-5 rotate-180" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path d="M9 5l7 7-7 7" stroke-width="2"/></svg>
                </button>
                <!-- <span class="text-sm font-bold text-gray-600">{{ doc_number }} من {{ total_of_doc }}</span> -->

 <!-- @click="GetAllDocN('next')" :disabled="doc_number >= total_of_doc" -->
                <button class="p-2 disabled:opacity-30 hover:bg-white rounded-lg transition-all text-gray-600">
                  <svg class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path d="M9 5l7 7-7 7" stroke-width="2"/></svg>
                </button>
              </div>
            </div>
          </section>
        </main>
      </div>
    </div>
  </div>
</template>