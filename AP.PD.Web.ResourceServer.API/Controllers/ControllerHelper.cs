using AP.PD.Shared;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using AP.PD.Business.Services.Interfaces;

namespace AP.PD.Web.ResourceServer.API.Controllers
{
    public class ControllerHelper
    {
        internal static void ValidateTemplate(Guid id, AcpTemplateDTO template)
        {
            if (template == null)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No template with Id = {0}", id)),
                    ReasonPhrase = "Template Id Not Found"
                };
                throw new HttpResponseException(responseMessage);
            }
        }

    }
}