using System;

namespace StickerApp.Misc
{
    public class StickerAppException : Exception
    {
        public readonly string Error;
        public readonly string Reason;

        public StickerAppException(string error, string reason = null)
        {
            Error = error;
            Reason = reason;
        }
    }
}