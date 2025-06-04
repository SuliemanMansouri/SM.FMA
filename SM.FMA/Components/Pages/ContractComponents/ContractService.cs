using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;

namespace SM.FMA.Components.Pages.ContractComponents;

public class ContractService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : IContractService
{
    public async Task DeleteContract(Guid contractId)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.Contracts.FirstOrDefaultAsync(x=>x.Id == contractId);
        if(tmp != null)
        {
            db.Contracts.Remove(tmp);
            db.SaveChanges();
        }
    }

    public async Task<Contract> GetFacultyMemberContractAsync(Guid contractId)
    {
        var db = dbContextFactory.CreateDbContext();
        var contracts = await db.Contracts.FirstOrDefaultAsync(x=>x.Id == contractId);
        return contracts;
    }

    public async Task<List<Contract>> GetFacultyMemberContractsAsync(Guid facultyMemberId)
    {
        var db = dbContextFactory.CreateDbContext();
        var contracts = await db.Contracts.Where(x=>x.FacultyMemberId == facultyMemberId).ToListAsync();
        return contracts;
    }

    public async Task<Contract> UpsertContractAsync(Contract contract)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.Contracts.FirstOrDefaultAsync(x=>x.Id==contract.Id);
        if (tmp != null) 
        {
            tmp.Date = contract.Date;
            tmp.ScanUri = contract.ScanUri;
            db.SaveChanges();
            return tmp;
        }
        else
        {
            db.Contracts.Add(contract);
            db.SaveChanges();
            return contract;
        }

    }
}
