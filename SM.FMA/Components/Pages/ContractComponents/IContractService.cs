using SM.FMA.Data.Entities;

namespace SM.FMA.Components.Pages.ContractComponents;

public interface IContractService
{
    Task<List<Contract>> GetFacultyMemberContractsAsync(Guid facultyMemberId);
    Task<Contract> GetFacultyMemberContractAsync(Guid contractId);
    Task<Contract> UpsertContractAsync(Contract contract);
    Task DeleteContract(Guid contractId);
}
