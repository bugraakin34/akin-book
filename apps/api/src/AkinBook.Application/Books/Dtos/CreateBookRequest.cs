using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Books.Dtos
{
    public sealed class CreateBookRequest
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string? Description { get; set; }
        public string? Isbn { get; set; }
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }
    }
}
