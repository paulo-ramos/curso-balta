using System;

namespace Blog.Screens.CategoryScreens
{
    public static class MenuCategoryScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Gestão de Categorys");
            Console.WriteLine("-------------------");
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Listar Categorys");
            Console.WriteLine("2 - Cadastrar Categorys");
            Console.WriteLine("3 - Atualizar Category");
            Console.WriteLine("4 - Excluir Category");
            Console.WriteLine("0 - Voltar");
            Console.WriteLine();
            Console.WriteLine();
            var option = short.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    ListCategoryScreen.Load();
                    break;
                case 2:
                    CreateCategoryScreen.Load();
                    break;
                case 3:
                    UpdateCategoryScreen.Load();
                    break;
                case 4:
                    DeleteCategoryScreen.Load();
                    break;
                default: Program.Load(); break;
            }
        }
    }
}