namespace SM.FMA.Data.Entities
{
    public class FacultyPosition
    {
        public Guid Id { get; set; }
        public string PositionName { get; set; }
        public string Office { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public FacultyMember FacultyMember { get; set; }
        public Guid FacultyMemberId { get; set; }

    }
}
