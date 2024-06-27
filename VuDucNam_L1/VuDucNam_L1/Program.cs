using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Repository.Repositories;
using VuDucNam_L1.Service.IServices;
using VuDucNam_L1.Service.Services;
using VuDucNam_L1.Validation;

namespace VuDucNam_L1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddFluentValidation();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<IValidator<EmployeeModel>, EmployeeValidator>();
            builder.Services.AddTransient<IValidator<CityModel>, CityValidator>();
            builder.Services.AddTransient<IValidator<DistrictModel>, DistrictValidator>();
            builder.Services.AddTransient<IValidator<WardModel>, WardValidator>();
            builder.Services.AddTransient<IValidator<CertificateModel>, CertificateValidator>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
            builder.Services.AddScoped<IWardRepository, WardRepository>();
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IEthnicRepository, EthnicRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IDistrictService, DistrictService>();
            builder.Services.AddScoped<IWardService, WardService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<IEthnicService, EthnicService>();

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
