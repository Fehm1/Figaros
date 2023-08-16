namespace Figaros.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAboutRepository About { get; }
        IBookingRepository Bookings { get; }
        IEmployeeRepository Employees { get; }
        IFAQRepository FAQs { get; }
        IImageRepository Images { get; }
        IOurSalonRepository OurSalon { get; }
        IPriceRepository Prices { get; }
        IProductRepository Products { get; }
        IProfessionRepository Professions { get; }
        IRequestRepository Requests { get; }
        IServiceRepository Services { get; }
        ISettingRepository Settings { get; }
        ISliderRepository Sliders { get; }
        ISponsorRepository Sponsors { get; }
        ITimeRepository Times { get; }
        Task<int> SaveAsync();
    }
}
