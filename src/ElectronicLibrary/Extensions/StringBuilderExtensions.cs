using System.Text;

namespace ElectronicLibrary.Extensions
{
    internal static class StringBuilderExtensions
    {
        internal static StringBuilder AppendPagination(this StringBuilder stringBuilder)
        {
            return stringBuilder.Append(" ORDER BY (SELECT NULL) OFFSET((@Page - 1) * @Size) ROWS FETCH NEXT @Size ROWS ONLY");
        }
    }
}
