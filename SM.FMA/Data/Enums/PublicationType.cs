using System.ComponentModel.DataAnnotations;

namespace SM.FMA.Data.Enums
{
    public enum PublicationType
    {
        [Display(Name = "غير محدد")]
        Other,
        [Display(Name = "رسالة/مشروع")]
        Thesis,
        [Display(Name = "ورقة بحثية")]
        Paper,
        [Display(Name = "كتاب")]
        Book,
        [Display(Name = "فصل كتاب")]
        BookChapter,
        [Display(Name = "تقرير فني")]
        TechnicalReport,
        [Display(Name = "براءة اختراع")]
        Patent
    }
}
