using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Books.Dtos
{
    public sealed class UpdateBookRequest
    {
        public string TitleTr { get; set; } = default!;
        public string TitleEn { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string? DescriptionTr { get; set; }
        public string? DescriptionEn { get; set; }
        public string? Isbn { get; set; }
        public string? CoverUrl { get; set; }
        public int? PublishedYear { get; set; }
    }
}
