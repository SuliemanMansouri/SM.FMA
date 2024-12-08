namespace SM.FMA.Components.Pages.FacultyMemberComponents
{
    public interface IFacultyMemberService
    {
        Task<IEnumerable<FacultyMemberDto>> GetFacultyMembersAsync();
        Task<FacultyMemberDto> GetFacultyMemberAsync(Guid id);
        Task<IEnumerable<FacultyMemberDto>> GetFacultyMemberAsync(string Name);
        Task<FacultyMemberDto> UpsertFacultyMemberAsync(FacultyMemberDto teacher);
        Task DeleteFacultyMemberAsync(Guid id);
    }
}
