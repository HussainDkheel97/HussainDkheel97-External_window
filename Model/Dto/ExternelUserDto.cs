namespace External_Party.Models.Dto
{
    public class ExternelUserDto
    {
        public string User_Name { get; set; }

        public string Password { get; set; }

        public bool state { get; set; }

        // الربط مع الجهة الخارجية
        public int External_Party_ID { get; set; }
    }
}
