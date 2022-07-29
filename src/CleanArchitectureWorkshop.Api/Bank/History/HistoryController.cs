using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWorkshop.Api.Bank.History
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public HistoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("statements")]
        public async Task<IActionResult> GetStatementsAsync()
        {
            var response = await this.mediator.Send(new GetStatementsQuery());
            return this.Ok(response);
        }
    }
}