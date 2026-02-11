using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using DrugManager.Controllers.Drug.Models;
using DrugManager.Repositories.Contracts.PagedResult;
using DrugManager.Repositories.Drug.Models;
using Oracle.ManagedDataAccess.Client;

namespace DrugManager.Repositories.Drug
{
    public class DrugRepository
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["Oracle-xe21-Training"].ConnectionString;

        public async Task<DrugEntity> GetByCodeAsync(string code)
        {
            using (var conn = new OracleConnection(_connStr))
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.BindByName = true;

                    var sb = new StringBuilder();

                    sb.AppendLine("SELECT");
                    sb.AppendLine("  DRUG_ID,");
                    sb.AppendLine("	 DRUG_CODE,");
                    sb.AppendLine("	 DRUG_NAME,");
                    sb.AppendLine("	 DRUG_NAME_EN,");
                    sb.AppendLine("	 FORM_TYPE,");
                    sb.AppendLine("	 UNIT,");
                    sb.AppendLine("	 IS_CONTROLLED");
                    sb.AppendLine("FROM DRUGS");
                    sb.AppendLine("WHERE IS_ACTIVE = 'Y'");
                    sb.AppendLine("	 AND DRUG_CODE = :DrugCode");

                    cmd.CommandText = sb.ToString();
                    cmd.Parameters.Add(new OracleParameter("DrugCode", OracleDbType.Varchar2) { Value = code });

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new DrugEntity
                            {
                                DrugId = reader.GetInt32(0),
                                DrugCode = reader.GetString(1),
                                DrugName = reader.GetString(2),
                                DrugNameEn = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                FormType = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Unit = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                IsControlled = reader.GetString(6) == "Y"
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<bool> CreateDrugAsync(CreateDrugRequestModel req)
        {
            using (var conn = new OracleConnection(_connStr))
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.BindByName = true;

                    var sb = new StringBuilder();

                    sb.AppendLine("INSERT INTO DRUGS");
                    sb.AppendLine("(");
                    sb.AppendLine("	 DRUG_CODE,");
                    sb.AppendLine("	 DRUG_NAME,");
                    sb.AppendLine("	 DRUG_NAME_EN,");
                    sb.AppendLine("	 FORM_TYPE,");
                    sb.AppendLine("	 UNIT,");
                    sb.AppendLine("	 IS_CONTROLLED");
                    sb.AppendLine(")");
                    sb.AppendLine("VALUES");
                    sb.AppendLine("(");
                    sb.AppendLine("	 :DrugCode,");
                    sb.AppendLine("	 :DrugName,");
                    sb.AppendLine("	 :DrugNameEn,");
                    sb.AppendLine("	 :FormType,");
                    sb.AppendLine("	 :Unit,");
                    sb.AppendLine("	 :IsControlled");
                    sb.AppendLine(")");

                    cmd.CommandText = sb.ToString();

                    cmd.Parameters.Add(new OracleParameter("DrugCode", OracleDbType.Varchar2) {Value = req.DrugCode});

                    cmd.Parameters.Add(new OracleParameter("DrugName", OracleDbType.Varchar2) {Value = req.DrugName});

                    cmd.Parameters.Add(new OracleParameter("DrugNameEn", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.DrugNameEn)
                            ? DBNull.Value
                            : (object)req.DrugNameEn
                    });

                    cmd.Parameters.Add(new OracleParameter("FormType", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.FormType)
                            ? DBNull.Value
                            : (object)req.FormType
                    });

                    cmd.Parameters.Add(new OracleParameter("Unit", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.Unit)
                            ? DBNull.Value
                            : (object)req.Unit
                    });

                    cmd.Parameters.Add(new OracleParameter("IsControlled", OracleDbType.Varchar2)
                    {
                        Value = req.IsControlled ? "Y" : "N"
                    });

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateDrugByCodeAsync(string code, UpdateDrugRequestModel req)
        {
            using (var conn = new OracleConnection(_connStr))
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.BindByName = true;

                    var sb = new StringBuilder();

                    sb.AppendLine("UPDATE DRUGS SET");
                    sb.AppendLine("	 DRUG_NAME = :DrugName,");
                    sb.AppendLine("	 DRUG_NAME_EN = :DrugNameEn,");
                    sb.AppendLine("	 FORM_TYPE = :FormType,");
                    sb.AppendLine("	 UNIT = :Unit,");
                    sb.AppendLine("	 IS_CONTROLLED = :IsControlled,");
                    sb.AppendLine("	 UPDATED_AT = SYSTIMESTAMP");
                    sb.AppendLine("WHERE DRUG_CODE = :DrugCode");

                    cmd.CommandText = sb.ToString();

                    cmd.Parameters.Add(new OracleParameter("DrugName", OracleDbType.Varchar2)
                    {
                        Value = req.DrugName
                    });

                    cmd.Parameters.Add(new OracleParameter("DrugNameEn", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.DrugNameEn)
                            ? DBNull.Value
                            : (object)req.DrugNameEn
                    });

                    cmd.Parameters.Add(new OracleParameter("FormType", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.FormType)
                            ? DBNull.Value
                            : (object)req.FormType
                    });

                    cmd.Parameters.Add(new OracleParameter("Unit", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(req.Unit)
                            ? DBNull.Value
                            : (object)req.Unit
                    });

                    cmd.Parameters.Add(new OracleParameter("IsControlled", OracleDbType.Varchar2)
                    {
                        Value = req.IsControlled ? "Y" : "N"
                    });

                    cmd.Parameters.Add(new OracleParameter("DrugCode", OracleDbType.Varchar2)
                    {
                        Value = string.IsNullOrWhiteSpace(code)
                            ? DBNull.Value
                            : (object)code
                    });

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteDrugByCodeAsync(string code)
        {
            using (var conn = new OracleConnection(_connStr))
            {
                await conn.OpenAsync();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.BindByName = true;

                    var sb = new StringBuilder();

                    sb.AppendLine("UPDATE DRUGS SET");
                    sb.AppendLine("	IS_ACTIVE = 'N',");
                    sb.AppendLine("	DELETED_AT = SYSTIMESTAMP");
                    sb.AppendLine("WHERE DRUG_Code = :DrugCode");

                    cmd.CommandText = sb.ToString();

                    cmd.Parameters.Add(new OracleParameter("DrugCode", OracleDbType.Varchar2)
                    {
                        Value = code
                    });

                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
