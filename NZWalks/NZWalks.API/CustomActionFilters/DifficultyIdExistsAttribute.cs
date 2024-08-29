using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.CustomActionFilters
{
	public class DifficultyIdExistsAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var dbContext = (NZWalksDbContext)validationContext.GetService(typeof(NZWalksDbContext));
			
			var difficultyId = (Guid)value;

			if (dbContext.Difficulties.Any(d => d.Id == difficultyId))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult($"Difficulty with Id {difficultyId} does not exist.");
		}
	}
}
