using AP.PD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AP.PD.Web.ResourceServer.API.Repository
{
    public class ResourceRepository : IDisposable
    {
        private readonly ResourceContext _resourceContext;

        public ResourceRepository()
        {
            _resourceContext = new ResourceContext();
        }

        public List<ParcelDeliveryDomain> GetParcelDeliveries(DateTime selectedDate, Guid roleId, Guid userId)
        {
            if (roleId != Guid.Empty)
            {
                var role = _resourceContext.Roles.Single(p => p.Id == roleId);
                if (role != null && role.Name.Equals("Rep"))
                {
                    return _resourceContext.ParcelDeliveries.Where(p => p.DeliveredDate <= selectedDate && p.RepUserId == userId).ToList();
                }
            }
            return _resourceContext.ParcelDeliveries.Where(p => p.DeliveredDate <= selectedDate).ToList();
        }

        public List<CategoryDomain> GetCategories()
        {
            return _resourceContext.Categories.ToList();
        }

        public ParcelDeliveryDomain AddParcelDelivery(ParcelDeliveryDomain parcelDelivery)
        {
            _resourceContext.ParcelDeliveries.Add(parcelDelivery);
            _resourceContext.SaveChanges();
            return parcelDelivery;
        }

        public void Dispose()
        {
            _resourceContext.Dispose();
        }
    }
}