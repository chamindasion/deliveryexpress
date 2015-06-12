
using AP.PD.Shared;
using System;
using System.Collections.Generic;

namespace AP.PD.Business.Interface
{
    public interface IDeliveryService
    {
        List<ParcelDeliveryDto> GetAll(string roleIdValue, string userValue);
        void Add(ParcelDeliveryDto[] deliveryDtos, string userValue);
        bool Delete(Guid deliveryId);
    }
}
