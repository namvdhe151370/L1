using Microsoft.EntityFrameworkCore;

namespace VuDucNam_L1.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ethnic> Ethnics { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ward>()
                .HasOne(w => w.District)
                .WithMany(d => d.Wards)
                .HasForeignKey(w => w.DistrictId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Ethnic)
                .WithMany()
                .HasForeignKey(e => e.EthnicId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Job)
                .WithMany()
                .HasForeignKey(e => e.JobId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.District)
                .WithMany()
                .HasForeignKey(e => e.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Ward)
                .WithMany()
                .HasForeignKey(e => e.WardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Certificates)
                .WithOne(c => c.Employee)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ethnic>().HasData(
                new Ethnic { EthnicId = 1, EthnicName = "Kinh" },
                new Ethnic { EthnicId = 2, EthnicName = "Hoa" },
                new Ethnic { EthnicId = 3, EthnicName = "Tày" },
                new Ethnic { EthnicId = 4, EthnicName = "Mường" },
                new Ethnic { EthnicId = 5, EthnicName = "Thái" },
                new Ethnic { EthnicId = 6, EthnicName = "Khơ Me" },
                new Ethnic { EthnicId = 7, EthnicName = "Nùng" },
                new Ethnic { EthnicId = 8, EthnicName = "H'Mông" },
                new Ethnic { EthnicId = 9, EthnicName = "Dao" },
                new Ethnic { EthnicId = 10, EthnicName = "Gia Rai" },
                new Ethnic { EthnicId = 11, EthnicName = "Ê Đê" },
                new Ethnic { EthnicId = 12, EthnicName = "Ba Na" },
                new Ethnic { EthnicId = 13, EthnicName = "Xơ Đăng" },
                new Ethnic { EthnicId = 14, EthnicName = "Sán Chay" },
                new Ethnic { EthnicId = 15, EthnicName = "Cơ Tu" },
                new Ethnic { EthnicId = 16, EthnicName = "Chăm" },
                new Ethnic { EthnicId = 17, EthnicName = "Sán Dìu" },
                new Ethnic { EthnicId = 18, EthnicName = "Hrê" },
                new Ethnic { EthnicId = 19, EthnicName = "Ra Glai" },
                new Ethnic { EthnicId = 20, EthnicName = "M'nông" }
            );

