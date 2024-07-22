using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.SmsModels;

namespace FSSEstate.Business.Interfaces.Helpers
{
    public interface ISmsHelper
    {
        public Task SendMessege(string emailOrPhone, string code);
        public Task SendUrlMessage(string email, string message);
        public Task<SmsResponseModel> SendMessageToPhone(string phone, string message);
        public UriBuilder CreateUrlSendToEmail(AccountCreateModel obj, string guid);
    }
}
