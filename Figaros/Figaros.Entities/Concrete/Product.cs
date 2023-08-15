using Figaros.Shared.Entities.Abstract;

namespace Figaros.Entities.Concrete
{
    public class Product : EntityBase, IEntity
    {
        public string ImageString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public int ProductAmount { get; set; }
        public int SaleCount { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }

    }
}
