using SM.FMA.Data.Enums;

namespace SM.FMA.Components.Pages.PublicationComponents
{
    public class PublicationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? DatePublished { get; set; }
        public string Publisher { get; set; } = string.Empty;
        public PublicationType PublishingType { get; set; }
        public string CoAuthors { get; set; }
        public Guid FacultyMemberId { get; set; }
    }
}
