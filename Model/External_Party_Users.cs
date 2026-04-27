using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMSystem.Model
{
    public class External_Party_Users
    {
        [Key] // تحديد المفتاح الأساسي للمستخدم
        public int User_ID { get; set; }

        public string User_Name { get; set; }

        public string Password { get; set; }

        public bool state { get; set; }

        // الربط مع الجهة الخارجية
        public int External_Party_ID { get; set; }

        [ForeignKey("External_Party_ID")]
        public virtual External_Party ExternalParty { get; set; }
    }
}