using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            /*
            testovací přihlášení
                       username:   leeroy
                       heslo:      jenkins
            */
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
        }
    }
}
