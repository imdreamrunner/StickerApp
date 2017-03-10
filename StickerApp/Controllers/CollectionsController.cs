using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StickerApp.ApiModels;
using StickerApp.Services;

namespace StickerApp.Controllers
{
    [Route("v1/collections")]
    public class CollectionsController
    {

        private readonly ILogger<StickersController> _log;
        private readonly StickerDb _db;

        public CollectionsController(StickerDb database, ILogger<StickersController> logger)
        {
            _db = database;
            _log = logger;
        }


        /// <summary>
        /// Get a list of collections.
        /// </summary>
        /// <remarks>
        /// You can query with an offset and a limit if you want.
        /// </remarks>
        /// <param name="limit">Maximum number of collections.</param>
        /// <param name="offset">Skip certain number of collection, used for pagingnation.</param>
        /// <returns>A task of <see cref="CollectionListResponse"/>.</returns>
        /// <response code="200">Returns a list of stickers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(StickerListResponse), 200)]
        public async Task<CollectionListResponse> GetList([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var collections = await _db.Collections.Skip(offset).Take(limit).ToListAsync();
            _log.LogInformation($"Return {collections.Count} collections to user.");
            var response = new CollectionListResponse { Collections = collections};
            return response;
        }
    }
}