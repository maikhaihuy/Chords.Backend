using System;
using System.Linq;
using Chords.DataAccess.Models;
using Chords.WebApi.Helpers;
using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Songs
{
    public class AddSongInput
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public string Slug { get; set; } = null!;

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string GenreId { get; set; } = null!;
        public string[] AuthorIds { get; set; }
    }

    public class AddSongInputType : InputObjectType<AddSongInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddSongInput> descriptor)
        {
            descriptor.Field(b => b.Title).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.Slug).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            
            descriptor.Field(b => b.GenreId).Type<StringType>();
            descriptor.Field(b => b.AuthorIds).Type<ListType<StringType>>();
            
            base.Configure(descriptor);
        }
    }
    
    public class AddSongInputValidator : AbstractValidator<AddSongInput>
    {
        public AddSongInputValidator(PredicateValidators predicateValidators)
        {
            Predicate<string> authorIsExist = predicateValidators.IsExist<Artist>;
            Predicate<string> genreIsExist = predicateValidators.IsExist<Genre>;
            
            RuleFor(input => input.Title)
                .NotEmpty()
                .WithMessage($"Title is required.");
            
            RuleFor(input => input.Slug)
                .NotEmpty()
                .WithMessage($"Slug is required.");
            
            RuleFor(input => input.GenreId)
                .NotEmpty()
                .WithMessage("Genre is required.")
                .Must(genreId => genreIsExist(genreId))
                .WithMessage($"Genre is not found.");
            
            RuleFor(input => input.AuthorIds)
                .Must(singerIds => singerIds.All(_ => authorIsExist(_)))
                .When(input => input.AuthorIds is {Length: > 0})
                .WithMessage($"Author is not found.");
        }
    }
}