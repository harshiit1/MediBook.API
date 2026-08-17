namespace Medicare.Application.Helper.DocumentHelper
{
    public static class DocumentHelper
    {
        private const int MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        private static readonly Dictionary<string, byte[]> AllowedSignatures = new()
        {
            { "PDF", new byte[] { 0x25, 0x50, 0x44, 0x46 } }, // %PDF
            { "PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }, // ‰PNG
            { "JPG", new byte[] { 0xFF, 0xD8 } }              // JPEG SOI marker
        };

        public static byte[]? ConvertBase64ToBytes(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            try
            {
                return Convert.FromBase64String(base64String);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static bool IsFileSizeValid(byte[] fileBytes)
        {
            return fileBytes.Length <= MaxFileSizeBytes;
        }

        public static bool IsAllowedFileType(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length < 2)
                return false;

            foreach (var signature in AllowedSignatures.Values)
            {
                if (fileBytes.Length >= signature.Length &&
                    fileBytes.Take(signature.Length).SequenceEqual(signature))
                    return true;
            }

            return false;
        }

        public static string GetFileType(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length < 2)
                return "Unknown";

            foreach (var (type, signature) in AllowedSignatures)
            {
                if (fileBytes.Length >= signature.Length &&
                    fileBytes.Take(signature.Length).SequenceEqual(signature))
                    return type;
            }

            return "Unknown";
        }

        public static string? ConvertBytesToBase64(byte[]? fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
                return null;

            return Convert.ToBase64String(fileBytes);
        }

        public static (byte[]? Bytes, string? Error) ProcessDocument(string? docString)
        {
            if (string.IsNullOrWhiteSpace(docString))
                return (null, "Invalid document format. Must be a valid Base64 string.");

            // "data:image/png;base64,ABC123" → "ABC123"

            var base64String = docString.Contains(",") ? docString.Split(',')[1] : docString;

            var fileBytes = ConvertBase64ToBytes(base64String);

            if (fileBytes == null)
                return (null, "Invalid document format. Must be a valid Base64 string.");

            if (!IsFileSizeValid(fileBytes))
                return (null, $"Document size exceeds the allowed limit of {MaxFileSizeBytes / (1024 * 1024)}MB.");

            if (!IsAllowedFileType(fileBytes))
                return (null, "Only PDF, PNG, and JPG documents are allowed.");

            return (fileBytes, null);
        }
    }
}
