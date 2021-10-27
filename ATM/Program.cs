using System;

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

            app.Start("C:\\.Training\\manifesto\\ATM\\Inputs\\input1.txt");
        }
    }
}
