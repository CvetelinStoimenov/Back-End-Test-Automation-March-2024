using Eventmi.Core.Models.Event;
using Eventmi.Infrastructure.Data.Contexts;
using Eventmi.Infrastructure.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Net;
using System.Xml.Linq;

namespace Eventmi.Tests
{
    [TestFixture]
    public class EventControllerTests
    {
        private RestClient _client;
        private const string _baseURL = @"https://localhost:7236";
        private static int lastEventId;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(_baseURL);
        }

        [Test]
        public async Task GetAllEvents_ShouldReturnsSuccessStatusCode()
        {
            // Arrange: Prepare the HTTP request to the All action
            var request = new RestRequest("/Event/All");

            // Act: Execute HTTP request
            var response = await _client.ExecuteAsync(request, Method.Get);

            // Assert: Verify that the response status code is OK (200)
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Add_GetRequest_ShouldReturn_AddView()
        {
            // Arrange
            var request = new RestRequest("Event/Add");

            // Act
            var response = await _client.ExecuteAsync(request, Method.Get);

            //Assert
            // Check status code
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Add_PostRequest_AddEventAndRedirects() 
        {
            // Arrange
            var newEvent = new EventFormModel()
            {
                Name = "DEV: Challenge Accepted",
                Place = "Sofia Tech Park",
                Start = new DateTime(2024, 09, 09, 08, 0, 0),
                End = new DateTime(2024, 09, 09, 09, 0, 0),
            };

            // Create a request object, specifying the endpoint and method
            var request = new RestRequest("/Event/Add", Method.Post);

            // Specify that the request will use collected data from form
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // Add form data to the request
            request.AddParameter("Name", newEvent.Name);
            request.AddParameter("Place", newEvent.Place);
            request.AddParameter("Start", newEvent.Start.ToString("MM/dd/yyyy hh:mm tt"));
            request.AddParameter("End", newEvent.End.ToString("MM/dd/yyyy hh:mm tt"));

            // Act
            // Execute the request
            var response = await _client.ExecuteAsync(request);

            var eventInDb = GetEventByName(newEvent.Name);
            lastEventId = eventInDb.Id;

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            //// Now check the database using CheckEventExists() method
            Assert.That(CheckEventExists(newEvent.Name), "The event was not added to the database");
        }

        private bool CheckEventExists(string eventNamse)
        {
            // Define your DbContext option
            var option = new DbContextOptionsBuilder<EventmiContext>
                ().UseSqlServer
                ("Server=(local)\\SQLEXPRESS;Database=Eventmi;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            // Instantiate the context with the options
            using var context = new EventmiContext(option);

            // Perform the check
            return context.Events.Any(e => e.Name == eventNamse);
        }

        [Test]
        public async Task GetEventDetails_ReturnsSuccessAndExpectedContent()
        {
            // Arrange: Assuming an event with a known Id exists
            var eventId = 1; // Adjusting this ID as needed
            // Create a request object, specifying the endpoint and method
            var request = new RestRequest($"/Event/Details/{eventId}");

            // Act: Execute HTTP request
            var response = await _client.ExecuteAsync(request, Method.Get);

            // Assert: Verify that the response status code is OK (200)
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task EditAction_returnsViewForValidId()
        {
            // Arrange
            int? eventId = 24; // Ensure this is a valid ID for an existing event
            var request = new RestRequest($"/Event/Edit/{eventId}");

            // Act
            var response = await _client.ExecuteAsync(request, Method.Get);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task EditPostAction_WithValidId_ShouldEditSuccessful()
        {
            // Arrange
            var eventId = 28;
            var eventToEdit = await GetEventByIdAsync(eventId);

            var eventModel = new EventFormModel()
            {
                Id = eventToEdit.Id,
                Name = eventToEdit.Name,
                Place = eventToEdit.Place,
                Start = eventToEdit.Start,
                End = eventToEdit.End
            };

            string updateName = "Updated Event Name";
            eventModel.Name = updateName;

            var request = new RestRequest($"/Event/Edit/{eventId}", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // Add form data to the request
            request.AddParameter("Id", eventModel.Id);
            request.AddParameter("Name", eventModel.Name);
            request.AddParameter("Place", eventModel.Place);
            request.AddParameter("Start", eventModel.Start.ToString("MM/dd/yyyy hh:mm tt"));
            request.AddParameter("End", eventModel.End.ToString("MM/dd/yyyy hh:mm tt"));

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private async Task<Event> GetEventByIdAsync(int id)
        {
            var options = new DbContextOptionsBuilder<EventmiContext>
                    ().UseSqlServer
                    ("Server=(local)\\SQLEXPRESS;Database=Eventmi;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

            using (var context = new EventmiContext(options))
            {
                return await context.Events.FirstOrDefaultAsync(e => e.Id == id);            
            }
        }

        [Test]
        public async Task EditPostAction_WithIdMismatch_ReturnNotFound()
        {
            // Arrange
            var eventId = 10;
            var eventToEdit = await GetEventByIdAsync(eventId);

            var eventModel = new EventFormModel()
            {
                Id = 156,
                Name = eventToEdit.Name,
                Place = eventToEdit.Place,
                Start = eventToEdit.Start,
                End = eventToEdit.End
            };

            //string updateName = "Test Updated Event Name";
            //eventModel.Name = updateName;

            var request = new RestRequest($"/Event/Edit/{eventId}", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // Add form data to the request
            request.AddParameter("Id", eventModel.Id);
            request.AddParameter("Name", eventModel.Name);
            request.AddParameter("Place", eventModel.Place);
            request.AddParameter("Start", eventModel.Start.ToString("MM/dd/yyyy hh:mm tt"));
            request.AddParameter("End", eventModel.End.ToString("MM/dd/yyyy hh:mm tt"));

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task EditPostAction_WithInvalidModel_ReturnViewWithModel()
        {
            // Arrange
            var eventId = 28;
            var eventToEdit = await GetEventByIdAsync(eventId);

            var eventModel = new EventFormModel()
            {
                Id = eventToEdit.Id,
                Name = "",
                Place = ""
            };

            string updateName = "Updated Event Name";
            eventModel.Name = updateName;

            var request = new RestRequest($"/Event/Edit/{eventId}", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // Add form data to the request
            request.AddParameter("Id", eventModel.Id);
            request.AddParameter("Name", eventModel.Name);

            // Act
            var response = await _client.ExecuteAsync(request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task DeleteAction_WithValidId_RedirectsToAllEvents()
        {
            //// Arrange
            //var newEvent = new EventFormModel()
            //{
            //    Name = "Soft Uni Conf",
            //    Place = "Soft Uni",
            //    Start = new DateTime(2024, 09, 09, 08, 0, 0),
            //    End = new DateTime(2024, 09, 09, 09, 0, 0),
            //};

            //// Create a request object, specifying the endpoint and method
            //var request = new RestRequest("/Event/Add", Method.Post);

            //// Specify that the request will use collected data from form
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            //// Add form data to the request
            //request.AddParameter("Name", newEvent.Name);
            //request.AddParameter("Place", newEvent.Place);
            //request.AddParameter("Start", newEvent.Start.ToString("MM/dd/yyyy hh:mm tt"));
            //request.AddParameter("End", newEvent.End.ToString("MM/dd/yyyy hh:mm tt"));

            //await _client.ExecuteAsync(request);

            //var eventInDb = GetEventByName(newEvent.Name);
            //var eventIDToDelete = eventInDb.Id;
            var eventIDToDelete = lastEventId;
            var deleteRequest = new RestRequest($"/Event/Delete/{eventIDToDelete}", Method.Post);

            // Act
            var response = await _client.ExecuteAsync(deleteRequest);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private Event GetEventByName(string name)
        {
            var options = new DbContextOptionsBuilder<EventmiContext>
                    ().UseSqlServer
                    ("Server=(local)\\SQLEXPRESS;Database=Eventmi;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

            using var context = new EventmiContext(options);

            return context.Events.FirstOrDefault(e => e.Name == name);
        }
    }
}