using AP.PD.Business.Interface;
using AP.PD.Shared;
using AP.PD.Web.ExceptionHandling.API.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AP.PD.Web.ResourceServer.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/category")]
    [NotImplExceptionFilter]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("")]
        public List<CategoryDto> GetCategories()
        {
            //var categories = ResourceRepository.GetCategories();
            var categories = _categoryService.GetAll();
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
