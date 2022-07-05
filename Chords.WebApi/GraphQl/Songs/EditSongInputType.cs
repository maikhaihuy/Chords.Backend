using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chords.DataAccess.Models;
using Chords.WebApi.Helpers;
using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Songs
{
    public class EditSongInput
    {
        [Required]
        public string Id { get; set; } = null!;
        public string Title { get; set; }
        public string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        
        public string GenreId { get; set; }
        public string[] AuthorIds { get; set; }
    }

    public class EditSongInputType : InputObjectType<EditSongInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EditSongInput> descriptor)
        {
            descriptor.Field(b => b.Id).Type<IdType>();

            descriptor.Field(b => b.Title).Type<StringType>();
            descriptor.Field(b => b.Description).Type<StringType>();
            descriptor.Field(b => b.SeoTitle).Type<StringType>();
            descriptor.Field(b => b.SeoDescription).Type<StringType>();
            descriptor.Field(b => b.SeoKeywords).Type<StringType>();
            
            descriptor.Field(b => b.GenreId).Type<StringType>();
            descriptor.Field(b => b.AuthorIds).Type<ListType<StringType>>();

            base.Configure(descriptor);
        }
    }
    
    public class EditSongInputValidator : AbstractValidator<EditSongInput>
    {
        public EditSongInputValidator(PredicateValidators predicateValidators)
        {
            Predicate<string[]> authorIsExist = predicateValidators.IsExist<Artist>;
            Predicate<string> genreIsExist = predicateValidators.IsExist<Genre>;
            Predicate<string> songIsExist = predicateValidators.IsExist<Song>;
            
            RuleFor(input => input.Id)
                .NotEmpty()
                .WithMessage($"Id is required.")
                .Must(id => songIsExist(id))
                .WithMessage($"Song is not found.");
            
            RuleFor(input => input.GenreId)
                .Must(genreId => genreIsExist(genreId))
                .When(input => !string.IsNullOrEmpty(input.GenreId))
                .WithMessage($"Genre is not found.");
            
            RuleFor(input => input.AuthorIds)
                .Must(authorIds => authorIsExist(authorIds))
                .When(input => input.AuthorIds is {Length: > 0})
                .WithMessage($"Author is not found.");
        }
    }
}