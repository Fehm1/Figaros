using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Figaros.Data.Concrete.EntityFramework.Repositories
{
    public class EFEmployeeRepository : EFEntityRepositoryBase<Employee>, IEmployeeRepository
    {
        public EFEmployeeRepository(DbContext context) : base(context)
        {
        }
    }
}
