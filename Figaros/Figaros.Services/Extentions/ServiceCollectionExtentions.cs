using Figaros.Data.Abstract;
using Figaros.Data.Concrete;
using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Services.Abstract;
using Figaros.Services.Concrete;
using Figaros.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Figaros.Services.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<AppDbContext>();

            serviceCollection.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped<IAboutService, AboutManager>();
            serviceCollection.AddScoped<IBookingService, BookingManager>();
            serviceCollection.AddScoped<IEmployeeService, EmployeeManager>();
            serviceCollection.AddScoped<IFAQService, FAQManager>();
            serviceCollection.AddScoped<IImageService, ImageManager>();
            serviceCollection.AddScoped<IPriceService, PriceManager>();
            serviceCollection.AddScoped<IProductService, ProductManager>();
            serviceCollection.AddScoped<IProfessionService, ProfessionManager>();
            serviceCollection.AddScoped<IRequestService, RequestManager>();
            serviceCollection.AddScoped<IServiceService, ServiceManager>();
            serviceCollection.AddScoped<ISettingService, SettingManager>();
            serviceCollection.AddScoped<ISliderService, SliderManager>();
            serviceCollection.AddScoped<ISponsorService, SponsorManager>();
            serviceCollection.AddScoped<ITimeService, TimeManager>();
            return serviceCollection;
        }
    }
}
