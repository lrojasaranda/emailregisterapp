using Dapper;
using EmailRegisterApp.DB.Entity;
using EmailRegisterApp.Helper;
using EmailRegisterApp.Helper.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmailRegisterApp.DB
{
    public class UserPersistence
    {
        public void Insert(UserEntity entity) {
            using (IDbConnection cnn = new SqlConnection(Keys.GetSqlConexion()))
            {
                cnn.Open();
                using (var tran = cnn.BeginTransaction())
                {
                    try
                    {
                        var p =new DynamicParameters();
                        p.Add($"@iv{Helpers.getPropertyName(() => (entity.Name))}", entity.Name, DbType.String);
                        p.Add($"@iv{Helpers.getPropertyName(() => (entity.Email))}", entity.Email, DbType.String);
                        p.Add($"@iv{Helpers.getPropertyName(() => (entity.Company))}", entity.Company, DbType.String);
                        p.Add($"@oi{Helpers.getPropertyName(() => (entity.UserId))}", entity.UserId, DbType.Int32, ParameterDirection.Output);
                        p.Add(DB.Parameter.NumeroError, null, DbType.Int16, ParameterDirection.Output);
                        p.Add(DB.Parameter.MensajeError, null, DbType.String, ParameterDirection.Output, 2000);

                        cnn.Execute(DB.StoreProcedure.UserInsert, p, tran, null, CommandType.StoredProcedure);
                        entity.UserId = p.Get<int>($"@oi{Helpers.getPropertyName(() => (entity.UserId))}");
                        var error = p.Get<string>(DB.Parameter.MensajeError);
                        if (error != null)
                            throw new SaveException(error);
                      
                            foreach (var email in entity.Emails)
                            {
                            if(Exists(email.Email))
                                throw new SaveException("Ya existe email : " + email.Email);
                            var pd = new DynamicParameters();
                            pd.Add($"@ii{Helpers.getPropertyName(() => (entity.UserId))}", entity.UserId, DbType.Int32);
                            pd.Add($"@iv{Helpers.getPropertyName(() => (email.Email))}", email.Email, DbType.String);
                                pd.Add($"@iv{Helpers.getPropertyName(() => (email.Motive))}", email.Motive, DbType.String);

                                pd.Add(DB.Parameter.NumeroError, null, DbType.Int16, ParameterDirection.Output);
                                pd.Add(DB.Parameter.MensajeError, null, DbType.String, ParameterDirection.Output, 2000);
                                cnn.Execute(DB.StoreProcedure.EmailInsert, pd, tran, null, CommandType.StoredProcedure);
                                var emailError = pd.Get<string>(DB.Parameter.MensajeError);
                            if (emailError != null)
                                throw new SaveException(emailError);
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }

            }
        }

        public bool Exists(string email) {
            using (IDbConnection cnn = new SqlConnection(Keys.GetSqlConexion()))
            {
                cnn.Open();
                var p = new DynamicParameters();
                p.Add($"@iv{Helpers.getPropertyName(() => (email))}", email, DbType.String);
                p.Add(DB.Parameter.NumeroError, null, DbType.Int16, ParameterDirection.Output);
                p.Add(DB.Parameter.MensajeError, null, DbType.String, ParameterDirection.Output, 2000);

                var doc = cnn.ExecuteScalar<string>(DB.StoreProcedure.EmailExists, p, commandType: CommandType.StoredProcedure);
                cnn.Close();
                return !string.IsNullOrEmpty(doc);
            }
        }
    }
}