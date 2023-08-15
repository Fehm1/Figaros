using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Data.Concrete.EntityFramework.Repositories
{
    public class EFBookingRepository : EFEntityRepositoryBase<Booking>, IBookingRepository
    {
        public EFBookingRepository(DbContext context) : base(context)
        {
        }
    }
}
