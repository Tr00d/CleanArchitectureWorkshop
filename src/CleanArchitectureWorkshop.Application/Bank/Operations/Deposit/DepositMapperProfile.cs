using AutoMapper;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;

public class DepositMapperProfile : Profile
{
    public DepositMapperProfile()
    {
        this.CreateMap<DepositRequest, DepositCommand>()
            .ConstructUsing(request => new DepositCommand(Guid.NewGuid(), request.Amount));
    }
}