
using System;

namespace Qupla.IndicatorServer.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new Server.Server();
                server.Start();

                Console.WriteLine("Server started, press any key to quit");
                Console.ReadKey();

                server.Stop();
            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                logger.Fatal(e);
                Console.ReadKey();
            }
        }
    }
}
