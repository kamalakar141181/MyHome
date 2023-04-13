using Dapper;
using Microsoft.Data.SqlClient;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Helpers;
using MyHome.WebAPI.Models;
using System;
using System.Data;

namespace MyHome.WebAPI.Business
{
    public class TestProductBLUsingADODotNet : ITestProductBLUsingADODotNet
    {
        private readonly ISqlHelper _sqlHelper;
        public TestProductBLUsingADODotNet(ISqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public async Task<int> AddTestProduct(TestProductEntity testProductEntity)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var result = 0;

            if (!string.IsNullOrEmpty(testProductEntity.Name))
            {
                dynamicParameters.Add("Name", testProductEntity.Name, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(testProductEntity.Description))
            {
                dynamicParameters.Add("Description", testProductEntity.Description, DbType.String, ParameterDirection.Input);
            }
            try
            {
                _sqlHelper.SaveData("spAddTestProduct", dynamicParameters);
                result = 1;
            }
            catch (Exception)
            {

                throw;
            }

            
            return result;
        }

        public async Task<IEnumerable<TestProductEntity>> GetTestProductByID(int testProductID)
        {
            List<TestProductEntity> lstTestProductEntity = new List<TestProductEntity>();
            DynamicParameters dynamicParameters = new DynamicParameters();
            if (testProductID > 0)
            {
                dynamicParameters.Add("ID", testProductID, DbType.Int32, ParameterDirection.Input);
            }
            lstTestProductEntity = _sqlHelper.GetData<TestProductEntity>("spGetTestProductByID", dynamicParameters, CommandType.StoredProcedure);
            return lstTestProductEntity;
        }

        public async Task<List<TestProductEntity>> GetTestProductList()
        {
            List<TestProductEntity> lstTestProductEntity = new List<TestProductEntity>();
            lstTestProductEntity = _sqlHelper.GetData<TestProductEntity>("spGetTestProductList");
            return lstTestProductEntity;
        }

        public async Task<int> UpdateTestProduct(TestProductEntity testProductEntity)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            var result = 0;

            if (testProductEntity.ID > 0)
            {
                dynamicParameters.Add("ID", testProductEntity.ID, DbType.Int64, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(testProductEntity.Name))
            {
                dynamicParameters.Add("Name", testProductEntity.Name, DbType.String, ParameterDirection.Input);
            }

            if (!string.IsNullOrEmpty(testProductEntity.Description))
            {
                dynamicParameters.Add("Description", testProductEntity.Description, DbType.String, ParameterDirection.Input);
            }
            try
            {
                _sqlHelper.SaveData("spUpdateTestProduct", dynamicParameters);
                result = 1;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
            
        }

        public async Task<int> DeleteTestProduct(int testProductID)
        {
            var result = 0;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", testProductID, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                _sqlHelper.SaveData("spDeleteTestProduct", dynamicParameters);
                result = 1;
            }
            catch (Exception)
            {

                throw;
            }
            
            return result;
        }
    }
}
