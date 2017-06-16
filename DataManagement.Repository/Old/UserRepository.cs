using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DataManagement.Entities;
using DataManagement.Repository.Interfaces;
using System.Linq;

namespace DataManagement.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public bool AddUser(User user)
        {
            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@UserMobile", user.UserMobile);
                parameters.Add("@UserEmail", user.UserEmail);
                parameters.Add("@FaceBookUrl", user.FaceBookUrl);
                parameters.Add("@LinkedInUrl", user.LinkedInUrl);
                parameters.Add("@TwitterUrl", user.TwitterUrl);
                parameters.Add("@PersonalWebUrl", user.PersonalWebUrl);
                SqlMapper.Execute(con, "AddUser", param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", user.UserId);
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@UserMobile", user.UserMobile);
                parameters.Add("@UserEmail", user.UserEmail);
                parameters.Add("@FaceBookUrl", user.FaceBookUrl);
                parameters.Add("@LinkedInUrl", user.LinkedInUrl);
                parameters.Add("@TwitterUrl", user.TwitterUrl);
                parameters.Add("@PersonalWebUrl", user.PersonalWebUrl);
                SqlMapper.Execute(con, "UpdateUser", param:parameters, commandType:System.Data.CommandType.StoredProcedure);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteUser(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            SqlMapper.Execute(con, "DeleteUser", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return true;
        }

        public IList<User> GetAllUser()
        {
            IList<User> customerList = SqlMapper.Query<User>(con, "GetAllUsers", commandType: System.Data.CommandType.StoredProcedure).ToList();
            return customerList;
        }
        public User GetUserById(int userId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                return SqlMapper.Query<User>(con, "GetUserById", param:parameters, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}
