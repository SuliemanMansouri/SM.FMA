namespace SM.FMA.Components.Pages.FacultyRankComponents
{
    public class FacultyRankDto
    {
        public Guid Id { get; set; }
        public string RankName { get; set; } = string.Empty;
        public DateOnly? PromotionDate { get; set; }
        public string CriteriaMet { get; set; } = string.Empty; // comma-separated
        public Guid FacultyMemberId { get; set; }
    }
}
