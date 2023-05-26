using Dapper;
using MyHome.WebAPI.Helpers;
using MyHome.WebAPI.Models;
using System.Data;

namespace MyHome.WebAPI.Business
{
    public class UtilityBL : IUtilityBL
    {
        private readonly ISqlHelper sqlHelper;
        public UtilityBL(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }
        public async Task<IEnumerable<KeyValuePairEntity>> GetListOfValues(string[] tableInfo)
        {
            string sWhereClause = string.Empty;
            string sTableName = string.Empty;
            string sDisplayValue = string.Empty;
            string sDataValue = string.Empty;
            string sExtraColumn = string.Empty;
            string sOrderByClause = string.Empty;


            List<KeyValuePairEntity> lstKeyValuePairEntity = new List<KeyValuePairEntity>();
            int inputValuesCount = tableInfo.Length;
            // DropDownArr[0] = "tblTestProduct^ID>1^Name^ID^Description^Name DESC^";
            for (int tableCount = 0; tableCount < inputValuesCount; tableCount++)
            {
                string[] spParam;

                char[] splitter = { '^' };
                spParam = tableInfo[tableCount].Split(splitter);

                if (spParam[0] != "")
                {
                    sTableName = spParam[0].ToString();
                }

                if (spParam[1] != "*")
                {
                    sWhereClause = spParam[1].ToString();
                }

                if (spParam[2] != "")
                {
                    sDisplayValue = spParam[2].ToString();
                }

                if (spParam[3] != "")
                {
                    sDataValue = spParam[3].ToString();
                }

                if (spParam[4] != "*")
                {

                    for (int extraParam = 4; extraParam < spParam.Length; extraParam++)
                    {
                        if (extraParam == 4)
                        {
                            sExtraColumn = spParam[extraParam].ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(spParam[extraParam].ToString()))
                                sExtraColumn = sExtraColumn + ", " + spParam[extraParam].ToString();

                        }

                    }
                }
                if (!string.IsNullOrEmpty(spParam[5]))
                {
                    sOrderByClause = spParam[5].ToString();
                }
                //else
                //{
                //    sOrderByClause = string.Empty;
                //}
            }

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("tableName", sTableName, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("displayValue", sDisplayValue, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("dataValue", sDataValue, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("extraColumn", sExtraColumn, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("whereClause", sWhereClause, DbType.String, ParameterDirection.Input);
            dynamicParameters.Add("OrderByClause", sOrderByClause, DbType.String, ParameterDirection.Input);

            lstKeyValuePairEntity = sqlHelper.GetData<KeyValuePairEntity>("SpGetKeyValuePair", dynamicParameters, CommandType.StoredProcedure);
            return lstKeyValuePairEntity;
        }
    }
}
