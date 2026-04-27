using AutoMapper;
using External_Party.Models.Dto;
using Microsoft.EntityFrameworkCore;
using External_Party.Models;
using MMSystem.Model;

namespace External_Party.Services.Admin
{
    public class AdminMoc : IAdmin
    {
        public AdminMoc(AppDbCon data, IMapper mapper)
        {
            _data = data;

            _mapper = mapper;

        }

        private AppDbCon _data { get; }
        private IMapper _mapper { get; }
        public async Task<bool> AddUser(ExternelUserDto userDto)
        {
            // 1. التحقق من عدم تكرار اسم المستخدم (Username)
            var exists = await _data.External_Party_Users
                .FirstOrDefaultAsync(u => u.User_Name == userDto.User_Name);

            if (exists != null)
                throw new Exception("اسم المستخدم موجود مسبقاً، اختر اسماً آخر.");

            // 2. تحويل الـ DTO إلى Entity
            var user = _mapper.Map<MMSystem.Model.External_Party_Users>(userDto);

            // 3. الإضافة والحفظ
            await _data.External_Party_Users.AddAsync(user);
            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            // 1. البحث عن المستخدم في قاعدة البيانات
            var user = await _data.External_Party_Users
                .FirstOrDefaultAsync(u => u.User_ID == UserId);

            // 2. التحقق من وجوده
            if (user == null)
                throw new Exception("المستخدم غير موجود بالفعل.");

            // 3. بدلاً من المسح، نغير الحالة فقط
            user.state = false;

            // 4. حفظ التعديل
            // ملاحظة: الـ Entity Framework يراقب الكائن 'user'، 
            // بمجرد تغيير الخاصية وعمل save، سيقوم بإرسال أمر UPDATE لقاعدة البيانات.
            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UdateUser(int UserId, ExternelUserDto userDto)
        {
            // 1. البحث عن المستخدم الحالي في قاعدة البيانات باستخدام الـ ID
            var user = await _data.External_Party_Users
                .FirstOrDefaultAsync(u => u.User_ID == UserId);

            if (user == null)
            {
                throw new Exception("المستخدم غير موجود.");
            }

            // 2. التحقق إذا كان الاسم الجديد مأخوذ من قبل مستخدم آخر (لتجنب تكرار الأسماء)
            if (user.User_Name != userDto.User_Name)
            {
                var nameExists = await _data.External_Party_Users
                    .FirstOrDefaultAsync(u => u.User_Name == userDto.User_Name && u.User_ID != UserId);

                if (nameExists != null)
                    throw new Exception("اسم المستخدم الجديد مشغول بالفعل، اختر اسماً آخر.");
            }

            // 3. تحديث الحقول يدوياً أو باستخدام Mapper
            user.User_Name = userDto.User_Name; // تحديث الاسم
            user.Password = userDto.Password;   // تحديث كلمة المرور
            user.state = userDto.state;         // تحديث الحالة (نشط/غير نشط)
            user.External_Party_ID = userDto.External_Party_ID; // تحديث تبعية الجهة

            // 4. حفظ التغييرات
            // بما أن الكائن 'user' تم جلبة عن طريق الـ Context، فإن EF يراقب التغييرات تلقائياً
            await _data.SaveChangesAsync();

            return true;
        }
    }
}
