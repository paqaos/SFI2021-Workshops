using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFI.Microservice.Events.Controllers
{
    [Route("")]
    [ApiController]
    public class ProbeController : ControllerBase
    {
        [HttpGet]
        public string GetSampleResponse()
        {
            return "Hello world! from docker";
        }
    }
}
