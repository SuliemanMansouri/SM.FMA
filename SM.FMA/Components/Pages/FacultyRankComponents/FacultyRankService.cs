using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;
using SM.FMA.Extensions;

namespace SM.FMA.Components.Pages.FacultyRankComponents
{
    public class FacultyRankService : IFacultyRankService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public FacultyRankService(IDbContextFactory<ApplicationDbContext> dbFactory) => _dbFactory = dbFactory;

        public async Task<IEnumerable<FacultyRankDto>> GetFacultyMemberRanksAsync(Guid facultyMemberId)
        {
            var db = _dbFactory.CreateDbContext();
            return await db.FacultyRanks.Where(r => r.FacultyMemberId == facultyMemberId)
                .Select(r => new FacultyRankDto
                {
                    Id = r.Id,
                    RankName = r.RankName,
                    PromotionDate = r.PromotionDate,
                    CriteriaMet = r.CriteriaMet.ToCommaSeparatedString(),
                    FacultyMemberId = r.FacultyMemberId
                }).ToListAsync();
        }

        public async Task<FacultyRankDto?> GetFacultyRankByIdAsync(Guid id)
        {
            var db = _dbFactory.CreateDbContext();
            var r = await db.FacultyRanks.FindAsync(id);
            return r == null ? null : new FacultyRankDto
            {
                Id = r.Id,
                RankName = r.RankName,
                PromotionDate = r.PromotionDate,
                CriteriaMet = r.CriteriaMet.ToCommaSeparatedString(),
                FacultyMemberId = r.FacultyMemberId
            };
        }

        public async Task DeleteFacultyRankAsync(Guid id)
        {
            var db = _dbFactory.CreateDbContext();
            var r = await db.FacultyRanks.FindAsync(id);
            if (r != null)
            {
                db.FacultyRanks.Remove(r);
                await db.SaveChangesAsync();
            }
        }

        public async Task<FacultyRankDto> UpsertFacultyRankAsync(FacultyRankDto rank)
        {
            var db = _dbFactory.CreateDbContext();
            var entity = await db.FacultyRanks.FirstOrDefaultAsync(x => x.Id == rank.Id);
            if (entity == null)
            {
                entity = new FacultyRank
                {
                    Id = rank.Id == Guid.Empty ? Guid.NewGuid() : rank.Id,
                    RankName = rank.RankName,
                    PromotionDate = rank.PromotionDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                    CriteriaMet = rank.CriteriaMet.ToListFromCommaSeparatedString(),
                    FacultyMemberId = rank.FacultyMemberId,
                    FacultyMember = null!
                };
                db.FacultyRanks.Add(entity);
            }
            else
            {
                entity.RankName = rank.RankName;
                entity.PromotionDate = rank.PromotionDate ?? entity.PromotionDate;
                entity.CriteriaMet = rank.CriteriaMet.ToListFromCommaSeparatedString();
            }
            await db.SaveChangesAsync();
            return new FacultyRankDto
            {
                Id = entity.Id,
                RankName = entity.RankName,
                PromotionDate = entity.PromotionDate,
                CriteriaMet = entity.CriteriaMet.ToCommaSeparatedString(),
                FacultyMemberId = entity.FacultyMemberId
            };
        }
    }
}
