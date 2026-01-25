using AkinBook.Application.Books.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AkinBook.Application.Books.Validators
{
    public sealed class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required.")
                .MaximumLength(200);

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1450, DateTime.UtcNow.Year)
                .When(x => x.PublishedYear.HasValue);

            RuleFor(x => x.Isbn)
               .MaximumLength(32)
               .When(x => !string.IsNullOrWhiteSpace(x.Isbn));

            RuleFor(x => x.CoverUrl)
               .MaximumLength(500)
               .When(x => !string.IsNullOrWhiteSpace(x.CoverUrl));
        }
    }
}
