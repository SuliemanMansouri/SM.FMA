namespace SM.FMA.Data.Entities
{
    public class FacultyMember
    {
        public FacultyMember() 
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
        public FacultyMember(Guid id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public List<Certificate> Certificates { get; set; } = [];   //certificates history
        public List<FacultyRank> FacultyRanks { get; set; } = [];   //promotion history
        public List<FacultyTitle> FacultyTitles { get; set; } = []; //titles history
        public List<Publication> Publications { get; set; } = [];   //publications history
        public List<FacultyPosition> FacultyPositions { get; set; } = [];  //positions history
        
       
    }
}
