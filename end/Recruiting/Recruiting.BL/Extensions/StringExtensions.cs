

namespace System
{
    public static class StringExtensions
    {
        public static string CompleteUri(this string baseUri, string queryToAppend)
        {
            if (String.IsNullOrEmpty(queryToAppend))
                return baseUri;

            if (baseUri.IndexOf('?') == -1)
            {
                return baseUri + "?" +queryToAppend;
            }
            else
            {
                return baseUri + "&" + queryToAppend;
            }
        }
    }
}
