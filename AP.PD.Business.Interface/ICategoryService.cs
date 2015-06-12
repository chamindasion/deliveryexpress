
using AP.PD.Shared;
using System.Collections.Generic;

namespace AP.PD.Business.Interface
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAll();
    }
}
