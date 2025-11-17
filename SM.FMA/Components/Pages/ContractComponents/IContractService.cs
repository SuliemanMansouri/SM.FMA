using SM.FMA.Components.Pages.ContractComponents;

namespace SM.FMA.Components.Pages.ContractComponents;

public interface IContractService
{
    Task<List<ContractDto>> GetFacultyMemberContractsAsync(Guid facultyMemberId);
    Task<ContractDto> GetFacultyMemberContractAsync(Guid contractId);
    Task<ContractDto> UpsertContractAsync(ContractDto contractDto);
    Task DeleteContract(Guid contractId);
}
