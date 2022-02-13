using System;
using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserRoleScreens
{
    public static class DeleteUserRoleScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Delete User x Role");
            Console.WriteLine("------------------");
            
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

            Delete(userId, roleId);
            Console.ReadKey();
            MenuUserRoleScreen.Load();
        }

        public static void Delete(int userId, int roleId)
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

            if (userRoleExists.Count==0){
                Console.WriteLine($"Role ({roleId}) informada para o Usuário ({userId}) NÃO está associada!");
                Console.WriteLine("Tente novamente!");
                Console.ReadKey();
                MenuUserRoleScreen.Load();
            }

            Console.Write("Confirma exclusão (s/n)? ");
            var confirmation = Console.ReadLine();

            int row = 0;

            if (confirmation.ToUpper() == "S") 
            {
                var userRoleRep = new UserRepository(Database.Connection);
                row = userRoleRep.DeleteWithRoles(user, role);
            }

            if( row > 0) {
                Console.WriteLine("Associação User x Role deletada com sucesso!");
            }else
            {
                Console.WriteLine("Não foi possível deletar a associação User x Role");
            }

            Console.ReadKey();
            MenuUserRoleScreen.Load();

        }
    }
}