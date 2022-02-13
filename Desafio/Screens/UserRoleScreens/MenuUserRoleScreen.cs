using System;

namespace Blog.Screens.UserRoleScreens
{
    public static class MenuUserRoleScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Gestão de User x Roles");
            Console.WriteLine("----------------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar User x Roles");
            Console.WriteLine("2 - Cadastrar User x Roles");
            Console.WriteLine("3 - Excluir User x Role");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine();
            Console.WriteLine();
            var option = short.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ListUserRoleScreen.Load();
                    break;
                case 2:
                    CreateUserRoleScreen.Load();
                    break;
                case 3:
                    DeleteUserRoleScreen.Load();
                    break;
                default: Program.Load(); break;
            }
        }
    }
}