using System.Text;

namespace System
{
    /// <summary>
    /// SharedExtensions
    /// </summary>
    public static class StringExtensions
    {
        public static decimal Similarity(
            this string source,
            string compare)
        {
            if (source is null && compare is null)
            {
                return 1;
            }
            if (source is null || compare is null)
            {
                return 0;
            }
            var intersect = source!.Intersect(compare!).Count();
            if (intersect == 0)
            {
                return 0;
            }

            var union = source!.Union(compare!).Count();
            if (union == 0)
            {
                return 0;
            }

            return decimal.Divide(intersect, union);
        }

        public static string Desensitize(
            this string source,
            int startLength = 3,
            int endLength = 4)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            ReadOnlySpan<char> sourceSpan = source;
            var leftSpan = sourceSpan.Slice(0, startLength);
            Span<char> centerSpan;
            {
                var centerLength = source.Length - startLength - endLength;
                centerSpan = new Span<char>(new char[centerLength]);
                centerSpan.Fill('*');
            }
            var rightSpan = sourceSpan.Slice(source.Length - endLength);

            return new StringBuilder(source.Length)
                .Append(leftSpan)
                .Append(centerSpan)
                .Append(rightSpan)
                .ToString();
        }
    }
}