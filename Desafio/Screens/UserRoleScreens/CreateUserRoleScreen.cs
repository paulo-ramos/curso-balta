using System;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserRoleScreens
{
    public static class CreateUserRoleScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Novo User x Role");
            Console.WriteLine("----------------");
            
            Console.Write("UserId: ");
            var user = Console.ReadLine();

            Console.Write("RoleId: ");
            var role = Console.ReadLine();

            int userId, roleId = 0;

            bool res = (int.TryParse(user, out userId) && int.TryParse(role, out roleId) );
            if (res == false)
            {
                Console.WriteLine("Valores (UserId / RoleId) informados não são válidos como número!");
                Console.WriteLine("Tente novamente!");
                Console.ReadKey();
                MenuUserRoleScreen.Load();
            }

            Create(userId, roleId);
            Console.ReadKey();
            MenuUserRoleScreen.Load();
        }

        public static void Create(int userId, int roleId)
        {
            var userRepository = new Repository<User>(Database.Connection);
            var user = userRepository.Get(userId);

            if (user == null){
                Console.WriteLine($"Usuário informado não está casdastrado na base de dados:{userId}");
                Console.WriteLine("Tente novamente!");
                Console.ReadKey();
                MenuUserRoleScreen.Load();
            } else{
                Console.WriteLine($"Usuário selecionado:{userId}    {user.Name}");
            }

            var roleRepository = new Repository<Role>(Database.Connection);
            var role = roleRepository.Get(roleId);

            if (role == null){
                Console.WriteLine($"Role informada não está casdastrado na base de dados:{roleId}");
                Console.WriteLine("Tente novamente!");
                Console.ReadKey();
                MenuUserRoleScreen.Load();
            } else{
                Console.WriteLine($"Role  selecionada:{roleId}    {role.Name}");
            }

            var userRoleRepository = new UserRepository(Database.Connection);
            var userRoleExists = userRoleRepository.GetWithRoles(userId, roleId);

            if (userRoleExists.Count>0){
                Console.WriteLine($"Role ({roleId}) informada para o Usuário ({userId}) JÁ está associada!");
                Console.WriteLine("Tente novamente!");
                Console.ReadKey();
                MenuUserRoleScreen.Load();
            }

            Console.Write("Confirma inclusão (s/n)? ");
            var confirmation = Console.ReadLine();

            int row = 0;

            if (confirmation.ToUpper() == "S") 
            {
                var userRoleRep = new UserRepository(Database.Connection);
                row = userRoleRep.AddUserRole(user, role);
            }

            if( row > 0) {
                Console.WriteLine("UserRole cadastrado com sucesso!");
            }else
            {
                Console.WriteLine("Não foi possível salvar o UserRole");
            }

            Console.ReadKey();
            MenuUserRoleScreen.Load();

        }
    }
}