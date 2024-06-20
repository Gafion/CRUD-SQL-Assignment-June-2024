using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CRUD_SQL_Assignment_June_2024
{
    internal static class Database
    {
        private static readonly string server = "localhost";
        private static readonly string name = "repo";
        private static readonly string user = "root";
        private static readonly string pword = "";

        static readonly MySqlConnection connection = new($"SERVER={server};DATABASE={name};UID={user};PWD={pword};Convert Zero Datetime=True");

        public static void Init()
        {
            connection.Open();
        }

        public static void Close()
        {
            connection.Close();
        }

        public static void Write(string _query)
        {
            MySqlCommand cmd = new(_query, connection);
            cmd.ExecuteReader();

        }

        public static void Write1(string _query)
        {
            connection.Open();
            MySqlCommand cmd = new(_query, connection);
            cmd.ExecuteReader();
            connection.Close();

        }

        public static List<List<string>> Read1(string _query, List<string> _returns)
        {
            connection.Open();
            MySqlCommand cmd = new(_query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<List<string>> tmp1 = [];

            while (reader.Read())
            {
                List<string> tmp = [];
                foreach (string s in _returns)
                {
                    tmp.Add(reader[s]?.ToString() ?? "Not found");
                }
                tmp1.Add(tmp);
            }
            connection.Close();
            return tmp1;
        }

        public static MySqlDataReader Read(string _query)
        {
            MySqlCommand cmd = new(_query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public static List<string> GetPostalCodes()
        {
            List<List<string>> results = Read1("SELECT City FROM city", ["City"]);

            List<string> tmp = [];
            foreach (List<string> s in results)
            {
                tmp.Add(s[0]);
            }

            return tmp;
        }

        public static List<string> GetCourses()
        {
            List<List<string>> results = Read1("SELECT schoolsName FROM schools", ["schoolsName"]);

            List<string> tmp = [];
            foreach (List<string> s in results)
            {
                tmp.Add(s[0]);
            }

            return tmp;
        }

        public static List<string> GetCompanies()
        {
            List<List<string>> results = Read1("SELECT CompanyName FROM company", ["CompanyName"]);

            List<string> tmp = [];
            foreach (List<string> s in results)
            {
                tmp.Add(s[0]);
            }

            return tmp;
        }

        public static int GetJobIndex(string job)
        {
            int tmp = 0;
            Database.Init();
            MySqlDataReader reader = Database.Read($"SELECT JobID FROM jobs WHERE JobName = '{job}'");

            while (reader.Read())
            {
                tmp = (int)reader["JobID"];
            }
            Database.Close();
            return tmp;
        }

        public static int GetEducationIndex(string edu)
        {
            int tmp = 0;
            Database.Init();
            MySqlDataReader reader = Database.Read($"SELECT educationID FROM schools WHERE schoolsName = '{edu}'");

            while (reader.Read())
            {
                tmp = (int)reader["educationID"];
            }
            Database.Close();
            return tmp;
        }

        public static void AddUser(User user)
        {
            Write1($"INSERT INTO person (FirstName, LastName, PostID, Address) VALUES ('{user.FirstName}', '{user.LastName}', '{user.PostCode}', '{user.Address}');" +
                $"INSERT INTO employment (EmploymentID, CompanyID, Employed, EmployEnd) VALUES ('{user.Id}', '{user.Company}', '{user.Employed}', '{user.EmployEnd}');" + 
                $"INSERT INTO education (EducationID, CourseID, EducationEndDate) VALUES ('{user.Id}', '{user.Education}', '{user.EducationEnd}')" );
        }

        public static void RemoveUserWithID(int userID)
        {
            Write1($"DELETE FROM person WHERE PersonID = {userID};" +
                $"DELETE FROM employment WHERE EmploymentID = {userID};" +
                $"DELETE FROM education WHERE EducationID = {userID};");
        }
    }
}
