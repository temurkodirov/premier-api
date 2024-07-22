namespace FSSEstate.Core.Models.AccountModels
{
    public class PasswordUpdateModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
