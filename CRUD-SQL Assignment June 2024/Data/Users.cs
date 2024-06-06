using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_SQL_Assignment_June_2024
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostCode{ get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string EducationEnd { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Employed { get; set; } = string.Empty;
        public string EmployEnd { get; set; } = string.Empty;
    }
}
