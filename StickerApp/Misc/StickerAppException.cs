using System;

namespace StickerApp.Misc
{
    public class StickerAppException : Exception
    {
        public readonly string Error;
        public readonly string Reason;

        /// <summary>
        /// Suggested HTTP Error Code if this exception is thrown outside an action.
        /// </summary>
        public readonly int? SuggestedHttpErrorCode;

        /// <summary>
        /// Constructor for StickerAppException.
        /// </summary>
        public StickerAppException(string error, string reason = null, int? suggestedHttpErrorCode = null)
        {
            Error = error;
            Reason = reason;
            SuggestedHttpErrorCode = suggestedHttpErrorCode;
        }
    }
}