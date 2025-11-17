namespace SM.FMA.Components.Pages.FacultyRankComponents
{
    public interface IFacultyRankService
    {
        Task<IEnumerable<FacultyRankDto>> GetFacultyMemberRanksAsync(Guid facultyMemberId);
        Task<FacultyRankDto?> GetFacultyRankByIdAsync(Guid id);
        Task<FacultyRankDto> UpsertFacultyRankAsync(FacultyRankDto rank);
        Task DeleteFacultyRankAsync(Guid id);
    }
}
