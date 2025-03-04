﻿using Dapper;
using SMS.Application.Interface.Data;
using SMS.Application.Interface.Repositories.Base;
using System.Data;

namespace SMS.Infrastructure.Implementation.Repositories
{
    public class DapperRepository(IApplicationDbContext dbContext) : IDapperRepository
    {
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            var result = await dbContext.Connection.QueryAsync<T>(sql, param, transaction);

            return result.AsList();
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await dbContext.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public Task<T> QuerySingleAsync<T>(string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return dbContext.Connection.QuerySingleAsync<T>(sql, param, transaction);
        }
    }
}
