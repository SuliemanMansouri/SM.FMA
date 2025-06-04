namespace SM.FMA.Data.Entities
{
    public class Contract
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ScanUri { get; set; }
        public Guid FacultyMemberId { get; set; }
        public FacultyMember FacultyMember { get; set; }

    }
}
