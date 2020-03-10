using System;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s1 = new Server();
            s1.Start();
        }
    }
}
