using DarkSoulsOBSOverlay.Models;
using DarkSoulsOBSOverlay.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DarkSoulsOBSOverlay.Controllers
{
    [Route("Settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSettings()
        {
            return new OkObjectResult(JsonConvert.SerializeObject(DarkSoulsReader.GetSettings()));
        }

        [HttpPost]
        public IActionResult UpdateSettings([FromBody] string data)
        {
            DarkSoulsReader.SetSettings(JsonConvert.DeserializeObject<Settings>(data));
            return new OkResult();
        }
    }
}
