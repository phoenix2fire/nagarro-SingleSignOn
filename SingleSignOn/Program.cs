using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleSignOn
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApplicationLogin.LoginCyberRoam();
            WebApplicationLogin.Login("http://portal/", "my_account_login", "my_account_password");
            ProcessManager.StartProcess("OUTLOOK");
            ProcessManager.StartProcess(string.Concat(Environment.GetEnvironmentVariables()["ProgramFiles"], @"\Yahoo!\Messenger\yahoomessenger.exe"));
        }
    }
}
