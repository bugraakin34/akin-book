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

        [Authorize(Roles = "Admin")]
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookResponse>> GetById(Guid id)
        {
            var book = await _db.Books
                .Where(x => x.Id == id)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description,
                    Isbn = x.Isbn,
                    CoverUrl = x.CoverUrl,
                    PublishedYear = x.PublishedYear,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .SingleOrDefaultAsync();

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<BookResponse>> Update(Guid id, UpdateBookRequest request)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var book = await _db.Books.SingleOrDefaultAsync(x => x.Id == id);
            if (book is null)
                return NotFound();

            if (book.UserId != userId)
                return Forbid();

            book.Title = request.Title.Trim();
            book.Author = request.Author.Trim();
            book.Description = request.Description?.Trim();
            book.Isbn = request.Isbn?.Trim();
            book.CoverUrl = request.CoverUrl?.Trim();
            book.PublishedYear = request.PublishedYear;
            book.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            var response = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Isbn = book.Isbn,
                CoverUrl = book.CoverUrl,
                PublishedYear = book.PublishedYear,
                UserId = book.UserId,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt
            };

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var book = await _db.Books.SingleOrDefaultAsync(x => x.Id == id);
            if (book is null)
                return NotFound();

            if (book.UserId != userId)
                return Forbid();

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
