using System.ComponentModel.DataAnnotations;

namespace MicroServiceArchitecture.Web.Models.Catalogs
{
    public class FeatureViewModel
    {
        [Display(Name = "Kurs süre")]
        public int Duration { get; set; }
    }
}