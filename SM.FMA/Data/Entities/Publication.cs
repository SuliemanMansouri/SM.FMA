using SM.FMA.Data.Enums;

namespace SM.FMA.Data.Entities;
public class Publication
{
    public Publication()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        DatePublished = DateTime.Now;
        Publisher = string.Empty;
        PublishingType = PublicationType.Other;
        CoAuthors = new List<string>();
        FacultyMemberId = Guid.Empty;
    }
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime DatePublished { get; set; }
    public required string Publisher { get; set; }
    public PublicationType PublishingType { get; set; }
    public required List<string> CoAuthors { get; set; }
    public FacultyMember? FacultyMember { get; set; }
    public required Guid FacultyMemberId { get; set; }
}