using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Data.Concrete.EntityFramework.Repositories
{
    public class EFProfessionRepository : EFEntityRepositoryBase<Profession>, IProfessionRepository
    {
        public EFProfessionRepository(DbContext context) : base(context)
        {
        }
    }
}
