using DarkSoulsOBSOverlay.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace DarkSoulsOBSOverlay.Controllers
{
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("Status/Subscribe")]
        public async Task Subscribe()
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {

                        WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                        await CommunicationService.AddClient(HttpContext, webSocket);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception e)
            {

            }
        }

        [HttpGet]
        [Route("Close")]
        public IActionResult Close()
        {
            Response.OnCompleted(() => Task.Run(() =>
            {
                Environment.Exit(0);
            }));
            return new OkObjectResult("Closing Application");
        }
    }
}
