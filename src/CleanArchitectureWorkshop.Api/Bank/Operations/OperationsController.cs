using AutoMapper;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using CleanArchitectureWorkshop.Application.Bank.Operations.GetBalance;
using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWorkshop.Api.Bank.Operations
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public OperationsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawAsync([FromBody] WithdrawRequest request)
        {
            var command = this.mapper.Map<WithdrawCommand>(request);
            await this.mediator.Send(command);
            return this.Ok(command.Id);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] DepositRequest request)
        {
            var command = this.mapper.Map<DepositCommand>(request);
            await this.mediator.Send(command);
            return this.Ok(command.Id);
        }

        [HttpGet("balance")]
        public async Task<OkObjectResult> GetBalance()
        {
            return Ok(await this.mediator.Send(new GetBalanceQuery()));
        }
    }
}