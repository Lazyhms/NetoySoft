using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Microsoft.EntityFrameworkCore;

public static class RelationalDatabaseFacadeExtensions
{
    public static IEnumerable<T> ExecuteSqlReader<T>(
        this DatabaseFacade databaseFacade,
        FormattableString formattableString,
        Func<IDataRecord, T> resultSelector)
    {
        var facadeDependencies = GetFacadeDependencies(databaseFacade);
        var concurrencyDetector = facadeDependencies.CoreOptions.AreThreadSafetyChecksEnabled
            ? facadeDependencies.ConcurrencyDetector
            : null;
        var logger = facadeDependencies.CommandLogger;

        concurrencyDetector?.EnterCriticalSection();

        try
        {
            var rawSqlCommand = facadeDependencies.RawSqlCommandBuilder
                .Build(formattableString.Format, formattableString.GetArguments()!);

            var relationalDataReader = rawSqlCommand
                .RelationalCommand
                .ExecuteReader(
                    new RelationalCommandParameterObject(
                        facadeDependencies.RelationalConnection,
                        rawSqlCommand.ParameterValues,
                        null,
                        ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Context,
                        logger, CommandSource.Unknown));

            return relationalDataReader.DbDataReader.Cast<IDataRecord>().Select(resultSelector);
        }
        finally
        {
            concurrencyDetector?.ExitCriticalSection();
        }
    }

    public static async Task<IEnumerable<T>> ExecuteSqlReaderAsync<T>(
        this DatabaseFacade databaseFacade,
        FormattableString formattableString,
        Func<IDataRecord, T> resultSelector,
        CancellationToken cancellationToken = default)
    {
        var facadeDependencies = GetFacadeDependencies(databaseFacade);
        var concurrencyDetector = facadeDependencies.CoreOptions.AreThreadSafetyChecksEnabled
            ? facadeDependencies.ConcurrencyDetector
            : null;
        var logger = facadeDependencies.CommandLogger;

        concurrencyDetector?.EnterCriticalSection();

        try
        {
            var rawSqlCommand = facadeDependencies.RawSqlCommandBuilder
                .Build(formattableString.Format, formattableString.GetArguments()!);

            var relationalDataReader = await rawSqlCommand
               .RelationalCommand
               .ExecuteReaderAsync(
                   new RelationalCommandParameterObject(
                       facadeDependencies.RelationalConnection,
                       rawSqlCommand.ParameterValues,
                       null,
                       ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Context,
                       logger, CommandSource.Unknown), cancellationToken);

            return relationalDataReader.DbDataReader.Cast<IDataRecord>().Select(resultSelector);
        }
        finally
        {
            concurrencyDetector?.ExitCriticalSection();
        }
    }

    private static IRelationalDatabaseFacadeDependencies GetFacadeDependencies(DatabaseFacade databaseFacade)
    {
        var dependencies = ((IDatabaseFacadeDependenciesAccessor)databaseFacade).Dependencies;

        if (dependencies is IRelationalDatabaseFacadeDependencies relationalDependencies)
        {
            return relationalDependencies;
        }

        throw new InvalidOperationException(RelationalStrings.RelationalNotInUse);
    }
}
