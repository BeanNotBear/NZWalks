using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalks.API.Data;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.CustomActionFilters
{
	public sealed class RegionIdExistsAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var dbContext = (NZWalksDbContext)validationContext.GetService(typeof(NZWalksDbContext));
			var regionId = (Guid)value;

			if (dbContext.Regions.Any(r => r.Id == regionId))
			{
				return ValidationResult.Success;
			}

			return new ValidationResult($"Region with Id {regionId} does not exist.");
		}
	}
}
