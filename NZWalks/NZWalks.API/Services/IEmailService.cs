﻿namespace NZWalks.API.Services
{
	public interface IEmailService
	{
		Task SendEmailAsync(string email, string subject, string htmlMessage);
	}
}
