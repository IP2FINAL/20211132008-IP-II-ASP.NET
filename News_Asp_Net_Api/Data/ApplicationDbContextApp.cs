using News_Asp_Net_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace News_Asp_Net_Api.Data
{
    public class ApplicationDbContextApp : DbContext
    { 
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<kategoriler> kategoriler { get; set; }
        public virtual DbSet<kategori_ara> kategori_ara { get; set; }
        public virtual DbSet<kullanicilar> Kullanicilar { get; set; }

        public ApplicationDbContextApp(DbContextOptions<ApplicationDbContextApp> options) : base(options) { }
    }
}
