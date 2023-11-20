using MailKit;
using Microsoft.AspNetCore.Http;
using SupportMailer.Services;
using Microsoft.AspNetCore.Mvc;
using SupportMailer.Models;
using IMailService = SupportMailer.Services.IMailService;

namespace SupportMailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult>SendMailAsync(MailData mailData) {
            bool result = await _mail.SendAsync(mailData, new CancellationToken());
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has been sent");
            }
            else {
                return StatusCode(StatusCodes.Status500InternalServerError, "An Error Occured.");
            }
        }
    }
}
