using Dapper;
using MonitsRequest.Core.Interfaces.Repository;
using MonitsRequest.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MonitsRequest.Core.Repositories
{
    public class HealthCheckResultRepository : RepositoryBase<HealthCheckResult>, IHealthCheckResultRepository
    {
        public HealthCheckResultRepository(IDbConnection connection) : base(connection)
        {
        }

        public override bool Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public override HealthCheckResult Get(Guid key)
        {
            throw new NotImplementedException();
        }

        public override HealthCheckResult Insert(HealthCheckResult entity)
        {
            try
            {
                _connection.Open();
                _connection.Execute("INSERT INTO HealthCheckResult (HealthCheckResultKey, HealthCheckKey, RequestAt, ResponseAt, StatusCode, ContentResult, ResponseInMilliseconds) VALUES (@HealthCheckResultKey, @HealthCheckKey, @RequestAt, @ResponseAt, @StatusCode, @ContentResult, @ResponseInMilliseconds)", entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public override HealthCheckResult Update(HealthCheckResult entity)
        {
            throw new NotImplementedException();
        }
    }
}
