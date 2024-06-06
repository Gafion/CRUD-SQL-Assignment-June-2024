using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    public class UserRepository
    {
        private readonly List<User> users = [];

        public UserRepository()
        {
            AddUser("Christian", "Colberg", "Trønninge By 14", "Regstrup", "4420", "Datatekniker", "Current", "ZBC", "Feb 19 - 2024", "Current");
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public void AddUser(string firstName, string lastName, string address, string city, string postCode, string Education, string educationEnd, string company, string employed, string employEnd)
        {
            int newId = users.Count != 0 ? users.Max(u => u.Id) + 1 : 1;
            User newUser = new()
            {
                Id = newId,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                PostCode = postCode,
                Education = Education,
                EducationEnd = educationEnd,
                Company = company,
                Employed = employed,
                EmployEnd = employEnd
    };

            users.Add(newUser);
        }

        public bool RemoveUser(int id)
        {
            int removedCount = users.RemoveAll(u => u.Id == id);
            return removedCount > 0;
        }

    }
}
