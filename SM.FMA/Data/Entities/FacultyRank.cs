namespace SM.FMA.Data.Entities
{
    public class FacultyRank
    {
        public Guid Id { get; set; }
        public required string RankName { get; set; }
        public DateOnly PromotionDate { get; set; }
        public required List<string> CriteriaMet { get; set; }
        public required FacultyMember FacultyMember { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
