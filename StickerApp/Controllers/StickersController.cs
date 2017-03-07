using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StickerApp.Misc;
using StickerApp.Responses;
using StickerApp.Services;

namespace StickerApp.Controllers
{
    [Route("v1/stickers")]
    public class StickersController : Controller
    {
        private readonly ILogger<StickersController> _log;
        private readonly Database _db;

        public StickersController(Database database, ILogger<StickersController> logger)
        {
            _db = database;
            _log = logger;
        }

        /// <summary>
        /// Get a list of stickers.
        /// </summary>
        /// <remarks>
        /// You can query with an offset and a limit if you want.
        /// </remarks>
        /// <param name="limit">Maximum number of stickers.</param>
        /// <param name="offset">Skip certain number of stickers, used for pagingnation.</param>
        /// <returns>A task of <see cref="StickerListResponse"/>.</returns>
        /// <response code="200">Returns a list of stickers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(StickerListResponse), 200)]
        public async Task<StickerListResponse> GetList([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var stickers = await _db.Stickers.Skip(offset).Take(limit).ToListAsync();
            _log.LogInformation($"Return {stickers.Count} stickers to user.");
            var response = new StickerListResponse() { Stickers = stickers};
            return response;
        }

        // GET api/stickers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stickers
        [HttpPost]
        [CheckToken]
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