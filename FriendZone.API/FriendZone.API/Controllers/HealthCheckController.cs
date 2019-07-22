using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendZone.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FriendZone.API.Controllers
{
    [Route("api/[controller]")]
    public class HealthCheckController : Controller
    {
        private readonly Application Application;

        public HealthCheckController(IOptions<Application> options)
        {
            Application = options.Value;
        }

        [HttpGet]
        public IActionResult DoHealthCheck()
        {
            return Ok(Application);
        }

    }
}