namespace PPMGChronical.Models
{
    // Models/AuthResult.cs
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public UserData User { get; set; }
    }

    public class UserData
    {
        public string Uid { get; set; }
        public string Email { get; set; }
    }

    // Models/FirestoreResult.cs
    public class FirestoreResult<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
        public string Id { get; set; }
    }
}
