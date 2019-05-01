using System.ComponentModel.DataAnnotations;

namespace Authentication.Model
{
    public class EditUserModel : AddUserModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }
    }
}
