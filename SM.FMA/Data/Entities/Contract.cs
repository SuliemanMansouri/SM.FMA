namespace SM.FMA.Data.Entities
{
    public class Contract
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public required string ScanUri { get; set; }
        public required Guid FacultyMemberId { get; set; }
        public FacultyMember? FacultyMember { get; set; }

    }
}
