using Dapper;

namespace ElectronicLibrary.Extensions
{
    internal static class DynamicParametersExtensions
    {
        internal static DynamicParameters AddParameter(this DynamicParameters dynamicParameters, string parameterName, object parameterValue)
        {
            dynamicParameters.Add(parameterName, parameterValue);
            return dynamicParameters;
        }

        internal static DynamicParameters AddPaginationParameters(this DynamicParameters dynamicParameters, int page, int size)
        {
            return dynamicParameters.AddParameter("Page", page)
                                    .AddParameter("Size", size);
        }

        internal static DynamicParameters AddIdParameter(this DynamicParameters dynamicParameters, int id)
        {
            return dynamicParameters.AddParameter("Id", id);
        }
    }
}
