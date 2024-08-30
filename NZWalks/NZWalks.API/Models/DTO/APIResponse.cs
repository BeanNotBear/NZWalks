namespace NZWalks.API.Models.DTO
{
	public class APIResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; } = null;
		public object Data { get; set; }

		public APIResponse(bool success, string message, object data)
		{
			Success = success;
			Message = message;
			Data = data;
		}
	}
}
