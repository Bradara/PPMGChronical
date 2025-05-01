namespace PPMGChronical.Services
{
    // Services/ChronicleService.cs
    using Microsoft.JSInterop;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using PPMGChronical.Models;

    public class ChronicleService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string EventsCollection = "events";
        private const string PeopleCollection = "people";
        private const string MilestonesCollection = "milestones";
        private const string MediaCollection = "media";

        public ChronicleService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Events methods
        public async Task<string> AddEventAsync(ChronicleEvent chronicleEvent)
        {
            chronicleEvent.CreatedAt = DateTime.UtcNow;
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.addDocument",
                EventsCollection,
                JsonSerializer.Serialize(chronicleEvent)
            );

            return result.Success ? result.Id : null;
        }

        public async Task<List<ChronicleEvent>> GetEventsAsync()
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.getCollection",
                EventsCollection
            );

            if (result.Success)
            {
                return JsonSerializer.Deserialize<List<ChronicleEvent>>(result.Data);
            }

            return new List<ChronicleEvent>();
        }

        public async Task<ChronicleEvent> GetEventAsync(string id)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.getDocument",
                EventsCollection,
                id
            );

            if (result.Success)
            {
                var eventData = JsonSerializer.Deserialize<ChronicleEvent>(result.Data);
                eventData.Id = result.Id;
                return eventData;
            }

            return null;
        }

        public async Task<bool> UpdateEventAsync(ChronicleEvent chronicleEvent)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<bool>>(
                "firebaseInterop.updateDocument",
                EventsCollection,
                chronicleEvent.Id,
                JsonSerializer.Serialize(chronicleEvent)
            );

            return result.Success;
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<bool>>(
                "firebaseInterop.deleteDocument",
                EventsCollection,
                id
            );

            return result.Success;
        }

        // People methods
        public async Task<string> AddPersonAsync(Person person)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.addDocument",
                PeopleCollection,
                JsonSerializer.Serialize(person)
            );

            return result.Success ? result.Id : null;
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.getCollection",
                PeopleCollection
            );

            if (result.Success)
            {
                return JsonSerializer.Deserialize<List<Person>>(result.Data);
            }

            return new List<Person>();
        }

        // Milestone methods
        public async Task<string> AddMilestoneAsync(Milestone milestone)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.addDocument",
                MilestonesCollection,
                JsonSerializer.Serialize(milestone)
            );

            return result.Success ? result.Id : null;
        }

        public async Task<List<Milestone>> GetMilestonesAsync()
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.getCollection",
                MilestonesCollection
            );

            if (result.Success)
            {
                return JsonSerializer.Deserialize<List<Milestone>>(result.Data);
            }

            return new List<Milestone>();
        }

        // Media methods
        public async Task<string> AddMediaItemAsync(MediaItem mediaItem)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.addDocument",
                MediaCollection,
                JsonSerializer.Serialize(mediaItem)
            );

            return result.Success ? result.Id : null;
        }

        public async Task<List<MediaItem>> GetMediaItemsAsync()
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>(
                "firebaseInterop.getCollection",
                MediaCollection
            );

            if (result.Success)
            {
                return JsonSerializer.Deserialize<List<MediaItem>>(result.Data);
            }

            return new List<MediaItem>();
        }

        // Advanced queries
        public async Task<List<ChronicleEvent>> GetEventsByYearAsync(int year)
        {
            // This is a client-side filter since Firestore complex queries
            // require special JavaScript interop
            var allEvents = await GetEventsAsync();
            return allEvents
                .Where(e => e.Date.Year == year)
                .ToList();
        }

        public async Task<List<ChronicleEvent>> GetEventsByCategoryAsync(string category)
        {
            var allEvents = await GetEventsAsync();
            return allEvents
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
