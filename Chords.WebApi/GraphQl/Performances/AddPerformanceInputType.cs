using System;
using System.Linq;
using Chords.DataAccess.Models;
using Chords.WebApi.Helpers;
using FluentValidation;
using HotChocolate.Types;

namespace Chords.WebApi.GraphQl.Performances
{
    public class AddPerformanceInput
    {
        public string KeyTone { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string SongId { get; set; } = null!;
        public string[] SingerIds { get; set; }
    }
    
    public class AddPerformanceInputType : InputObjectType<AddPerformanceInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddPerformanceInput> descriptor)
        {
            descriptor.Field(b => b.KeyTone).Type<StringType>();
            descriptor.Field(b => b.Url).Type<StringType>();
            descriptor.Field(b => b.SongId).Type<StringType>();
            descriptor.Field(b => b.SingerIds).Type<ListType<StringType>>();

            base.Configure(descriptor);
        }
    }
    
    public class AddPerformanceInputValidator : AbstractValidator<AddPerformanceInput>
    {
        public AddPerformanceInputValidator(PredicateValidators predicateValidators)
        {
            Predicate<string> songIsExist = predicateValidators.IsExist<Song>;
            Predicate<string> singerIsExist = predicateValidators.IsExist<Artist>;
            
            RuleFor(input => input.KeyTone)
                .NotEmpty()
                .WithMessage("Keytone is required.");
            RuleFor(input => input.Url)
                .NotEmpty()
                .WithMessage("Url is required.");
            RuleFor(input => input.SongId)
                .NotEmpty()
                .WithMessage("Song is required.")
                .Must(songId => songIsExist(songId))
                .WithMessage($"Song not found.");
            RuleFor(input => input.SingerIds)
                .Must(singerIds => singerIds.All(_ => singerIsExist(_)))
                .When(input => input.SingerIds is {Length: > 0})
                .WithMessage($"Singer not found.");
        }
    }
}