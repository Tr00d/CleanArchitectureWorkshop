using AutoMapper;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public class WithdrawMapperProfile : Profile
{
    public WithdrawMapperProfile()
    {
        this.CreateMap<WithdrawRequest, WithdrawCommand>()
            .ConstructUsing(request => new WithdrawCommand(Guid.NewGuid(), request.Amount));
    }
}