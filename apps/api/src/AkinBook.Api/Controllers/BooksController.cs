using AkinBook.Application.Books.Dtos;
using AkinBook.Application.Common;
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
        public async Task<ActionResult<List<BookResponse>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null
            )
        {
            if(page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if(pageSize > 50) pageSize = 50;

            var query = _db.Books.AsNoTracking().AsQueryable();

            if(!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    EF.Functions.ILike(x.TitleTr, $"%{search}%") ||
                    EF.Functions.ILike(x.TitleEn, $"%{search}%") ||
                    EF.Functions.ILike(x.Author, $"%{search}%")
                );
            }

            var TotalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    TitleTr = x.TitleTr,
                    TitleEn = x.TitleEn,
                    Author = x.Author,
                    DescriptionTr = x.DescriptionTr,
                    DescriptionEn = x.DescriptionEn,
                    Isbn = x.Isbn,
                    CoverUrl = x.CoverUrl,
                    PublishedYear = x.PublishedYear,
                    UserId = x.UserId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();

            var response = new PagedResponse<BookResponse>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = TotalCount
            };

            return Ok(response);
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
                TitleTr = request.TitleTr.Trim(),
                TitleEn = request.TitleEn.Trim(),
                Author = request.Author.Trim(),
                DescriptionTr = request.DescriptionTr?.Trim(),
                DescriptionEn = request.DescriptionEn?.Trim(),
                Isbn = request.Isbn?.Trim(),
                CoverUrl = request.CoverUrl?.Trim(),
                PublishedYear = request.PublishedYear,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            var response = new BookResponse
            {
                Id = book.Id,
                TitleTr = book.TitleTr,
                TitleEn = book.TitleEn,
                Author = book.Author,
                DescriptionTr = book.DescriptionTr,
                DescriptionEn = book.DescriptionEn,
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
                    TitleTr = x.TitleTr,
                    TitleEn = x.TitleEn,
                    Author = x.Author,
                    DescriptionTr = x.DescriptionTr,
                    DescriptionEn = x.DescriptionEn,
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

            book.TitleTr = request.TitleTr.Trim();
            book.TitleEn = request.TitleEn.Trim();
            book.Author = request.Author.Trim();
            book.DescriptionTr = request.DescriptionTr?.Trim();
            book.DescriptionEn = request.DescriptionEn?.Trim();
            book.Isbn = request.Isbn?.Trim();
            book.CoverUrl = request.CoverUrl?.Trim();
            book.PublishedYear = request.PublishedYear;
            book.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            var response = new BookResponse
            {
                Id = book.Id,
                TitleTr = book.TitleTr,
                TitleEn = book.TitleEn,
                Author = book.Author,
                DescriptionTr = book.DescriptionTr,
                DescriptionEn = book.DescriptionEn,
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
