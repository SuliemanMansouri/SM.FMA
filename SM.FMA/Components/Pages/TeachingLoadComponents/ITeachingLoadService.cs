using SM.FMA.Data.Entities;

public interface ITeachingLoadService
{
    Task<List<TeachingLoad>> GetFacultyMemberTeachingLoadsAsync(Guid facultyMemberId);
    Task<TeachingLoad> GetTeachingLoadAsync(Guid teachingLoadId);
    Task<TeachingLoad> UpsertTeachingLoadAsync(TeachingLoad teachingLoad);
    Task DeleteTeachingLoadAsync(Guid teachingLoadId);
}
