using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Localization;

namespace ptPKT.WebUI.Controllers.API
{
    public class MetaDataController : BaseApiController
    {
        private readonly IStringLocalizer _localizer;

        public MetaDataController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = typeof(Startup).Assembly;

            var creationDate = System.IO.File.GetCreationTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            _localizer.GetString("Hello");

            return Ok($"Version: {version}, Last Updated: {creationDate}");
        }
    }
}