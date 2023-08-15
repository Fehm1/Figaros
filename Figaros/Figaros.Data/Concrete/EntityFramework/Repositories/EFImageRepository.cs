using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Data.Concrete.EntityFramework.Repositories
{
    public class EFImageRepository : EFEntityRepositoryBase<Image>, IImageRepository
    {
        public EFImageRepository(DbContext context) : base(context)
        {
        }
    }
}
