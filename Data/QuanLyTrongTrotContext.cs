using backend_qltt2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace backend_qltt2.Data
{
    public class QuanLyTrongTrotContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public QuanLyTrongTrotContext(DbContextOptions<QuanLyTrongTrotContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Huyen> Huyens { get; set; }
        public DbSet<Xa> Xas { get; set; }
        public DbSet<GiongCayTrong> GiongCayTrongs { get; set; }
        public DbSet<GiongCay_LuuHanh> GiongCay_LuuHanhs { get; set; }
        public DbSet<SXTT> SXTTs { get; set; }
        public DbSet<CoSoSanXuat> CoSoSanXuats { get; set; }
        public DbSet<CoSoBuonBan> CoSoBuonBans { get; set; }
        public DbSet<ThuocBVTV> ThuocBVTvs { get; set; }
        public DbSet<PhanBon> PhanBons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập khóa chính của bảng trung gian
            modelBuilder.Entity<GiongCay_LuuHanh>()
                .HasKey(gl => new { gl.ID_GiongCay, gl.ID_Xa });

            // Thiết lập mối quan hệ giữa GiongCay_LuuHanh và GiongCayTrong
            modelBuilder.Entity<GiongCay_LuuHanh>()
                .HasOne(gl => gl.GiongCayTrong)
                .WithMany(g => g.Xas) // Navigation property trong GiongCayTrong
                .HasForeignKey(gl => gl.ID_GiongCay);

            // Thiết lập mối quan hệ giữa GiongCay_LuuHanh và Xa
            modelBuilder.Entity<GiongCay_LuuHanh>()
                .HasOne(gl => gl.Xa)
                .WithMany(x => x.GiongCayTrongs) // Navigation property trong Xa
                .HasForeignKey(gl => gl.ID_Xa);

            base.OnModelCreating(modelBuilder);
        }

        // Sử dụng IConfiguration để lấy chuỗi kết nối từ appsettings.json
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
