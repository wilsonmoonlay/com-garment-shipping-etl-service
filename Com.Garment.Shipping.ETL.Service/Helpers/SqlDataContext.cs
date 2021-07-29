using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Com.DyeingPrinting.ETL.Service.Helpers
{
    public class SqlDataContext<TModel> : ISqlDataContext<TModel>
    {

        private readonly SqlConnection _connectionOrigin;
        private readonly SqlConnection _connectionDestination;

        public SqlDataContext(string connectionStringOrigin, string connectionStringDestination)
        {
            _connectionOrigin = CreateConnection(connectionStringOrigin);
            _connectionDestination = CreateConnection(connectionStringDestination);
        }

        public async Task<int> ExecuteAsync(string query, IEnumerable<TModel> models)
        {
            if(_connectionOrigin.State != ConnectionState.Open)
                _connectionOrigin.Open(); 
            
            var transaction = _connectionOrigin.BeginTransaction();
            var result = await _connectionOrigin.ExecuteAsync(query, models, transaction : transaction);
            transaction.Commit();

            if(_connectionOrigin.State == ConnectionState.Open)
                _connectionOrigin.Close();
            return result;
        }

        public async Task<IEnumerable<TModel>> QueryAsync(string query)
        {
            if(_connectionOrigin.State == ConnectionState.Closed)
                _connectionOrigin.Open(); 

            
            var transaction = _connectionOrigin.BeginTransaction();
            var result = await _connectionOrigin.QueryAsync<TModel>(query, transaction : transaction);
            _connectionOrigin.Close();

            if(_connectionOrigin.State == ConnectionState.Open)
                _connectionOrigin.Close();
            return result;
        }

        public async Task<IEnumerable<TModel>> QueryAsync(string query, Object newObject)
        {
            if(_connectionOrigin.State == ConnectionState.Closed)
                _connectionOrigin.Open(); 
            
            var transaction = _connectionOrigin.BeginTransaction();
            var result = await _connectionOrigin.QueryAsync<TModel>(query, newObject, transaction : transaction);
            transaction.Commit();

            if(_connectionOrigin.State == ConnectionState.Open)
                _connectionOrigin.Close();
            return result;
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            return new SqlConnection(connectionString);
        }
    }

    public interface ISqlDataContext<TModel>
    {
        Task<int> ExecuteAsync(string query, IEnumerable<TModel> model);
        Task<IEnumerable<TModel>> QueryAsync(string query);
        Task<IEnumerable<TModel>> QueryAsync(string query, Object newObject);
    }
}
