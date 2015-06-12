using AP.PD.Business.Interface;
using AP.PD.Data;
using AP.PD.Shared;
using System.Collections.Generic;
using System.Linq;

namespace AP.PD.Business.Domain
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repository = new CategoryRepository();

        public List<CategoryDto> GetAll()
        {
            var categories = _repository.GetCategories();
            List<CategoryDto> categoryDtos = null;
            if (categories != null && categories.Any())
            {
                categoryDtos = categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                }).ToList();
            }
            return categoryDtos;
        }
    }
}
