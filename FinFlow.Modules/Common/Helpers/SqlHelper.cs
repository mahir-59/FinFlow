using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinFlow.Modules.Common.Helpers
{
    public class SqlHelper
    {
        private readonly DBContext _dbContext;

        public SqlHelper(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // =========================================
        // EXECUTE
        // INSERT UPDATE DELETE
        // =========================================

        public async Task<int> ExecuteAsync(
                string query,
                object? parameters = null,
                IDbTransaction? transaction = null)
        {
            // If transaction exists
            if (transaction != null)
            {
                return await transaction
                    .Connection
                    .ExecuteAsync(
                        query,
                        parameters,
                        transaction);
            }

            // Normal connection
            using var connection =
                _dbContext.CreateConnection();

            return await connection.ExecuteAsync(
                query,
                parameters);
        }

        // =========================================
        // QUERY FIRST
        // =========================================

        public async Task<T?>
            QueryFirstOrDefaultAsync<T>(
                string query,
                object? parameters = null,
                IDbTransaction? transaction = null)
        {
            if (transaction != null)
            {
                return await transaction
                    .Connection
                    .QueryFirstOrDefaultAsync<T>(
                        query,
                        parameters,
                        transaction);
            }

            using var connection =
                _dbContext.CreateConnection();

            return await connection
                .QueryFirstOrDefaultAsync<T>(
                    query,
                    parameters);
        }

        // =========================================
        // QUERY LIST
        // =========================================

        public async Task<IEnumerable<T>>
            QueryAsync<T>(
                string query,
                object? parameters = null,
                IDbTransaction? transaction = null)
        {
            if (transaction != null)
            {
                return await transaction
                    .Connection
                    .QueryAsync<T>(
                        query,
                        parameters,
                        transaction);
            }

            using var connection =
                _dbContext.CreateConnection();

            return await connection
                .QueryAsync<T>(
                    query,
                    parameters);
        }

        // =========================================
        // SCALAR
        // =========================================

        public async Task<T>
            ExecuteScalarAsync<T>(
                string query,
                object? parameters = null,
                IDbTransaction? transaction = null)
        {
            if (transaction != null)
            {
                return await transaction
                    .Connection
                    .ExecuteScalarAsync<T>(
                        query,
                        parameters,
                        transaction);
            }

            using var connection =
                _dbContext.CreateConnection();

            return await connection
                .ExecuteScalarAsync<T>(
                    query,
                    parameters);
        }
    }
}
