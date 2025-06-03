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
                NameAr = teacher.NameAr,
                NameEn = teacher.NameEn,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                Sex = teacher.Sex,
                DOB = teacher.DOB,
                POB = teacher.POB,
                NID = teacher.NID,
                RegistrationNumber = teacher.RegistrationNumber,
                FinancialNumber = teacher.FinancialNumber,
                IBAN = teacher.IBAN,
                SSN = teacher.SSN,
                BranchName = teacher.BranchName,
                BankName = teacher.BankName,
                EmployeeId = teacher.EmployeeId,
                AcademicId = teacher.AcademicId,
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
                existingMember.NameAr = teacher.NameAr;
                existingMember.Email = teacher.Email;
                existingMember.PhoneNumber = teacher.PhoneNumber;
                existingMember.Sex = teacher.Sex;
                existingMember.DOB = teacher.DOB;
                existingMember.POB = teacher.POB;
                existingMember.NID = teacher.NID;
                existingMember.RegistrationNumber = teacher.RegistrationNumber;
                existingMember.FinancialNumber = teacher.FinancialNumber;
                existingMember.IBAN = teacher.IBAN;
                existingMember.SSN = teacher.SSN;
                existingMember.BranchName = teacher.BranchName;
                existingMember.BankName = teacher.BankName;
                existingMember.EmployeeId = teacher.EmployeeId;

            }

            await _dbContext.SaveChangesAsync();

            return new FacultyMemberDto
            {
                Id = facultyMember.Id,
                NameAr = facultyMember.NameAr,
                NameEn = facultyMember.NameEn,
                Email = facultyMember.Email,
                PhoneNumber = facultyMember.PhoneNumber,
                Sex = facultyMember.Sex,
                DOB = facultyMember.DOB,
                POB = facultyMember.POB,
                NID = facultyMember.NID,
                RegistrationNumber = facultyMember.RegistrationNumber,
                FinancialNumber = facultyMember.FinancialNumber,
                IBAN = facultyMember.IBAN,
                SSN = facultyMember.SSN,
                BranchName = facultyMember.BranchName,
                BankName = facultyMember.BankName,
                EmployeeId = facultyMember.EmployeeId,
                AcademicId = facultyMember.AcademicId,
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
                NameAr = facultyMember.NameAr,
                NameEn = facultyMember.NameEn,
                Email = facultyMember.Email,
                PhoneNumber = facultyMember.PhoneNumber,
                Sex = facultyMember.Sex,
                DOB = facultyMember.DOB,
                POB = facultyMember.POB,
                NID = facultyMember.NID,
                RegistrationNumber = facultyMember.RegistrationNumber,
                FinancialNumber = facultyMember.FinancialNumber,
                IBAN = facultyMember.IBAN,
                SSN = facultyMember.SSN,
                BranchName = facultyMember.BranchName,
                BankName = facultyMember.BankName,
                EmployeeId = facultyMember.EmployeeId,
                AcademicId = facultyMember.AcademicId,
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
                .Where(f => f.NameAr.Contains(Name) || f.NameEn.Contains(Name))
                .Select(f => new FacultyMemberDto
                {
                    Id = f.Id,
                    NameAr = f.NameAr,
                    NameEn = f.NameEn,
                    Email = f.Email,
                    PhoneNumber = f.PhoneNumber,
                    Sex = f.Sex,
                    DOB = f.DOB,
                    POB = f.POB,
                    NID = f.NID,
                    RegistrationNumber = f.RegistrationNumber,
                    FinancialNumber = f.FinancialNumber,
                    IBAN = f.IBAN,
                    SSN = f.SSN,
                    BranchName = f.BranchName,
                    BankName = f.BankName,
                    EmployeeId = f.EmployeeId,
                    AcademicId = f.AcademicId,
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
                    NameAr = f.NameAr,
                    NameEn = f.NameEn,
                    Email = f.Email,
                    PhoneNumber = f.PhoneNumber,
                    Sex = f.Sex,
                    DOB = f.DOB,
                    POB = f.POB,
                    NID = f.NID,
                    RegistrationNumber = f.RegistrationNumber,
                    FinancialNumber = f.FinancialNumber,
                    IBAN = f.IBAN,
                    SSN = f.SSN,
                    BranchName = f.BranchName,
                    BankName = f.BankName,
                    EmployeeId = f.EmployeeId,
                    AcademicId = f.AcademicId,
                    PapersCount = f.Publications.Count(p => p.PublishingType == PublicationType.Paper),
                    BooksCount = f.Publications.Count(p => p.PublishingType == PublicationType.Book),
                    PublicationsCount = f.Publications.Count
                })
                .ToListAsync();

            return teachers;
        }
    }
}
