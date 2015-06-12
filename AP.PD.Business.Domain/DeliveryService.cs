
using AP.PD.Business.Interface;
using AP.PD.Data;
using AP.PD.Domain;
using AP.PD.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AP.PD.Business.Domain
{
    public class DeliveryService : IDeliveryService
    {
        private readonly DeliveryRepository _repository = new DeliveryRepository();

        public List<Shared.ParcelDeliveryDto> GetAll(string roleIdValue, string userValue)
        {
            var deliveriesDomains = _repository.GetParcelDeliveries(DateTime.Now, new Guid(roleIdValue), new Guid(userValue));

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

        public void Add(ParcelDeliveryDto[] deliveryDtos, string userValue)
        {
            if (deliveryDtos != null && deliveryDtos.Any())
            {
                foreach (var deliveryDomain in from deliveryDto in deliveryDtos
                                               where deliveryDto.DeliveredQuantity > 0
                                               select new ParcelDeliveryDomain
                                               {
                                                   Id = Guid.NewGuid(),
                                                   DeliveredDate = deliveryDto.DeliveredDate.Date,
                                                   RepUserId = new Guid(userValue),
                                                   CategoryId = new Guid(Convert.ToString(deliveryDto.CategoryId)),
                                                   DeliveredQuantity = deliveryDto.DeliveredQuantity
                                               })
                {
                    _repository.AddParcelDelivery(deliveryDomain);
                }
            }
        }

        public bool Delete(Guid deliveryId)
        {
            return _repository.DeleteDelivery(deliveryId);
        }
    }
}
