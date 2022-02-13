using System;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.RoleScreens
{
    public static class ListRoleScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de Roles");
            Console.WriteLine("-------------");
            List();
            Console.ReadKey();
            MenuRoleScreen.Load();
        }

        private static void List()
        {
            var repository = new Repository<Role>(Database.Connection);
            var Roles = repository.Get();
            foreach (var item in Roles)
                Console.WriteLine($"{item.Id} - {item.Name} ({item.Slug})");
        }
    }
}