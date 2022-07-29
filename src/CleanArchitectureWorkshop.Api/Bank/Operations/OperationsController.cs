using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWorkshop.Api.Bank.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public OperationsController(IMediator mediator)
        {
            this.mediator = mediator;
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