using AutoMapper;
using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public class WithdrawMapperProfile : Profile
{
    public WithdrawMapperProfile()
    {
        this.CreateMap<WithdrawRequest, WithdrawCommand>()
            .ConstructUsing(request => new WithdrawCommand(Guid.NewGuid(), Amount.FromValue(request.Amount)));
    }
}