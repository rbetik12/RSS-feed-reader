using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using rss_feed.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using rss_feed.Services;
using rss_feed.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace rss_feed.Controllers {
    [ApiController]
    public class SettingsController : Controller {
        private readonly IConfigLoaderService _configLoaderService;
        private readonly IConfigSaverService _configSaverService;

        public SettingsController(IConfigLoaderService configLoaderService, IConfigSaverService configSaverService) {
            _configLoaderService = configLoaderService;
            _configSaverService = configSaverService;
        }

        [HttpGet("api/settings")]
        public JsonResult GetSettings() {
            Config config = _configLoaderService.GetConfiguration();
            return Json(config);
        }

        [HttpPut("api/settings")]
        public async Task<IActionResult> SetSettings() {
            try {
                await _configSaverService.SaveConfig(Request.Body);
            }
            catch (JsonException) {
                return BadRequest(new ErrorResponse("Bad JSON received"));
            }

            return StatusCode(200);
        }
    }
};