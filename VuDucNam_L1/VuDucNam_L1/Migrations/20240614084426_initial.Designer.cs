﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VuDucNam_L1.DataAccess;

#nullable disable

namespace VuDucNam_L1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240614084426_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            CityId = 1,
                            CityName = "Hà Nội"
                        },
                        new
                        {
                            CityId = 2,
                            CityName = "Hồ Chí Minh"
                        },
                        new
                        {
                            CityId = 3,
                            CityName = "Hải Phòng"
                        },
                        new
                        {
                            CityId = 4,
                            CityName = "Đà Nẵng"
                        },
                        new
                        {
                            CityId = 5,
                            CityName = "Cần Thơ"
                        },
                        new
                        {
                            CityId = 6,
                            CityName = "Hà Giang"
                        },
                        new
                        {
                            CityId = 7,
                            CityName = "Cao Bằng"
                        },
                        new
                        {
                            CityId = 8,
                            CityName = "Lai Châu"
                        },
                        new
                        {
                            CityId = 9,
                            CityName = "Lào Cai"
                        },
                        new
                        {
                            CityId = 10,
                            CityName = "Lạng Sơn"
                        });
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.District", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DistrictId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictId");

                    b.HasIndex("CityId");

                    b.ToTable("Districts");

                    b.HasData(
                        new
                        {
                            DistrictId = 1,
                            CityId = 1,
                            DistrictName = "Ba Đình"
                        },
                        new
                        {
                            DistrictId = 2,
                            CityId = 1,
                            DistrictName = "Hoàn Kiếm"
                        },
                        new
                        {
                            DistrictId = 3,
                            CityId = 1,
                            DistrictName = "Hai Bà Trưng"
                        },
                        new
                        {
                            DistrictId = 4,
                            CityId = 2,
                            DistrictName = "Quận 1"
                        },
                        new
                        {
                            DistrictId = 5,
                            CityId = 2,
                            DistrictName = "Quận 2"
                        },
                        new
                        {
                            DistrictId = 6,
                            CityId = 2,
                            DistrictName = "Quận 3"
                        },
                        new
                        {
                            DistrictId = 7,
                            CityId = 3,
                            DistrictName = "Hồng Bàng"
                        },
                        new
                        {
                            DistrictId = 8,
                            CityId = 3,
                            DistrictName = "Ngô Quyền"
                        },
                        new
                        {
                            DistrictId = 9,
                            CityId = 3,
                            DistrictName = "Lê Chân"
                        },
                        new
                        {
                            DistrictId = 10,
                            CityId = 4,
                            DistrictName = "Hải Châu"
                        },
                        new
                        {
                            DistrictId = 11,
                            CityId = 4,
                            DistrictName = "Thanh Khê"
                        },
                        new
                        {
                            DistrictId = 12,
                            CityId = 4,
                            DistrictName = "Sơn Trà"
                        },
                        new
                        {
                            DistrictId = 13,
                            CityId = 5,
                            DistrictName = "Ninh Kiều"
                        },
                        new
                        {
                            DistrictId = 14,
                            CityId = 5,
                            DistrictName = "Bình Thủy"
                        },
                        new
                        {
                            DistrictId = 15,
                            CityId = 5,
                            DistrictName = "Cái Răng"
                        },
                        new
                        {
                            DistrictId = 16,
                            CityId = 6,
                            DistrictName = "Hà Giang"
                        },
                        new
                        {
                            DistrictId = 17,
                            CityId = 6,
                            DistrictName = "Đồng Văn"
                        },
                        new
                        {
                            DistrictId = 18,
                            CityId = 6,
                            DistrictName = "Mèo Vạc"
                        },
                        new
                        {
                            DistrictId = 19,
                            CityId = 7,
                            DistrictName = "Cao Bằng"
                        },
                        new
                        {
                            DistrictId = 20,
                            CityId = 7,
                            DistrictName = "Bảo Lâm"
                        },
                        new
                        {
                            DistrictId = 21,
                            CityId = 7,
                            DistrictName = "Hà Quảng"
                        },
                        new
                        {
                            DistrictId = 22,
                            CityId = 8,
                            DistrictName = "Lai Châu"
                        },
                        new
                        {
                            DistrictId = 23,
                            CityId = 8,
                            DistrictName = "Tam Đường"
                        },
                        new
                        {
                            DistrictId = 24,
                            CityId = 8,
                            DistrictName = "Mường Tè"
                        },
                        new
                        {
                            DistrictId = 25,
                            CityId = 9,
                            DistrictName = "Lào Cai"
                        },
                        new
                        {
                            DistrictId = 26,
                            CityId = 9,
                            DistrictName = "Bát Xát"
                        },
                        new
                        {
                            DistrictId = 27,
                            CityId = 9,
                            DistrictName = "Sa Pa"
                        },
                        new
                        {
                            DistrictId = 28,
                            CityId = 10,
                            DistrictName = "Lạng Sơn"
                        },
                        new
                        {
                            DistrictId = 29,
                            CityId = 10,
                            DistrictName = "Cao Lộc"
                        },
                        new
                        {
                            DistrictId = 30,
                            CityId = 10,
                            DistrictName = "Tràng Định"
                        });
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CitizenNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EthnicId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WardId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CityId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EthnicId");

                    b.HasIndex("JobId");

                    b.HasIndex("WardId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Ethnic", b =>
                {
                    b.Property<int>("EthnicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EthnicId"));

                    b.Property<string>("EthnicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EthnicId");

                    b.ToTable("Ethnics");

                    b.HasData(
                        new
                        {
                            EthnicId = 1,
                            EthnicName = "Kinh"
                        },
                        new
                        {
                            EthnicId = 2,
                            EthnicName = "Hoa"
                        },
                        new
                        {
                            EthnicId = 3,
                            EthnicName = "Tày"
                        },
                        new
                        {
                            EthnicId = 4,
                            EthnicName = "Mường"
                        },
                        new
                        {
                            EthnicId = 5,
                            EthnicName = "Thái"
                        },
                        new
                        {
                            EthnicId = 6,
                            EthnicName = "Khơ Me"
                        },
                        new
                        {
                            EthnicId = 7,
                            EthnicName = "Nùng"
                        },
                        new
                        {
                            EthnicId = 8,
                            EthnicName = "H'Mông"
                        },
                        new
                        {
                            EthnicId = 9,
                            EthnicName = "Dao"
                        },
                        new
                        {
                            EthnicId = 10,
                            EthnicName = "Gia Rai"
                        },
                        new
                        {
                            EthnicId = 11,
                            EthnicName = "Ê Đê"
                        },
                        new
                        {
                            EthnicId = 12,
                            EthnicName = "Ba Na"
                        },
                        new
                        {
                            EthnicId = 13,
                            EthnicName = "Xơ Đăng"
                        },
                        new
                        {
                            EthnicId = 14,
                            EthnicName = "Sán Chay"
                        },
                        new
                        {
                            EthnicId = 15,
                            EthnicName = "Cơ Tu"
                        },
                        new
                        {
                            EthnicId = 16,
                            EthnicName = "Chăm"
                        },
                        new
                        {
                            EthnicId = 17,
                            EthnicName = "Sán Dìu"
                        },
                        new
                        {
                            EthnicId = 18,
                            EthnicName = "Hrê"
                        },
                        new
                        {
                            EthnicId = 19,
                            EthnicName = "Ra Glai"
                        },
                        new
                        {
                            EthnicId = 20,
                            EthnicName = "M'nông"
                        });
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            JobId = 1,
                            JobName = "Developer"
                        },
                        new
                        {
                            JobId = 2,
                            JobName = "Teacher"
                        },
                        new
                        {
                            JobId = 3,
                            JobName = "Doctor"
                        },
                        new
                        {
                            JobId = 4,
                            JobName = "Engineer"
                        },
                        new
                        {
                            JobId = 5,
                            JobName = "Nurse"
                        },
                        new
                        {
                            JobId = 6,
                            JobName = "Accountant"
                        },
                        new
                        {
                            JobId = 7,
                            JobName = "Lawyer"
                        },
                        new
                        {
                            JobId = 8,
                            JobName = "Artist"
                        },
                        new
                        {
                            JobId = 9,
                            JobName = "Chef"
                        },
                        new
                        {
                            JobId = 10,
                            JobName = "Pilot"
                        });
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Ward", b =>
                {
                    b.Property<int>("WardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WardId"));

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WardId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Wards");

                    b.HasData(
                        new
                        {
                            WardId = 1,
                            DistrictId = 2,
                            WardName = "Tràng Tiền"
                        },
                        new
                        {
                            WardId = 2,
                            DistrictId = 2,
                            WardName = "Lý Thái Tổ"
                        },
                        new
                        {
                            WardId = 3,
                            DistrictId = 1,
                            WardName = "Trúc Bạch"
                        },
                        new
                        {
                            WardId = 4,
                            DistrictId = 1,
                            WardName = "Ngọc Hà"
                        },
                        new
                        {
                            WardId = 5,
                            DistrictId = 3,
                            WardName = "Bách Khoa"
                        },
                        new
                        {
                            WardId = 6,
                            DistrictId = 3,
                            WardName = "Thượng Đình"
                        },
                        new
                        {
                            WardId = 7,
                            DistrictId = 4,
                            WardName = "Bến Nghé"
                        },
                        new
                        {
                            WardId = 8,
                            DistrictId = 4,
                            WardName = "Cầu Ông Lãnh"
                        },
                        new
                        {
                            WardId = 9,
                            DistrictId = 5,
                            WardName = "Thảo Điền"
                        },
                        new
                        {
                            WardId = 10,
                            DistrictId = 5,
                            WardName = "An Phú"
                        },
                        new
                        {
                            WardId = 11,
                            DistrictId = 6,
                            WardName = "Nguyễn Thái Bình"
                        },
                        new
                        {
                            WardId = 12,
                            DistrictId = 6,
                            WardName = "Phạm Ngũ Lão"
                        },
                        new
                        {
                            WardId = 13,
                            DistrictId = 7,
                            WardName = "Cầu Đất"
                        },
                        new
                        {
                            WardId = 14,
                            DistrictId = 7,
                            WardName = "Lê Lợi"
                        },
                        new
                        {
                            WardId = 15,
                            DistrictId = 8,
                            WardName = "Quán Bàu"
                        },
                        new
                        {
                            WardId = 16,
                            DistrictId = 8,
                            WardName = "Lạch Tray"
                        },
                        new
                        {
                            WardId = 17,
                            DistrictId = 9,
                            WardName = "Cầu Tre"
                        },
                        new
                        {
                            WardId = 18,
                            DistrictId = 9,
                            WardName = "Nghĩa Xá"
                        },
                        new
                        {
                            WardId = 19,
                            DistrictId = 10,
                            WardName = "Thạch Thang"
                        },
                        new
                        {
                            WardId = 20,
                            DistrictId = 10,
                            WardName = "Hòa Cường Bắc"
                        },
                        new
                        {
                            WardId = 21,
                            DistrictId = 11,
                            WardName = "Thanh Bình"
                        },
                        new
                        {
                            WardId = 22,
                            DistrictId = 11,
                            WardName = "An Khê"
                        },
                        new
                        {
                            WardId = 23,
                            DistrictId = 12,
                            WardName = "Mân Thái"
                        },
                        new
                        {
                            WardId = 24,
                            DistrictId = 12,
                            WardName = "An Hải Đông"
                        },
                        new
                        {
                            WardId = 25,
                            DistrictId = 13,
                            WardName = "An Bình"
                        },
                        new
                        {
                            WardId = 26,
                            DistrictId = 13,
                            WardName = "An Cư"
                        },
                        new
                        {
                            WardId = 27,
                            DistrictId = 14,
                            WardName = "Cái Khế"
                        },
                        new
                        {
                            WardId = 28,
                            DistrictId = 14,
                            WardName = "Thới Bình"
                        });
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Certificate", b =>
                {
                    b.HasOne("VuDucNam_L1.DataAccess.Employee", "Employee")
                        .WithMany("Certificates")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.District", b =>
                {
                    b.HasOne("VuDucNam_L1.DataAccess.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Employee", b =>
                {
                    b.HasOne("VuDucNam_L1.DataAccess.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VuDucNam_L1.DataAccess.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VuDucNam_L1.DataAccess.Ethnic", "Ethnic")
                        .WithMany()
                        .HasForeignKey("EthnicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VuDucNam_L1.DataAccess.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VuDucNam_L1.DataAccess.Ward", "Ward")
                        .WithMany()
                        .HasForeignKey("WardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("District");

                    b.Navigation("Ethnic");

                    b.Navigation("Job");

                    b.Navigation("Ward");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Ward", b =>
                {
                    b.HasOne("VuDucNam_L1.DataAccess.District", "District")
                        .WithMany("Wards")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.City", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.District", b =>
                {
                    b.Navigation("Wards");
                });

            modelBuilder.Entity("VuDucNam_L1.DataAccess.Employee", b =>
                {
                    b.Navigation("Certificates");
                });
#pragma warning restore 612, 618
        }
    }
}
