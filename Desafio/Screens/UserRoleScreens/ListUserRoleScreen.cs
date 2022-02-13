using System;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserRoleScreens
{
    public static class ListUserRoleScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de User x Roles");
            Console.WriteLine("---------------------");
            List();
            Console.ReadKey();
            MenuUserRoleScreen.Load();
        }

        private static void List()
        {
            var repository = new UserRepository(Database.Connection);
            var UserRoles = repository.GetWithRoles();
            foreach (var item in UserRoles){
                Console.WriteLine($"{item.Id}   - {item.Name}       {item.Email}    {item.Bio}      ({item.Slug})");
                foreach(var userRole in item.Roles)
                {
                    Console.WriteLine($"- {userRole.Name}   ({userRole.Slug})");
                }
            }
        }
    }
}