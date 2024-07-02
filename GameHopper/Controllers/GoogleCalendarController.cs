using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GameHopper.Controllers
{
    public class GoogleCalendarController : Controller
    {
        private readonly IConfiguration _configuration;

        public GoogleCalendarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var credential = await GetGoogleCredential();

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GameHopper",
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = await request.ExecuteAsync();

            return View(events.Items);
        }

        private async Task<UserCredential> GetGoogleCredential()
        {
            using (var stream = new FileStream("App_Data/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { CalendarService.Scope.CalendarReadonly },
                    "user",
                    CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(credPath, true));
            }
        }
    }
}
