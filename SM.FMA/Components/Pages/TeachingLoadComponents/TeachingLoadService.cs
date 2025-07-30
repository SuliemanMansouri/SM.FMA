using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;

namespace SM.FMA.Components.Pages.TeachingLoadComponents;

public class TeachingLoadService(IDbContextFactory<ApplicationDbContext> dbContextFactory) : ITeachingLoadService
{
    public async Task DeleteTeachingLoadAsync(Guid teachingLoadId)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.TeachingLoads.FirstOrDefaultAsync(x => x.Id == teachingLoadId);
        if (tmp != null)
        {
            db.TeachingLoads.Remove(tmp);
            db.SaveChanges();
        }
    }

    public async Task<TeachingLoad> GetTeachingLoadAsync(Guid teachingLoadId)
    {
        var db = dbContextFactory.CreateDbContext();
        var teachingLoad = await db.TeachingLoads.FirstOrDefaultAsync(x => x.Id == teachingLoadId);
        if (teachingLoad == null)
            throw new InvalidOperationException($"TeachingLoad with ID {teachingLoadId} not found.");
        return teachingLoad;
    }

    public async Task<List<TeachingLoad>> GetFacultyMemberTeachingLoadsAsync(Guid facultyMemberId)
    {
        try
        {
            var db = dbContextFactory.CreateDbContext();
            var teachingLoads = await db.TeachingLoads.Where(x => x.FacultyMemberId == facultyMemberId).ToListAsync();
            return teachingLoads;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error retrieving teaching loads for FacultyMemberId: {facultyMemberId}", ex);
        }
    }

    public async Task<TeachingLoad> UpsertTeachingLoadAsync(TeachingLoad teachingLoad)
    {
        var db = dbContextFactory.CreateDbContext();
        var tmp = await db.TeachingLoads.FirstOrDefaultAsync(x => x.Id == teachingLoad.Id);
        if (tmp != null)
        {
            tmp.AcademicYear = teachingLoad.AcademicYear;
            tmp.Semester = teachingLoad.Semester;
            tmp.ScanUri = teachingLoad.ScanUri;
            db.SaveChanges();
            return tmp;
        }
        else
        {
            db.TeachingLoads.Add(teachingLoad);
            db.SaveChanges();
            return teachingLoad;
        }
    }
}
