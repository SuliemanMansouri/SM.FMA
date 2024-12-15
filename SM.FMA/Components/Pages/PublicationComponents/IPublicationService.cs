namespace SM.FMA.Components.Pages.PublicationComponents
{
    public interface IPublicationService
    {
        Task<IEnumerable<PublicationDto>> GetAllPublicationsAsync();
        Task<IEnumerable<PublicationDto>> GetFacultyMemeberPublicationsAsync(Guid facultyMemberId);
        Task<PublicationDto?> GetPublicationByIdAsync(Guid id);
        Task<PublicationDto> UpsertPublicationAsync(PublicationDto publication);
        Task DeletePublicationAsync(Guid id);
    }
}
