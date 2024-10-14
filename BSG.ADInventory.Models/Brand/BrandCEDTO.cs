using BSG.ADInventory.Resources;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BSG.ADInventory.Models.Brand
{
    public class BrandCEDTO: BaseModel.BaseDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Display(ResourceType = typeof(Resource), Name = "FaTitle")]
        public string FaTitle { get; set; }
        
        [Display(ResourceType = typeof(Resource), Name = "EnTitle")]
        public string EnTitle { get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Picture")]
        [UIHint("Upload")]
        public string ImageUrl{ get; set; }


        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }
    }
}
