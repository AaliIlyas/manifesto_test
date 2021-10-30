using System;
using System.Reflection;

namespace ATM_PROJ
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            
            if (args.Length != 0)
            {
                app.Start(args[0]);
            }

            var path = Assembly.GetExecutingAssembly().Location;
            app.Start(path + "..\\..\\..\\..\\..\\Inputs\\input2.txt");
        }
    }
}
