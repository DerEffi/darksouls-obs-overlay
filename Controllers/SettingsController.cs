using DarkSoulsOBSOverlay.Models;
using DarkSoulsOBSOverlay.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DarkSoulsOBSOverlay.Controllers
{
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpGet]
        [Route("Settings")]
        public IActionResult GetSettings()
        {
            return new OkObjectResult(JsonConvert.SerializeObject(DarkSoulsReader.GetSettings()));
        }

        [HttpPost]
        [Route("Settings")]
        public IActionResult UpdateSettings([FromBody] Settings settings)
        {
            DarkSoulsReader.SetSettings(settings);
            return new OkResult();
        }
    }
}
