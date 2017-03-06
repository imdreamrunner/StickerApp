using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StickerApp.Responses;
using StickerApp.Services;

namespace StickerApp.Controllers
{
    [Route("stickers")]
    public class StickersController : Controller
    {
        private readonly ILogger<StickersController> _log;
        private readonly Database _db;

        public StickersController(Database database, ILogger<StickersController> logger)
        {
            _db = database;
            _log = logger;
        }

        // GET api/stickers
        [HttpGet]
        [ServiceFilter(typeof(TokenCheckingFilter))]
        public async Task<JsonResult> GetList([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var stickers = await _db.Stickers.Skip(offset).Take(limit).ToListAsync();
            _log.LogInformation($"Return {stickers.Count} stickers to user.");
            var response = new StickerListResponse() { Stickers = stickers};
            var jsonResponse = Json(response);
            return jsonResponse;
        }

        // GET api/stickers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stickers
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/stickers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/stickers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}