using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;
using SM.FMA.Data.Enums;

namespace SM.FMA.Components.Pages.FacultyMemberComponents
{
    public class FacultyMemberService : IFacultyMemberService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public FacultyMemberService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<FacultyMemberDto> UpsertFacultyMemberAsync(FacultyMemberDto teacher)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var facultyMember = new FacultyMember
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Publications = new List<Publication>(),
                Certificates = new List<Certificate>(),
                FacultyPositions = new List<FacultyPosition>(),
                FacultyRanks = new List<FacultyRank>(),
                FacultyTitles = new List<FacultyTitle>()
            };

            var existingMember = await _dbContext.FacultyMembers.Include(x => x.Publications)
                .FirstOrDefaultAsync(f => f.Email == teacher.Email);

            if (existingMember == null)
            {
                await _dbContext.FacultyMembers.AddAsync(facultyMember);
            }
            else
            {
                existingMember.Name = teacher.Name;
                existingMember.Email = teacher.Email;
                existingMember.PhoneNumber = teacher.PhoneNumber;
            }

            await _dbContext.SaveChangesAsync();

            return new FacultyMemberDto
            {
                Id = facultyMember.Id,
                Name = facultyMember.Name,
                Email = facultyMember.Email,
                PhoneNumber = facultyMember.PhoneNumber,
                PapersCount = facultyMember.Publications.Count(p => p.PublishingType == PublicationType.Paper),
                BooksCount = facultyMember.Publications.Count(p => p.PublishingType == PublicationType.Book),
                PublicationsCount = facultyMember.Publications.Count
            };
        }

        public async Task DeleteFacultyMemberAsync(Guid id)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var facultyMember = await _dbContext.FacultyMembers.FindAsync(id);
            if (facultyMember != null)
            {
                _dbContext.FacultyMembers.Remove(facultyMember);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<FacultyMemberDto> GetFacultyMemberAsync(Guid id)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var facultyMember = await _dbContext.FacultyMembers
                .Include(x => x.Publications)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (facultyMember == null)
                return null;

            return new FacultyMemberDto
            {
                Id = facultyMember.Id,
                Name = facultyMember.Name,
                Email = facultyMember.Email,
                PhoneNumber = facultyMember.PhoneNumber,
                PapersCount = facultyMember.Publications.Count(p => p.PublishingType == PublicationType.Paper),
                BooksCount = facultyMember.Publications.Count(p => p.PublishingType == PublicationType.Book),
                PublicationsCount = facultyMember.Publications.Count
            };
        }

        public async Task<IEnumerable<FacultyMemberDto>> GetFacultyMemberAsync(string Name)
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var facultyMembers = await _dbContext.FacultyMembers
                .Include(x => x.Publications)
                .Where(f => f.Name.Contains(Name))
                .Select(f => new FacultyMemberDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Email = f.Email,
                    PhoneNumber = f.PhoneNumber,
                    PapersCount = f.Publications.Count(p => p.PublishingType == PublicationType.Paper),
                    BooksCount = f.Publications.Count(p => p.PublishingType == PublicationType.Book),
                    PublicationsCount = f.Publications.Count
                })
                .ToListAsync();
            return facultyMembers;
        }

        public async Task<IEnumerable<FacultyMemberDto>> GetFacultyMembersAsync()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            var teachers = await _dbContext.FacultyMembers
                .Include(x => x.Publications)
                .Select(f => new FacultyMemberDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Email = f.Email,
                    PhoneNumber = f.PhoneNumber,
                    PapersCount = f.Publications.Count(p => p.PublishingType == PublicationType.Paper),
                    BooksCount = f.Publications.Count(p => p.PublishingType == PublicationType.Book),
                    PublicationsCount = f.Publications.Count
                })
                .ToListAsync();

            return teachers;
        }
    }
}
