using Microsoft.EntityFrameworkCore;
using Moq;
using SM.FMA.Components.Pages.FacultyMemberComponents;
using SM.FMA.Data;
using SM.FMA.Data.Entities;

namespace SM.FMA.Tests
{
    public class FacultyMemberServiceTests
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
        public async Task UpsertFacultyMemberAsync_ShouldAddNewFacultyMember_WhenFacultyMemberDoesNotExist()
        {
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);

            // Arrange
            var facultyMemberDto = new FacultyMemberDto
            {
                Id = Guid.NewGuid(),
                NameAr = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            var service = new FacultyMemberService(factory);

            // Act
            var result = await service.UpsertFacultyMemberAsync(facultyMemberDto);

            // Assert
            using var context = new ApplicationDbContext(options);
            var addedMember = await context.FacultyMembers.FindAsync(result.Id);
            Assert.NotNull(addedMember);
            Assert.Equal(facultyMemberDto.NameAr, addedMember.NameAr);
        }

        [Fact]
        public async Task UpsertFacultyMemberAsync_ShouldUpdateExistingFacultyMember_WhenFacultyMemberExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new FacultyMemberService(factory);

            var existingFacultyMember = new FacultyMember
            {
                Id = Guid.NewGuid(),
                NameAr = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            var cf = factory.CreateDbContext();
            cf.FacultyMembers.Add(existingFacultyMember);
            await cf.SaveChangesAsync();



            var facultyMemberDto = new FacultyMemberDto
            {
                Id = existingFacultyMember.Id,
                NameAr = "Jane Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "0987654321"
            };

            // Act

            var result = await service.UpsertFacultyMemberAsync(facultyMemberDto);

            // Assert
            var context = factory.CreateDbContext();
            var updatedMember = await context.FacultyMembers.FindAsync(result.Id);
            Assert.NotNull(updatedMember);
            Assert.Equal(facultyMemberDto.NameAr, updatedMember.NameAr);
            Assert.Equal(facultyMemberDto.PhoneNumber, updatedMember.PhoneNumber);
        }

        [Fact]
        public async Task DeleteFacultyMemberAsync_ShouldRemoveFacultyMember_WhenFacultyMemberExists()
        {

            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            // Arrange
            var facultyMemberId = Guid.NewGuid();
            var existingFacultyMember = new FacultyMember
            {
                Id = facultyMemberId,
                NameAr = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            var cf = factory.CreateDbContext();
            cf.FacultyMembers.Add(existingFacultyMember);
            await cf.SaveChangesAsync();

            var service = new FacultyMemberService(factory);

            // Act
            await service.DeleteFacultyMemberAsync(facultyMemberId);

            // Assert
            
            var context = factory.CreateDbContext();
            var deletedMember = await context.FacultyMembers.FindAsync(facultyMemberId);
            Assert.Null(deletedMember);
        }

        [Fact]
        public async Task GetFacultyMemberAsync_ShouldReturnFacultyMember_WhenFacultyMemberExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new FacultyMemberService(factory);
            var newFacultyMember = new FacultyMemberDto
            {
                NameAr = "John Doe",
                PhoneNumber = "123456789",
                Email = "author@company.com"
            };
            var savedFacultyMember = await service.UpsertFacultyMemberAsync(newFacultyMember);

            // Act
            var fetchedFacultyMember = await service.GetFacultyMemberAsync(savedFacultyMember.Id);

            // Assert
            Assert.NotNull(fetchedFacultyMember);
            Assert.Equal(newFacultyMember.NameAr, fetchedFacultyMember.NameAr);
        }

        [Fact]
        public async Task GetFacultyMemberAsync_ShouldReturnNull_WhenFacultyMemberDoesNotExist()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new FacultyMemberService(factory);
            // Arrange
            var facultyMemberId = Guid.NewGuid();

            // Act
            var result = await service.GetFacultyMemberAsync(facultyMemberId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFacultyMemberAsync_ShouldReturnFacultyMemberByName_WhenFacultyMemberExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new FacultyMemberService(factory);
            var newFacultyMember = new FacultyMemberDto
            {
                NameAr = "John Doe",
                PhoneNumber = "123456789",
                Email = "author@company.com"
            };
            var savedFacultyMember = await service.UpsertFacultyMemberAsync(newFacultyMember);

            // Act
            var fetchedFacultyMember = await service.GetFacultyMemberAsync("John Doe");

            // Assert
            Assert.NotNull(fetchedFacultyMember);
            Assert.Equal(newFacultyMember.NameAr, fetchedFacultyMember.ToList()[0].NameAr);
        }

        [Fact]
        public async Task GetFacultyMemberAsync_ShouldReturnTwoFacultyMembers_WhenFacultyMemberExists()
        {
            // Arrange
            var options = GetNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new FacultyMemberService(factory);
            var newFacultyMember1 = new FacultyMemberDto
            {
                Id = Guid.NewGuid(),
                NameAr = "John Doe1",
                PhoneNumber = "123456789",
                Email = "author1@company.com"
            };
            var savedFacultyMember1 = await service.UpsertFacultyMemberAsync(newFacultyMember1);

            var newFacultyMember2 = new FacultyMemberDto
            {
                Id = Guid.NewGuid(),
                NameAr = "John Doe2",
                PhoneNumber = "123456789",
                Email = "author2@company.com"
            };
            var savedFacultyMember2 = await service.UpsertFacultyMemberAsync(newFacultyMember2);
            // Act
            var fetchedFacultyMember = await service.GetFacultyMemberAsync("John");

            // Assert
            Assert.NotNull(fetchedFacultyMember);
            Assert.Equal(2, fetchedFacultyMember.Count());
            
        }
    }
}
