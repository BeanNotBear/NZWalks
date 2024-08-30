namespace NZWalks.API.Models.DTO
{
	public class QueryParameters
	{
		private const int MAX_PAGE_SIZE = 50;
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get => pageSize;
			set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
		}

		private int pageSize = 10;

		// Filtering params
		// Filter by name
		public string? Name { get; set; } = null;

		// Filter by range of length
		public double? MinLength { get; set; } = 0;
		public double? MaxLength { get; set; } = 100;

		// Sorting
		public string? SortBy { get; set; } = null;
		public bool IsAscending { get; set; } = true;

    }
}