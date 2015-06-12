using AP.PD.Business.Interface;
using AP.PD.Domain;
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
    [RoutePrefix("api/parcel")]
    [NotImplExceptionFilter]
    public class ParcelController : BaseController
    {
        private readonly IDeliveryService _deliveryService;

        public ParcelController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [Route("")]
        public List<ParcelDeliveryDto> GetParcelDeliveries()
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userValue = string.Empty;
            var roleIdValue = string.Empty;
            if (principal != null)
            {
                roleIdValue = principal.Claims.Single(c => c.Type == "role").Value;
                userValue = principal.Claims.Single(c => c.Type == "userId").Value;
            }

            //var deliveriesDomains = ResourceRepository.GetParcelDeliveries(DateTime.Now, new Guid(roleIdValue), new Guid(userValue));
            var deliveriesDomains = _deliveryService.GetAll(roleIdValue, userValue);

            List<ParcelDeliveryDto> deliveryDtos = null;
            if (deliveriesDomains != null && deliveriesDomains.Any())
            {
                deliveryDtos = new List<ParcelDeliveryDto>();
                foreach (var deliveryDomain in deliveriesDomains)
                {
                    var parcelDeliveryDto = new ParcelDeliveryDto
                    {
                        Id = deliveryDomain.Id,
                        CategoryId = deliveryDomain.CategoryId,
                        DeliveredDate = deliveryDomain.DeliveredDate,
                        DeliveredQuantity = deliveryDomain.DeliveredQuantity,
                        RepUserId = deliveryDomain.RepUserId
                    };

                    if (deliveryDomain.Category != null)
                    {
                        var categoryDto = new CategoryDto()
                        {
                            Id = deliveryDomain.Category.Id,
                            Name = deliveryDomain.Category.Name,
                            Description = deliveryDomain.Category.Description
                        };
                        parcelDeliveryDto.Category = categoryDto;
                    }

                    if (deliveryDomain.RepUser != null)
                    {
                        var userDto = new UserDto()
                        {
                            Id = deliveryDomain.RepUser.Id,
                            LoginId = deliveryDomain.RepUser.LoginId,
                            Password = deliveryDomain.RepUser.Password,
                            RoleId = deliveryDomain.RepUser.RoleId
                        };
                        parcelDeliveryDto.RepUser = userDto;
                    }
                    deliveryDtos.Add(parcelDeliveryDto);
                }
            }
            return deliveryDtos;
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage PostParcelDeliveryArray(ParcelDeliveryDto[] deliverieDtos)
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            if (principal != null)
            {
                var userValue = principal.Claims.Single(c => c.Type == "userId").Value;

                if (deliverieDtos != null && deliverieDtos.Any())
                {
                    _deliveryService.Add(deliverieDtos, userValue);
                }
            }
            var response = Request.CreateResponse<ParcelDeliveryDomain>(HttpStatusCode.Created, null);
            return response;
        }

        [Route("{deliveryId:Guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteParcelDelivery(Guid deliveryId)
        {
            try
            {
                if (_deliveryService.Delete(deliveryId))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Delivery not found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
