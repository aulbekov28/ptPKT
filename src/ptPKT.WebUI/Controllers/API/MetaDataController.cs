using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Localization;
using ptPKT.Core.Interfaces;

namespace ptPKT.WebUI.Controllers.API
{
    public class MetaDataController : BaseApiController
    {
        private readonly IEnvironmentService _localizer;

        public MetaDataController(IEnvironmentService environmentService)
        {
            _localizer = environmentService;
        }

        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = typeof(Startup).Assembly;

            var creationDate = System.IO.File.GetCreationTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return Ok($"Version: {version}, Last Updated: {creationDate}");
        }
    }
}