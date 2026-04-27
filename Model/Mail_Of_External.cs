using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // نحتاج هذا للـ ForeignKey

namespace MMSystem.Model
{
    public class Mail_Of_External
    {
        [Key]
        public int ExMailID { get; set; }

        public string? Mail_Summary { get; set; }

        public DateTime Date_Of_Mail { get; set; }

        public int clasification { get; set; }

        public bool state { get; set; }

        // إعداد المفتاح الأجنبي
        public int External_Party_ID { get; set; }

        [ForeignKey("External_Party_ID")]
        public virtual External_Party ExternalParty { get; set; }
    }
}