using SM.FMA.Data.Enums;

namespace SM.FMA.Data.Entities;
public class Publication
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateOnly DatePublished { get; set; }
    public required string Publisher { get; set; }
    public PublicationType PublishingType { get; set; }
    public required List<string> CoAuthors { get; set; }
    public required FacultyMember FacultyMember { get; set; }
    public Guid FacultyMemberId { get; set; }
}