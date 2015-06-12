
using AP.PD.Domain;
using System;
using System.Collections.Generic;

namespace AP.PD.Data
{
    public interface IDeliveryRepository
    {
        List<ParcelDeliveryDomain> GetParcelDeliveries(DateTime selectedDate, Guid roleId, Guid userId);
        ParcelDeliveryDomain AddParcelDelivery(ParcelDeliveryDomain parcelDelivery);
        bool DeleteDelivery(Guid deliveryId);
    }
}
