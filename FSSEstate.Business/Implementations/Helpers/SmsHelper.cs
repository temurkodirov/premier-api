using FSSEstate.Business.Interfaces.Helpers;
using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.SmsModels;
using FSSEstate.Repository.Entities;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Web;

namespace FSSEstate.Business.Implementations.Helpers
{
    public class SmsHelper : ISmsHelper
    {
        private readonly IConfigurationSection _config;
        private readonly IConfiguration _smsConfig;
        private readonly IConfiguration _hostConfig;

        public SmsHelper(IConfiguration configuration)
        {
            _config = configuration.GetSection("Email");
            _smsConfig = configuration.GetSection("SMSClient");
            _hostConfig = configuration.GetSection("LocalDomain");
        }

        public async Task SendMessege(string emailOrPhone, string code)
        {
            if (emailOrPhone.StartsWith("+99"))
            {
                //sms for phone number               

            }
            else
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config["EmailName"]));
                email.To.Add(MailboxAddress.Parse(emailOrPhone));
                email.Subject = "";
                email.Body = new TextPart(TextFormat.Html) { Text = code };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailName"], _config["Password"]);
                var res = await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

            }
        }

        public async Task SendUrlMessage(string email, string message)
        {
            var emailConfig = new MimeMessage();
            emailConfig.From.Add(MailboxAddress.Parse(_config["EmailName"]));
            emailConfig.To.Add(MailboxAddress.Parse(email));
            emailConfig.Subject = "";
            emailConfig.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailName"], _config["Password"]);
            var res = await smtp.SendAsync(emailConfig);
            await smtp.DisconnectAsync(true);
        }

        private async Task<string> Login()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_smsConfig["Host"]);
                var formData = new Dictionary<string, string>
                        {
                            { "email", $"{_smsConfig["EmailName"]}" },
                            { "password", $"{_smsConfig["Password"]}" }
                        };
                // Create HTTP request content with form data
                var content = new FormUrlEncodedContent(formData);

                // Send POST request and get the response
                var response = await httpClient.PostAsync("api/auth/login", content);

                // Check if the request was successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(responseContent);

                    // Access the token property directly
                    string token = jsonObject["data"]?["token"]?.ToString();
                    return token;
                }
                else
                    throw new Exception($"Response code: {response.StatusCode.ToString()}");
            }
        }

        public async Task<SmsResponseModel> SendMessageToPhone(string phone, string message)
        {
            // Specify your Authorization Bearer token
            var bearerToken = Login().Result;

            // Create HttpClient
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_smsConfig["Host"]);
                // Add Authorization Bearer token to request headers
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

                // Create form data
                var formData = new Dictionary<string, string>
                {
                    { "mobile_phone", $"{phone}" },
                    { "message", $"{message}" },
                    { "from", "4546" },
                    { "callback_url", "http://0000.uz/test.php" }
                };

                // Create HTTP request content with form data
                var content = new FormUrlEncodedContent(formData);

                // Send POST request and get the response
                var response = await httpClient.PostAsync("api/message/sms/send", content);

                // Check if the request was successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read and print the response content
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<SmsResponseModel>(responseContent);

                    return responseData;
                }
                else
                    throw new Exception($"Response code: {response.StatusCode.ToString()}");
            }
        }

        public UriBuilder CreateUrlSendToEmail(AccountCreateModel obj, string guid)
        {
            // create url send to email 
            
            var address = _hostConfig["Domain"]; // need to set host address
            var uriBuilder = new UriBuilder(address) { Port = 80 }; //need to set host address port 
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["emailOrPhoneNumber"] = obj.EmailOrPhoneNumber;
            query["code"] = guid;

            uriBuilder.Query = query.ToString();

            return uriBuilder;
        }
    }
}