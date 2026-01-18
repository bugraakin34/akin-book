using AkinBook.Application.Books.Dtos;
using AkinBook.Domain.Entities;
using AkinBook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AkinBook.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public sealed class BooksController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BooksController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookResponse>>> GetAll()
        {
            var items = await _db.Books
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description,
                    PublishedYear = x.PublishedYear,
                    UserId = x.UserId,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();

            return Ok(items);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BookResponse>> Create(CreateBookRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = request.Title.Trim(),
                Author = request.Author.Trim(),
                Description = request.Description?.Trim(),
                PublishedYear = request.PublishedYear,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            var response = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublishedYear = book.PublishedYear,
                UserId = book.UserId,
                CreatedAt = book.CreatedAt
            };

            return CreatedAtAction(nameof(GetAll), new { id = book.Id }, response);
        }
    }
}
