using SM.FMA.Data.Enums;

namespace SM.FMA.Data.Entities
{
    public class TeachingLoad
    {
        public Guid Id { get; set; }
        public string AcademicYear { get; set; }
        public Semester Semester { get; set; }
        public string ScanUri { get; set; }
        public Guid FacultyMemberId { get; set; }
        public FacultyMember FacultyMember { get; set; }
    }
}
