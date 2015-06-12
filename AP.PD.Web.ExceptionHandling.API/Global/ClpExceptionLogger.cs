using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace AP.PD.Web.ExceptionHandling.API.Global
{
    public class ClpExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
        }
    }
}

