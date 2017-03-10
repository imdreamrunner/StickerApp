using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StickerApp.ApiModels;
using StickerApp.Misc;
using StickerApp.Models;
using StickerApp.Services;

namespace StickerApp.Controllers
{
    [Route("v1/stickers")]
    public class StickersController : AppController
    {
        private readonly ILogger<StickersController> _log;
        private readonly StickerDb _db;

        public StickersController(StickerDb database, ILogger<StickersController> logger)
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

        /// <summary>
        /// Get a Single sticker.
        /// </summary>
        /// <param name="id">Reqiested sticker ID.</param>
        /// <returns>The requested sticker.</returns>
        [HttpGet("{id}")]
        public async Task<SingleStickerResponse> Get(int id)
        {
            var sticker = await _db.Stickers.Where(s => s.StickerId == id).FirstOrDefaultAsync();
            if (sticker == null)
            {
                throw new StickerAppException("StickerNotFound");
            }
            return new SingleStickerResponse(sticker);
        }

        /// <summary>
        /// Create new sticker.
        /// </summary>
        /// <response code="200">Return the created sticker.</response>
        [HttpPost]
        [CheckToken]
        [ProducesResponseType(typeof(SingleStickerResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<ApiResponse> Post([FromBody] StickerAddRequest stickerData)
        {
            var stickerModel = new Sticker
            {
                Name = stickerData.Name,
                Description = stickerData.Description,
                StickerTypeString = stickerData.Type,
                Tags = stickerData.Tags,
                Author = stickerData.Author
            };
            TryValidateModel(stickerModel);
            if (!ModelState.IsValid)
            {
                throw GenerateModelStateException("InvalidSticker");
            }
            _db.Stickers.Add(stickerModel);
            await _db.SaveChangesAsync();
            return new SingleStickerResponse(stickerModel);
        }

        /// <summary>
        /// Update a sticker
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stickerData"></param>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] StickerAddRequest stickerData)
        {
            var stickerModel = await _db.Stickers.Where(s => s.StickerId == id).FirstOrDefaultAsync();
            if (stickerModel == null)
            {
                throw new StickerAppException("StickerNotFound");
            }
            stickerModel.Name = stickerData.Name;
            stickerModel.Description = stickerData.Description;
            stickerModel.StickerTypeString = stickerData.Type;
            stickerModel.Tags = stickerData.Tags;
            stickerModel.Author = stickerData.Author;
            _db.Stickers.Update(stickerModel);
            await _db.SaveChangesAsync();
            return new SingleStickerResponse(stickerModel);
        }

        /// <summary>
        /// Delete a sticker.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [CheckToken]
        public async Task<ApiResponse> Delete(int id)
        {
            var sticker = await _db.Stickers.Where(s => s.StickerId == id).FirstOrDefaultAsync();
            if (sticker == null)
            {
                throw new StickerAppException("StickerNotFound");
            }
            _db.Stickers.Remove(sticker);
            await _db.SaveChangesAsync();
            return new SuccessResponse();
        }
    }
}