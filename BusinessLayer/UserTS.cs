using DataLayer;
using System.Data;

namespace BusinessLayer
{
    public static class UserTS
    {
        public static List<User> GetUser()
        {
            List<User> users = new List<User>();
            DataTable data = UserTDG.GetAll();

            foreach (DataRow row in data.Rows)
            {
                User user = new User();

                user.Id = (int)row["id_user"];
                user.Username = (string)row["username"];
                user.Password = Encrypt((string)row["password"]);
                user.Role = (int)row["role"];

                users.Add(user);
            }

            return users;
        }

        private static char Rot13(char letter)
        {
            if (letter >= 'A' && letter <= 'Z')
            {
                letter = (char)('A' + (letter - 'A' + 13) % 26);
            }
            else if (letter >= 'a' && letter <= 'z')
            {
                letter = (char)('a' + (letter - 'a' + 13) % 26);
            }
            else if (letter >= '0' && letter <= '9')
            {
                letter = (char)('0' + (letter - '0' + 5) % 10);
            }
            return letter;
        }

        private static string Encrypt(string txt)
        {
            string result = "";

            foreach (char letter in txt)
            {
                result += Rot13(letter);
            }

            return result;
        }

        public static int Login(string username, string password)
        {
            int role = 0;

            DataTable data = UserTDG.GetByUsername(username);
            
            if (data.Rows.Count != 0)
            {
                if (data.Rows[0]["password"].ToString() == Encrypt(password))
                {
                    role = (int)data.Rows[0]["role"];

                    User user = new User();
                    user.Id = (int)data.Rows[0]["id_user"];
                    user.Username = username;
                    user.Password = password;
                    user.Role = role;

                    CurrentUser.Instance.Login(user);
                }
            }

            return role;
        }

        public static void Logout()
        {
            CurrentUser.Instance.Logout();
        }

        public static int CreateUser(string username, string password1, string password2, int role)
        {
            if (password1 == password2)
            {
                DataTable data = UserTDG.GetByUsername(username);
                
                if (data.Rows.Count == 0)
                {
                    UserTDG.CreateUser(username, Encrypt(password1), role);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }

        public static int UpdateUser(int idUser, string username, string password1, string password2, int role)
        {
            if (password1 == password2)
            {
                DataTable data = UserTDG.GetByUsername(username);

                if (data.Rows.Count == 0 || (int)data.Rows[0]["id_user"] == idUser)
                {
                    UserTDG.UpdateUser(idUser, username, Encrypt(password1), role);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }
        }

        public static int GetCurrentUserId()
        {
            return CurrentUser.Instance.User.Id;
        }
    }

    internal class CurrentUser
    {
        private static CurrentUser instance;
        public static CurrentUser Instance
        {
            get
            {
                return instance = instance ?? new CurrentUser();
            }
        }

        public User User { get; set; }
        public bool IsLoggedIn
        {
            get
            {
                return User != null;
            }
        }

        public void Login(User user)
        {
            User = user;
        }

        public void Logout()
        {
            User = null;
        }

        public CurrentUser()
        { }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}