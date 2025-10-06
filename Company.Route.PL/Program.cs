using Company.Route.BLL;
using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Data.Contexts;
using Company.Route.PL.Mapping;
using Company.Route.PL.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.Route.PL
{
    public class Program
    {
        public static void Main(string[] args)

        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();  // Register Build-in MVC Services
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  // Allow DI For DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  // Allow DI For DepartmentRepository

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  // Allow DI For UnitOfWork

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["DefaultConnection"]); // Pass Key ==> return Value
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });  // Allow DI For CompanyDbContext

            // Life Time Services
            //builder.Services.AddScoped<();        // Create Life Time Per Request  -  Object UnReachable   
            //builder.Services.AddTransient();      // Create Life Time Per Operation 
            //builder.Services.TryAddSingleton();   // Create Life Time Per App 

            builder.Services.AddScoped<IScopedServices, ScopedServices>(); // Create Lif Time Per Request 
            builder.Services.AddTransient<ITransiantService, TransiantService>();
            builder.Services.AddSingleton<ISingletonServices, SingeltonServices>();

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));


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


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
