using System.ComponentModel.DataAnnotations;

namespace SM.FMA.Data.Enums
{
    public enum ContractualDesignation
    {
        [Display(Name = "غير محدد")]
        None = 0,
        [Display(Name = "قار")]
        Resident,
        [Display(Name = "متعاون")]
        Adjunct,
        [Display(Name = "معيد")]
        TeachingAssistant,
        [Display(Name = "زائر")]
        Visiting,
        [Display(Name = "باحث زائر")]
        VisitingResearcher,
        [Display(Name = "زميل بحث")]
        ResearchFellow,
        [Display(Name = "مؤقت")]
        TemporaryFaculty,
        [Display(Name = "ندب")]
        AffiliatedFaculty
    }
}
