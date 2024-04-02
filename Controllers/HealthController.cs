using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using dvcsharp_core_api.Models;
using dvcsharp_core_api.Data;

namespace dvcsharp_core_api
{
   [Route("api/[controller]")]
   public class HealthController : Controller
   {
      public HealthController()
      {
      }

      [HttpGet]
      public IActionResult Get([FromQuery] string message)
      {
         return Ok("<h1>API is healthy: " + message + "</h1>");
      }

      [HttpGet]
      public void RedirectUser([FromQuery] string url)
      {
         Response.Redirect(url);
      }
   }
}