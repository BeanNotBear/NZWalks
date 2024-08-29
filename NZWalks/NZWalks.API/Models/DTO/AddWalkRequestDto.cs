using NZWalks.API.CustomActionFilters;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
	public class AddWalkRequestDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(1000)]
		public string Description { get; set; }

		[Required]
		[Range(0, 50)]
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }

		[Required]
		[RegionIdExists]
		public Guid RegionId { get; set; }

		[Required]
		[DifficultyIdExists]
		public Guid DifficultyId { get; set; }
	}
}
