using Figaros.Data.Abstract;
using Figaros.Data.Concrete.EntityFramework.Contexts;
using Figaros.Data.Concrete.EntityFramework.Repositories;

namespace Figaros.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly EFAboutRepository _eFAboutRepository;
        private readonly EFBookingRepository _eFBookingRepository;
        private readonly EFEmployeeRepository _eFEmployeeRepository;
        private readonly EFFAQRepository _eFFAQRepository;
        private readonly EFImageRepository _eFImageRepository;
        private readonly EFPriceRepository _eFPriceRepository;
        private readonly EFProductRepository _eFProductRepository;
        private readonly EFProfessionRepository _eEFProfessionRepository;
        private readonly EFRequestRepository _eFRequestRepository;
        private readonly EFServiceRepository _eFServiceRepository;
        private readonly EFSettingRepository _eFSettingRepository;
        private readonly EFSliderRepository _eFSliderRepository;
        private readonly EFSponsorRepository _eFSponsorRepository;
        private readonly EFTimeRepository _eFTimeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAboutRepository About => _eFAboutRepository ?? new EFAboutRepository(_context);

        public IBookingRepository Bookings => _eFBookingRepository ?? new EFBookingRepository(_context);

        public IEmployeeRepository Employees => _eFEmployeeRepository ?? new EFEmployeeRepository(_context);

        public IFAQRepository FAQs => _eFFAQRepository ?? new EFFAQRepository(_context);

        public IImageRepository Images => _eFImageRepository ?? new EFImageRepository(_context);

        public IPriceRepository Prices => _eFPriceRepository ?? new EFPriceRepository(_context);

        public IProductRepository Products => _eFProductRepository ?? new EFProductRepository(_context);

        public IProfessionRepository Professions => _eEFProfessionRepository ?? new EFProfessionRepository(_context);

        public IServiceRepository Services => _eFServiceRepository ?? new EFServiceRepository(_context);

        public ISettingRepository Settings => _eFSettingRepository ?? new EFSettingRepository(_context);

        public ISliderRepository Sliders => _eFSliderRepository ?? new EFSliderRepository(_context);

        public ISponsorRepository Sponsors => _eFSponsorRepository ?? new EFSponsorRepository(_context);

        public IRequestRepository Requests => _eFRequestRepository ?? new EFRequestRepository(_context);

        public ITimeRepository Times => _eFTimeRepository ?? new EFTimeRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
