using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string? Isbn { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public int? PublicYear { get; set; }
        public DateTime CreatedAd { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAd { get; set; }
    }
}
