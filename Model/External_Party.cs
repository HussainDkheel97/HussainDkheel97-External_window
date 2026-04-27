using System.ComponentModel.DataAnnotations;

namespace MMSystem.Model
{
    public class External_Party
    {
        [Key]
        public int External_Party_ID { get; set; }

        public string? External_Party_Name { get; set; }

        public bool state { get; set; }


        // العلاقة بين الجهة و البريد الخاص بها
        public virtual ICollection<Mail_Of_External> Mails { get; set; } = new List<Mail_Of_External>();

        // العلاقة : الجهة الواحدة لها عدة مستخدمين
        public virtual ICollection<External_Party_Users> Users { get; set; } = new List<External_Party_Users>();
    }
}