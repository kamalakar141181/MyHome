using Dapper;
using System.Data;
using static Dapper.SqlMapper;


namespace MyHome.WebAPI.Helpers
{
    public class SqlHelper : IDisposable, ISqlHelper
    {
        private readonly IConnection connection;
        private readonly ILogger<SqlHelper> logger;
        private IDbConnection dbConnection;
        private IDbTransaction dbtransaction;

        public SqlHelper(IConnection connection, ILogger<SqlHelper> logger)
        {            
            this.connection = connection;
            this.logger = logger;
        }

        public void OpenConnection()
        {
            dbConnection = connection != null ? connection.GetConnection : null;
            if(dbConnection != null && dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }           
        }

        public void CloseConnection()
        {
            dbConnection = connection != null ? connection.GetConnection : null;
            if (dbConnection != null && dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
            }
        }

        public void StartTransaction()
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbtransaction = dbConnection.BeginTransaction(System.Data.IsolationLevel.Unspecified);
        }
        public void CommitTransaction()
        {
            if (dbtransaction != null)
                dbtransaction.Commit();
            if (dbConnection != null)
                dbConnection.Close();
        }

        public void RollbackTransaction()
        {
            if (dbtransaction != null)
                dbtransaction.Rollback();

            if (dbConnection != null)
                dbConnection.Close();
        }

        public DynamicParameters CreateParameter(string name, object value)
        {
            return CreateParameter(name, value, null, null, null, null, null);
        }

        public DynamicParameters CreateParameter(string name, object value, DbType? dbType)
        {
            return CreateParameter(name, value, dbType, null, null, null, null);
        }

        public DynamicParameters CreateParameter(string name, object value, DbType? dbType, ParameterDirection? direction)
        {
            return CreateParameter(name, value, dbType, direction, null, null, null);
        }

        public DynamicParameters CreateParameter(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            return CreateParameter(name, value, dbType, direction, size, null, null);
        }

        public DynamicParameters CreateParameter(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(name, value, dbType, direction, size, precision, scale);
            return dynamicParameters;
        }
        
        public DynamicParameters GenerateParametersFromModel(object model, bool generateId)
        {
            throw new NotImplementedException();
        }

        public List<T> GetData<T>(string query)
        {
            try
            {
                OpenConnection();
                List<T> data = dbConnection.Query<T>(query, commandType: CommandType.Text, commandTimeout: 200).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<T> GetData<T>(string query, DynamicParameters dynamicParameters)
        {
            try
            {
                OpenConnection();
                List<T> data = dbConnection.Query<T>(query, dynamicParameters, commandType: CommandType.Text, commandTimeout: 200).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<T> GetData<T>(string storedProcedure, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                OpenConnection();
                List<T> data = dbConnection.Query<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 600).ToList();
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public T GetScalar<T>(string storedProcedure, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                OpenConnection();
                T data = dbConnection.ExecuteScalar<T>(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 600);
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public TResult DoTransaction<TResult>(Func<IDbTransaction, TResult> task, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Unspecified)
        {            
            using ( var transaction = dbConnection.BeginTransaction(isolationLevel) )
            {
                try
                {
                    var result = task(transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    CloseConnection();
                }

            }            
        }

        public void SaveData(string storedProcedure, DynamicParameters dynamicParameters)
        {
            try
            {
                OpenConnection();
                dbConnection.Execute(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: 600);                
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveData<T>(string storedProcedure, DynamicParameters dynamicParameters, T model)
        {
            try
            {
                OpenConnection();
                dbConnection.Execute(storedProcedure, model, commandType: CommandType.StoredProcedure, commandTimeout: 200);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveDataInTransaction<T>(string storedProcedure, DynamicParameters dynamicParameters)
        {
            try
            {
                OpenConnection();
                StartTransaction();
                dbConnection.Execute(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure, transaction:dbtransaction, commandTimeout: 200);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                RollbackTransaction();
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T model)
        {
            try
            {
                OpenConnection();
                StartTransaction();
                dbConnection.Execute(storedProcedure, model, commandType: CommandType.StoredProcedure, transaction: dbtransaction, commandTimeout: 200);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                RollbackTransaction();
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
        
        protected virtual void Dispose(bool isDispose)
        {
            if(isDispose)
            {
                CommitTransaction();
                CloseConnection();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
                
    }
}
