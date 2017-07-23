using System.Web;
using Telerik.Reporting.Cache.Interfaces;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;

public class ReportsController : ReportsControllerBase
{
    protected override IReportResolver CreateReportResolver()
    {
        var reportsPath = HttpContext.Current.Server.MapPath("~/Reportes/");

        return new ReportFileResolver(reportsPath)
            .AddFallbackResolver(new ReportTypeResolver());
    }

    protected override ICache CreateCache()
    {
        return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache();
    }
}