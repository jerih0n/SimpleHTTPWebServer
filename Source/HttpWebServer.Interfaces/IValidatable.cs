

namespace HttpWebServer.Interfaces
{
    using Shared.DataTransfer;
    /// <summary>
    /// Taking information from the UI, validate data and return IsValid, message and string input to the HTTP server class
    /// </summary>
    public interface IValidatable
    {
        ValidationResult Validate();
    }
}
