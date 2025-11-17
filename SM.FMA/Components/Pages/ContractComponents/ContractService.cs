using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;

namespace SM.FMA.Components.Pages.ContractComponents;

public class ContractService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IContractService
{
    public async Task DeleteContract(Guid contractId)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.Contracts.FirstOrDefaultAsync(x => x.Id == contractId);
        if (tmp != null)
        {
            db.Contracts.Remove(tmp);
            db.SaveChanges();
        }
    }

    public async Task<ContractDto> GetFacultyMemberContractAsync(Guid contractId)
    {
        var db = dbContextFactory.CreateDbContext();
        var contract = await db.Contracts.FirstOrDefaultAsync(x => x.Id == contractId);
        if (contract == null)
            throw new InvalidOperationException($"Contract with ID {contractId} not found.");
        return ToDto(contract);
    }

    public async Task<List<ContractDto>> GetFacultyMemberContractsAsync(Guid facultyMemberId)
    {
        try
        {
            var db = dbContextFactory.CreateDbContext();
            var contracts = await db.Contracts.Where(x => x.FacultyMemberId == facultyMemberId).ToListAsync();
            return contracts.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error retrieving contracts for FacultyMemberId: {facultyMemberId}", ex);
        }
    }

    public async Task<ContractDto> UpsertContractAsync(ContractDto contractDto)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.Contracts.FirstOrDefaultAsync(x => x.Id == contractDto.Id);
        if (tmp != null)
        {
            tmp.Date = contractDto.Date;
            tmp.ScanUri = contractDto.ScanUri;
            db.SaveChanges();
            return ToDto(tmp);
        }
        else
        {
            var contract = new Contract
            {
                Id = contractDto.Id == Guid.Empty ? Guid.NewGuid() : contractDto.Id,
                Date = contractDto.Date,
                ScanUri = contractDto.ScanUri,
                FacultyMemberId = contractDto.FacultyMemberId
            };
            db.Contracts.Add(contract);
            db.SaveChanges();
            return ToDto(contract);
        }
    }

    private static ContractDto ToDto(Contract contract)
    {
        return new ContractDto
        {
            Id = contract.Id,
            Date = contract.Date,
            ScanUri = contract.ScanUri,
            FacultyMemberId = contract.FacultyMemberId
        };
    }
}
