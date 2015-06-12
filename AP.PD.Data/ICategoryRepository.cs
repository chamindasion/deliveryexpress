
using AP.PD.Domain;
using System.Collections.Generic;

namespace AP.PD.Data
{
    public interface ICategoryRepository
    {
        List<CategoryDomain> GetCategories();
    }
}
