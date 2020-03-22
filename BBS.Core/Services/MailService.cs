using BBS.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BBS.Web.Constants;
using BBS.Interfaces;
using System.Net;

namespace BBS.Core.Services
{
	public class MailService : IMailService
	{
		private readonly ILogger _logger;

		public MailService(ILogger logger)
		{
			_logger = logger;
		}

		private async Task<Response> SendEmailTemplate(string to, string templateId, object templateData)
		{
			var client = new SendGridClient(MailApi.ApiKey);
			var sender = new EmailAddress(MailApi.NoReplyMail, MailApi.AccServiceName);
			var recepient = new EmailAddress(to);
			var msg = MailHelper.CreateSingleTemplateEmail(sender, recepient, templateId, templateData);
			var response = await client.SendEmailAsync(msg);

			if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
			{
				_logger.Info($"Sent welcome email to {to}");
			}
			else
			{
				_logger.Warn($"Error when sending mail: {await response.Body.ReadAsStringAsync()}");
			}

			return response;
		}

		public async Task SignupWelcome(string email, string name)
		{
			await SendEmailTemplate(email, MailApi.WelcomeTemplate, new { username=name});
		}

		public async Task LoanRequest(string email, string name, bool accpeted)
		{
			await SendEmailTemplate(email, accpeted?MailApi.LoanAcceptedTemplate:MailApi.LoanDeclinedTemplate, new { username = name });
		}

		public async Task CardRequest(string email, string name, bool accpeted)
		{
			await SendEmailTemplate(email, accpeted?MailApi.CardAcceptedTemplate:MailApi.CardDeclinedTemplate, new { username = name });
		}

		public async Task DeqositAccept(string email, string name)
		{
			await SendEmailTemplate(email, MailApi.DepositAcceptedTemplate, new { username=name});
		}

		public async Task PasswordChange(string email, string name)
		{
			await SendEmailTemplate(email, MailApi.PasswordChangedTemplate, new { username=name});
		}
	}
}
