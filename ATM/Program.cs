using System;
using System.Reflection;

namespace ATM_PROJ
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            var path = Assembly.GetExecutingAssembly().Location;

            if (args.Length != 0)
            {
                app.Start(path + "..\\..\\..\\..\\..\\" + args[0]);
            }
            else
            {
                app.Start();
            }
        }
    }
}
