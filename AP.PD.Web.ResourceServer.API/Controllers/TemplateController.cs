//using AP.PD.Business.Services;
//using AP.PD.Business.Services.Interfaces;

using AP.PD.Shared;
using AP.PD.Web.ExceptionHandling.API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AP.PD.Web.ResourceServer.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/templates")]
    [NotImplExceptionFilter]
    public class TemplateController : ApiController
    {
        public TemplateController()
        {

        }

        //private IAcpTemplateVersionService _templateVersionService;
        //public TemplateController(IAcpTemplateVersionService templateVersionService)
        //{
        //    _templateVersionService = templateVersionService;
        //}


        [Route("")]
        public List<AcpTemplateDTO> GetTemplates()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var customClaimValue = principal.Claims.Where(c => c.Type == "role").Single().Value;

            //throw new NotImplementedException("hiiiiii");
            //return _templateVersionService.GetTemplatesWithHighestVersions().ToList();
            return Template.GetAllTemplates();
        }

        [Route("Customers")]
        public List<Customer> GetCustomers()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //var customClaimValue = principal.Claims.Where(c => c.Type == "CompanyID").Single().Value;

            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Lee", LastName = "Carroll", Address = "1234 Anywhere St.", City = "Phoenix", Orders = null},                
                new Customer { Id = 2, FirstName = "Jesse", LastName = "Hawkins", Address = "89 W. Center St.", City = "Atlanta", Orders = null},
                new Customer { Id = 3, FirstName = "Charles", LastName = "Sutton", Address = "455 7th Ave.", City = "Quebec", Orders = null}
            };
            return customers;
        }

        [Route("{id}")]
        public AcpTemplateDTO GetTemplate(Guid id)
        {
            //return _templateVersionService.GetTemplatesWithHighestVersions().ToList();
            var template = Template.GetAllTemplates().Find(p => p.Id == id);
            ControllerHelper.ValidateTemplate(id, template);
            return template;
        }

        [Route("Customers/Add")]
        [HttpPost]
        public HttpResponseMessage PostTemplate(Customer templateDto)
        {
            //item = repository.Add(item);
            var response = Request.CreateResponse<Customer>(HttpStatusCode.Created, templateDto);
            string uri = Url.Link("DefaultApi", new { id = templateDto.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //[Route("")]
        //public void PutTemplate(AcpTemplateDTO template)
        //{
        //    var templateDto = Template.GetAllTemplates().Find(p => p.Id == template.Id);
        //    ControllerHelper.ValidateTemplate(template.Id, template);
        //}
    }

    public class Template
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }

        public static List<AcpTemplateDTO> GetAllTemplates()
        {
            var orderList = new List<AcpTemplateDTO> 
            {
                new AcpTemplateDTO {Id = new Guid("C89E08AA-1304-46E6-BC8A-27A14297B5B0"), OriginalName = "Temp1"},
                new AcpTemplateDTO {Id = new Guid("3A647D16-B5A4-4D1B-B4D5-A31FC5A9C788"), OriginalName = "Temp2"},
                new AcpTemplateDTO {Id = new Guid("74CD6EFC-DF41-4F4D-B436-3333984F0C1B"), OriginalName = "Temp3"},
                new AcpTemplateDTO {Id = new Guid("1BA837C0-BC74-44EA-BE1B-CB684343B078"), OriginalName = "Temp4"},
                new AcpTemplateDTO {Id = new Guid("A6C167D9-56A7-45E3-AB36-7987545C2F8F"), OriginalName = "Temp5"}
            };

            return orderList;
        }
    }

    //{ product: 'Basket', price: 29.99, quantity: 1, orderTotal: 29.99 },
    //id: 11, firstName: 'Shanika', lastName: 'Passmore', address: '459 S. International Dr.', city: 'Orlando'
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<object> Orders { get; set; }
    }

}
