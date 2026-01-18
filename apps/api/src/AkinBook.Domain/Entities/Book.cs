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

        public int? PublishedYear { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
