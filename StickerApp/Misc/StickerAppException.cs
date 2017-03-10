using System;

namespace StickerApp.Misc
{
    public class StickerAppException : Exception
    {
        public readonly string Error;
        public readonly string Reason;
        public readonly int? SuggestedHttpErrorCode;

        public StickerAppException(string error, string reason = null, int? suggestedHttpErrorCode = null)
        {
            Error = error;
            Reason = reason;
            SuggestedHttpErrorCode = suggestedHttpErrorCode;
        }
    }
}