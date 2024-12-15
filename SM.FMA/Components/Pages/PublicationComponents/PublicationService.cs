using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;
using SM.FMA.Extensions;

namespace SM.FMA.Components.Pages.PublicationComponents
{
    public class PublicationService : IPublicationService
    {
        private readonly ApplicationDbContext _context;

        public PublicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PublicationDto>> GetAllPublicationsAsync()
        {
            return await _context.Publications
                .Select(p => new PublicationDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    DatePublished = p.DatePublished,
                    Publisher = p.Publisher,
                    PublishingType = p.PublishingType,
                    CoAuthors = p.CoAuthors.ToCommaSeparatedString(),
                    FacultyMemberId = p.FacultyMemberId
                })
                .ToListAsync();
        }

        public async Task<PublicationDto?> GetPublicationByIdAsync(Guid id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication == null) return null;

            return new PublicationDto
            {
                Id = publication.Id,
                Title = publication.Title,
                DatePublished = publication.DatePublished,
                Publisher = publication.Publisher,
                PublishingType = publication.PublishingType,
                CoAuthors = publication.CoAuthors.ToCommaSeparatedString(),
                FacultyMemberId = publication.FacultyMemberId
            };
        }


        public async Task DeletePublicationAsync(Guid id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication != null)
            {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PublicationDto>> GetFacultyMemeberPublicationsAsync(Guid facultyMemberId)
        {
            var publications = await _context.Publications
                .Where(p => p.FacultyMemberId == facultyMemberId)
                .Select(p => new PublicationDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    DatePublished = p.DatePublished,
                    Publisher = p.Publisher,
                    PublishingType = p.PublishingType,
                    CoAuthors = p.CoAuthors.ToCommaSeparatedString(),
                    FacultyMemberId = p.FacultyMemberId
                }).ToListAsync();

            return publications;


        }

        public async Task<PublicationDto> UpsertPublicationAsync(PublicationDto publication)
        {
            var entity = new Publication
            {
                Id = publication.Id,
                Title = publication.Title,
                DatePublished = (DateTime)publication.DatePublished,
                Publisher = publication.Publisher,
                PublishingType = publication.PublishingType,
                CoAuthors = publication.CoAuthors.ToListFromCommaSeparatedString(),
                FacultyMemberId = publication.FacultyMemberId
            };

            var existing = _context.Publications.Find(entity.Id);
            if (existing == null)
            {
                _context.Publications.Add(entity);
            }
            else
            {
                existing.Title = entity.Title;
                existing.DatePublished = entity.DatePublished;
                existing.Publisher = entity.Publisher;
                existing.PublishingType = entity.PublishingType;
                existing.CoAuthors = entity.CoAuthors;
                existing.FacultyMemberId = entity.FacultyMemberId;

            }

            await _context.SaveChangesAsync();

            return new PublicationDto
            {
                Id = entity.Id,
                Title = entity.Title,
                DatePublished = entity.DatePublished,
                Publisher = entity.Publisher,
                PublishingType = entity.PublishingType,
                CoAuthors = entity.CoAuthors.ToCommaSeparatedString(),
                FacultyMemberId = entity.FacultyMemberId
            };
        }
    }
}
