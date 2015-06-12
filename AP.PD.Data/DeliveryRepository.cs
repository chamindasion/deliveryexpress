using AP.PD.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AP.PD.Data
{
    public class DeliveryRepository : IDeliveryRepository
    {
        public List<ParcelDeliveryDomain> GetParcelDeliveries(DateTime selectedDate, Guid roleId, Guid userId)
        {
            using (var apContext = new ApContext())
            {
                if (roleId != Guid.Empty)
                {
                    var role = apContext.Roles.FirstOrDefault(p => p.Id == roleId);
                    if (role != null && role.Name.Equals("Rep"))
                    {
                        return
                            apContext.ParcelDeliveries.Where(
                                p => p.DeliveredDate <= selectedDate && p.RepUserId == userId).Include(p => p.Category).Include(p => p.RepUser).ToList();
                    }
                }
                return apContext.ParcelDeliveries.Where(p => p.DeliveredDate <= selectedDate).Include(p => p.Category).Include(p => p.RepUser).ToList();
            }
        }


        public ParcelDeliveryDomain AddParcelDelivery(ParcelDeliveryDomain parcelDelivery)
        {
            using (var apContext = new ApContext())
            {
                apContext.ParcelDeliveries.Add(parcelDelivery);
                apContext.SaveChanges();
                return parcelDelivery;
            }
        }

        public bool DeleteDelivery(Guid deliveryId)
        {
            using (var apContext = new ApContext())
            {
                if (deliveryId != Guid.Empty)
                {
                    var deliveryDomain = apContext.ParcelDeliveries.FirstOrDefault(p => p.Id == deliveryId);
                    if (deliveryDomain != null)
                    {
                        apContext.ParcelDeliveries.Remove(deliveryDomain);
                        apContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }

    }
}