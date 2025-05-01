namespace PPMGChronical.Services
{
    // Services/FirestoreService.cs
    using Microsoft.JSInterop;
    using PPMGChronical.Models;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class FirestoreService
    {
        private readonly IJSRuntime _jsRuntime;

        public FirestoreService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<FirestoreResult<string>> AddDocumentAsync<T>(string collectionName, T document)
        {
            var documentJson = JsonSerializer.Serialize(document);
            return await _jsRuntime.InvokeAsync<FirestoreResult<string>>("firebaseInterop.addDocument", collectionName, documentJson);
        }

        public async Task<FirestoreResult<T>> GetDocumentAsync<T>(string collectionName, string documentId)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>("firebaseInterop.getDocument", collectionName, documentId);

            if (result.Success)
            {
                var data = JsonSerializer.Deserialize<T>(result.Data);
                return new FirestoreResult<T>
                {
                    Success = true,
                    Data = data,
                    Id = result.Id
                };
            }

            return new FirestoreResult<T>
            {
                Success = false,
                Error = result.Error
            };
        }

        public async Task<FirestoreResult<List<T>>> GetCollectionAsync<T>(string collectionName)
        {
            var result = await _jsRuntime.InvokeAsync<FirestoreResult<string>>("firebaseInterop.getCollection", collectionName);

            if (result.Success)
            {
                var data = JsonSerializer.Deserialize<List<T>>(result.Data);
                return new FirestoreResult<List<T>>
                {
                    Success = true,
                    Data = data
                };
            }

            return new FirestoreResult<List<T>>
            {
                Success = false,
                Error = result.Error
            };
        }

        public async Task<FirestoreResult<bool>> UpdateDocumentAsync<T>(string collectionName, string documentId, T updatedData)
        {
            var dataJson = JsonSerializer.Serialize(updatedData);
            return await _jsRuntime.InvokeAsync<FirestoreResult<bool>>("firebaseInterop.updateDocument", collectionName, documentId, dataJson);
        }

        public async Task<FirestoreResult<bool>> DeleteDocumentAsync(string collectionName, string documentId)
        {
            return await _jsRuntime.InvokeAsync<FirestoreResult<bool>>("firebaseInterop.deleteDocument", collectionName, documentId);
        }
    }
}
