using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
        : base(connection)
            => _connection = connection;


        public int AddUserRole(User user, Role role){

            var insertSql = @"
                INSERT 
                INTO [UserRole] ([UserRole].[UserId], [UserRole].[RoleId])
                VALUES (@UserId, @RoleId) ";

            var rows = _connection.Execute(insertSql, new
            {
                UserId = user.Id,
                RoleId = role.Id
            });         

            return rows;
        }

        public List<User> GetWithRoles()
        {
            var query = @"
                SELECT
                    [User].*,
                    [Role].*
                FROM
                    [User]
                    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

            var users = new List<User>();

            var items = _connection.Query<User, Role, User>(
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;
                        if (role != null)
                            usr.Roles.Add(role);

                        users.Add(usr);
                    }
                    else
                        usr.Roles.Add(role);

                    return user;
                }, splitOn: "Id");

            return users;
        }

        public List<User> GetWithRoles(int userId, int roleId)
        {
            var query = $@"
                SELECT
                    [User].*,
                    [Role].*
                FROM
                    [User]
                    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]
                WHERE [UserRole].[UserId] = {userId}
                AND   ({roleId} = 0 OR [UserRole].[RoleId] = {roleId})";

            var users = new List<User>();

            var items = _connection.Query<User, Role, User>(
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;
                        if (role != null)
                            usr.Roles.Add(role);

                        users.Add(usr);
                    }
                    else
                        usr.Roles.Add(role);

                    return user;
                }, splitOn: "Id");

            return users;
        }


        public int DeleteWithRoles(User user, Role role)
        {
            var query = $@"
                DELETE [UserRole]
                WHERE [UserRole].[UserId] = {user.Id}
                AND   [UserRole].[RoleId] = {role.Id}";

            return _connection.Execute(query);
        }



    }
}