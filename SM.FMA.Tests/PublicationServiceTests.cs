using Microsoft.EntityFrameworkCore;
using Moq;
using SM.FMA.Components.Pages.PublicationComponents;
using SM.FMA.Data;
using SM.FMA.Data.Entities;
using SM.FMA.Data.Enums;

namespace SM.FMA.Tests
{
    public class PublicationServiceTests
    {
        private IDbContextFactory<ApplicationDbContext> GetDbContextFactory(DbContextOptions<ApplicationDbContext> options)
        {
            var mockFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(new ApplicationDbContext(options));
            return mockFactory.Object;
        }

        private static DbContextOptions<ApplicationDbContext> GetNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllPublicationsAsync_ShouldReturnAllPublications()
        {
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            // Arrange
            var publications = new List<Publication>
            {
                new Publication
                {
                    Id = Guid.NewGuid(),
                    Title = "Publication 1",
                    DatePublished = DateTime.Now,
                    Publisher = "Publisher 1",
                    PublishingType = PublicationType.Paper,
                    CoAuthors = new List<string> { "Author 1" },
                    FacultyMemberId = Guid.NewGuid()
                },
                new Publication
                {
                    Id = Guid.NewGuid(),
                    Title = "Publication 2",
                    DatePublished = DateTime.Now,
                    Publisher = "Publisher 2",
                    PublishingType = PublicationType.Book,
                    CoAuthors = new List<string> { "Author 2" },
                    FacultyMemberId = Guid.NewGuid()
                }
            };

            var context = factory.CreateDbContext();

            context.Publications.AddRange(publications);
            await context.SaveChangesAsync();


            var service = new PublicationService(factory);

            // Act
            var result = await service.GetAllPublicationsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPublicationByIdAsync_ShouldReturnPublication_WhenPublicationExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var publicationId = Guid.NewGuid();
            var publication = new Publication
            {
                Id = publicationId,
                Title = "Publication 1",
                DatePublished = DateTime.Now,
                Publisher = "Publisher 1",
                PublishingType = PublicationType.Paper,
                CoAuthors = new List<string> { "Author 1" },
                FacultyMemberId = Guid.NewGuid()
            };

            var context = factory.CreateDbContext();
            context.Publications.Add(publication);
            await context.SaveChangesAsync();


            var service = new PublicationService(factory);

            // Act
            var result = await service.GetPublicationByIdAsync(publicationId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(publication.Title, result.Title);
        }

        [Fact]
        public async Task GetPublicationByIdAsync_ShouldReturnNull_WhenPublicationDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var publicationId = Guid.NewGuid();
            var service = new PublicationService(factory);

            // Act
            var result = await service.GetPublicationByIdAsync(publicationId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeletePublicationAsync_ShouldRemovePublication_WhenPublicationExists()
        {
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            // Arrange
            var publicationId = Guid.NewGuid();
            var publication = new Publication
            {
                Id = publicationId,
                Title = "Publication 1",
                DatePublished = DateTime.Now,
                Publisher = "Publisher 1",
                PublishingType = PublicationType.Paper,
                CoAuthors = new List<string> { "Author 1" },
                FacultyMemberId = Guid.NewGuid()
            };

            var cf = factory.CreateDbContext();
            cf.Publications.Add(publication);
            await cf.SaveChangesAsync();


            var service = new PublicationService(factory);

            // Act
            await service.DeletePublicationAsync(publicationId);

            // Assert
            using var context = factory.CreateDbContext();
            var deletedPublication = await context.Publications.FindAsync(publicationId);
            Assert.Null(deletedPublication);
        }

        [Fact]
        public async Task GetFacultyMemeberPublicationsAsync_ShouldReturnPublications_WhenPublicationsExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var facultyMemberId = Guid.NewGuid();
            var publications = new List<Publication>
            {
                new Publication
                {
                    Id = Guid.NewGuid(),
                    Title = "Publication 1",
                    DatePublished = DateTime.Now,
                    Publisher = "Publisher 1",
                    PublishingType = PublicationType.Paper,
                    CoAuthors = new List<string> { "Author 1" },
                    FacultyMemberId = facultyMemberId
                },
                new Publication
                {
                    Id = Guid.NewGuid(),
                    Title = "Publication 2",
                    DatePublished = DateTime.Now,
                    Publisher = "Publisher 2",
                    PublishingType = PublicationType.Book,
                    CoAuthors = new List<string> { "Author 2" },
                    FacultyMemberId = facultyMemberId
                }
            };

            var context = factory.CreateDbContext();

            context.Publications.AddRange(publications);
            await context.SaveChangesAsync();


            var service = new PublicationService(factory);

            // Act
            var result = await service.GetFacultyMemeberPublicationsAsync(facultyMemberId);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpsertPublicationAsync_ShouldAddNewPublication_WhenPublicationDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var publicationDto = new PublicationDto
            {
                Id = Guid.NewGuid(),
                Title = "Publication 1",
                DatePublished = DateTime.Now,
                Publisher = "Publisher 1",
                PublishingType = PublicationType.Paper,
                CoAuthors = "Author 1",
                FacultyMemberId = Guid.NewGuid()
            };

            var service = new PublicationService(factory);

            // Act
            var result = await service.UpsertPublicationAsync(publicationDto);

            // Assert
            var context = factory.CreateDbContext();
            var addedPublication = await context.Publications.FindAsync(result.Id);
            Assert.NotNull(addedPublication);
            Assert.Equal(publicationDto.Title, addedPublication.Title);
        }

        [Fact]
        public async Task UpsertPublicationAsync_ShouldUpdateExistingPublication_WhenPublicationExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var publicationId = Guid.NewGuid();
            var existingPublication = new Publication
            {
                Id = publicationId,
                Title = "Publication 1",
                DatePublished = DateTime.Now,
                Publisher = "Publisher 1",
                PublishingType = PublicationType.Paper,
                CoAuthors = new List<string> { "Author 1" },
                FacultyMemberId = Guid.NewGuid()
            };

            var cf = factory.CreateDbContext();

            cf.Publications.Add(existingPublication);
            await cf.SaveChangesAsync();


            var publicationDto = new PublicationDto
            {
                Id = publicationId,
                Title = "Updated Publication",
                DatePublished = DateTime.Now,
                Publisher = "Updated Publisher",
                PublishingType = PublicationType.Book,
                CoAuthors = "Updated Author",
                FacultyMemberId = existingPublication.FacultyMemberId
            };

            var service = new PublicationService(factory);

            // Act
            var result = await service.UpsertPublicationAsync(publicationDto);

            // Assert
            using var context = factory.CreateDbContext();
            var updatedPublication = await context.Publications.FindAsync(result.Id);
            Assert.NotNull(updatedPublication);
            Assert.Equal(publicationDto.Title, updatedPublication.Title);
            Assert.Equal(publicationDto.Publisher, updatedPublication.Publisher);
        }
    }
}
