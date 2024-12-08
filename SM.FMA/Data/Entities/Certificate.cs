namespace SM.FMA.Data.Entities
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public DateOnly DateAwarded { get; set; }
        public FacultyMember FacultyMember { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
