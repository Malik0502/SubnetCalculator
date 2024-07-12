namespace Service
{
    public static class StringExtensions{
        public static string ReverseString(this string stringToReverse)
        {
            return new string(stringToReverse.Reverse().ToArray());
        }
    }
}