using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Books.Dtos
{
    public sealed class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string? Description { get; set; }
        public int? PublishedYear { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
