namespace SM.FMA.Data.Entities
{
    public class FacultyTitle
    {
        public Guid Id { get; set; }
        public required string TitleName { get; set; }
        public int TeachingHoursPerWeek { get; set; }
        public int SabbaticalLeaveEveryYears { get; set; }
        public required FacultyMember FacultyMember { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