            modelBuilder.Entity<Job>().HasData(
                new Job { JobId = 1, JobName = "Developer" },
                new Job { JobId = 2, JobName = "Teacher" },
                new Job { JobId = 3, JobName = "Doctor" },
                new Job { JobId = 4, JobName = "Engineer" },
                new Job { JobId = 5, JobName = "Nurse" },
                new Job { JobId = 6, JobName = "Accountant" },
                new Job { JobId = 7, JobName = "Lawyer" },
                new Job { JobId = 8, JobName = "Artist" },
                new Job { JobId = 9, JobName = "Chef" },
                new Job { JobId = 10, JobName = "Pilot" }
            );

            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Hà Nội" },
                new City { CityId = 2, CityName = "Hồ Chí Minh" },
                new City { CityId = 3, CityName = "Hải Phòng" },
                new City { CityId = 4, CityName = "Đà Nẵng" },
                new City { CityId = 5, CityName = "Cần Thơ" },
                new City { CityId = 6, CityName = "Hà Giang" },
                new City { CityId = 7, CityName = "Cao Bằng" },
                new City { CityId = 8, CityName = "Lai Châu" },
                new City { CityId = 9, CityName = "Lào Cai" },
                new City { CityId = 10, CityName = "Lạng Sơn" }
            );

            modelBuilder.Entity<District>().HasData(
                 new District { DistrictId = 1, DistrictName = "Ba Đình", CityId = 1 },
                 new District { DistrictId = 2, DistrictName = "Hoàn Kiếm", CityId = 1 },
                 new District { DistrictId = 3, DistrictName = "Hai Bà Trưng", CityId = 1 },
                 new District { DistrictId = 4, DistrictName = "Quận 1", CityId = 2 },
                 new District { DistrictId = 5, DistrictName = "Quận 2", CityId = 2 },
                 new District { DistrictId = 6, DistrictName = "Quận 3", CityId = 2 },
                 new District { DistrictId = 7, DistrictName = "Hồng Bàng", CityId = 3 },
                 new District { DistrictId = 8, DistrictName = "Ngô Quyền", CityId = 3 },
                 new District { DistrictId = 9, DistrictName = "Lê Chân", CityId = 3 },
                 new District { DistrictId = 10, DistrictName = "Hải Châu", CityId = 4 },
                 new District { DistrictId = 11, DistrictName = "Thanh Khê", CityId = 4 },
                 new District { DistrictId = 12, DistrictName = "Sơn Trà", CityId = 4 },
                 new District { DistrictId = 13, DistrictName = "Ninh Kiều", CityId = 5 },
                 new District { DistrictId = 14, DistrictName = "Bình Thủy", CityId = 5 },
                 new District { DistrictId = 15, DistrictName = "Cái Răng", CityId = 5 },
                 new District { DistrictId = 16, DistrictName = "Hà Giang", CityId = 6 },
                 new District { DistrictId = 17, DistrictName = "Đồng Văn", CityId = 6 },
                 new District { DistrictId = 18, DistrictName = "Mèo Vạc", CityId = 6 },
                 new District { DistrictId = 19, DistrictName = "Cao Bằng", CityId = 7 },
                 new District { DistrictId = 20, DistrictName = "Bảo Lâm", CityId = 7 },
                 new District { DistrictId = 21, DistrictName = "Hà Quảng", CityId = 7 },
                 new District { DistrictId = 22, DistrictName = "Lai Châu", CityId = 8 },
                 new District { DistrictId = 23, DistrictName = "Tam Đường", CityId = 8 },
                 new District { DistrictId = 24, DistrictName = "Mường Tè", CityId = 8 },
                 new District { DistrictId = 25, DistrictName = "Lào Cai", CityId = 9 },
                 new District { DistrictId = 26, DistrictName = "Bát Xát", CityId = 9 },
                 new District { DistrictId = 27, DistrictName = "Sa Pa", CityId = 9 },
                 new District { DistrictId = 28, DistrictName = "Lạng Sơn", CityId = 10 },
                 new District { DistrictId = 29, DistrictName = "Cao Lộc", CityId = 10 },
                 new District { DistrictId = 30, DistrictName = "Tràng Định", CityId = 10 }
             );

            modelBuilder.Entity<Ward>().HasData(
                new Ward { WardId = 1, WardName = "Tràng Tiền", DistrictId = 2 },
                new Ward { WardId = 2, WardName = "Lý Thái Tổ", DistrictId = 2 },
                new Ward { WardId = 3, WardName = "Trúc Bạch", DistrictId = 1 },
                new Ward { WardId = 4, WardName = "Ngọc Hà", DistrictId = 1 },
                new Ward { WardId = 5, WardName = "Bách Khoa", DistrictId = 3 },
                new Ward { WardId = 6, WardName = "Thượng Đình", DistrictId = 3 },
                new Ward { WardId = 7, WardName = "Bến Nghé", DistrictId = 4 },
                new Ward { WardId = 8, WardName = "Cầu Ông Lãnh", DistrictId = 4 },
                new Ward { WardId = 9, WardName = "Thảo Điền", DistrictId = 5 },
                new Ward { WardId = 10, WardName = "An Phú", DistrictId = 5 },
                new Ward { WardId = 11, WardName = "Nguyễn Thái Bình", DistrictId = 6 },
                new Ward { WardId = 12, WardName = "Phạm Ngũ Lão", DistrictId = 6 },
                new Ward { WardId = 13, WardName = "Cầu Đất", DistrictId = 7 },
                new Ward { WardId = 14, WardName = "Lê Lợi", DistrictId = 7 },
                new Ward { WardId = 15, WardName = "Quán Bàu", DistrictId = 8 },
                new Ward { WardId = 16, WardName = "Lạch Tray", DistrictId = 8 },
                new Ward { WardId = 17, WardName = "Cầu Tre", DistrictId = 9 },
                new Ward { WardId = 18, WardName = "Nghĩa Xá", DistrictId = 9 },
                new Ward { WardId = 19, WardName = "Thạch Thang", DistrictId = 10 },
                new Ward { WardId = 20, WardName = "Hòa Cường Bắc", DistrictId = 10 },
                new Ward { WardId = 21, WardName = "Thanh Bình", DistrictId = 11 },
                new Ward { WardId = 22, WardName = "An Khê", DistrictId = 11 },
                new Ward { WardId = 23, WardName = "Mân Thái", DistrictId = 12 },
                new Ward { WardId = 24, WardName = "An Hải Đông", DistrictId = 12 },
                new Ward { WardId = 25, WardName = "An Bình", DistrictId = 13 },
                new Ward { WardId = 26, WardName = "An Cư", DistrictId = 13 },
                new Ward { WardId = 27, WardName = "Cái Khế", DistrictId = 14 },
                new Ward { WardId = 28, WardName = "Thới Bình", DistrictId = 14 }
            );

        }
    }
}
