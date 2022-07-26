using CleanArchitectureWorkshop.Application.Bank.GetStatements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWorkshop.Api.Bank
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("statements")]
        public async Task<IActionResult> GetStatementsAsync()
        {
            var response = await this.mediator.Send(new GetStatementsQuery());
            return this.Ok(response);
        }

        [HttpPost("deposit")]
        public Task<IActionResult> WithdrawAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPost("withdraw")]
        public Task<IActionResult> DepositAsync()
        {
            throw new NotImplementedException();
        }
    }
}