using AP.PD.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AP.PD.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<CategoryDomain> GetCategories()
        {
            using (var apContext = new ApContext())
            {
                return apContext.Categories.ToList();
            }
        }
    }
}
