using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("192.168.0.107", 1024);
            server.Start();
            Console.ReadLine();
            server.Stop();
        }
    }
}
