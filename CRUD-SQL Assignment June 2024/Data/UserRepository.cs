using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    public static class UserRepository
    {
        public static readonly List<User> users = [];

       

        public static List<User> GetAllUsers()
        {
            return users;
        }


        

    }
}
