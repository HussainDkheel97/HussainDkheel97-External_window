using External_Party.Models.Dto;
using External_Party.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using External_Party.Models.Dto;

namespace External_Party.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase // الوراثة من ControllerBase أفضل للـ APIs
    {
        private readonly IAdmin _adminService;

        public AdminController(IAdmin adminService)
        {
            _adminService = adminService;
        }

        // 1. إضافة مستخدم خارجي جديد
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] ExternelUserDto userDto)
        {
            try
            {
                var result = await _adminService.AddUser(userDto);
                return Ok(new { success = true, message = "تم إضافة المستخدم بنجاح." });
            }
            catch (Exception ex)
            {
                // إرجاع رسالة الخطأ التي حددناها في الـ Service (مثل: الاسم موجود مسبقاً)
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // 2. تحديث بيانات مستخدم
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] ExternelUserDto userDto)
        {
            try
            {
                var result = await _adminService.UdateUser(id, userDto);
                return Ok(new { success = true, message = "تم تحديث البيانات بنجاح." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // 3. حذف مستخدم (تغيير الحالة إلى غير نشط)
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _adminService.DeleteUser(id);
                return Ok(new { success = true, message = "تم إيقاف حساب المستخدم (حذف منطقي)." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}