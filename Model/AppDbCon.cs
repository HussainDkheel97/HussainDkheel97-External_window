using Microsoft.EntityFrameworkCore;

namespace MMSystem.Model
{
    public class AppDbCon : DbContext
    {
        // هذا السطر هو مفتاح الحل
        public AppDbCon(DbContextOptions<AppDbCon> options) : base(options)
        {
        }

        // جداولك هنا...
        public DbSet<External_Party> External_Parties { get; set; }
        public DbSet<Mail_Of_External> Mail_Of_Externals { get; set; }

        public DbSet<External_Party_Users> External_Party_Users { get; set; }
    }
}