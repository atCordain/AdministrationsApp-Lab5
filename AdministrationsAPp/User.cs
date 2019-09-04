using System;

namespace AdministrationsAPp
{
    internal class User
    {
        internal string userEmail;
        internal string userName;

        public User(string name, string email)
        {
            UserName = name;
            UserEmail = email;
        }

        public string UserName { get => userName; set => userName = value; }
        public string UserEmail { get => userEmail; set => userEmail = value; }
    }
}