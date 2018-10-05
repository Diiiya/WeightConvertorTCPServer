using Convertor3;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPServer3
{
    class Program
    {
        private static readonly int PORT = 10;

        static void Main(string[] args)
        {
            IPAddress localAddress = IPAddress.Loopback;
            TcpListener serverSocket = new TcpListener(localAddress, PORT);
            serverSocket.Start();
            Console.WriteLine("The TCP server is ready!");
            Console.WriteLine("Running on port " + PORT);
            while (true)
            {
                TcpClient client = serverSocket.AcceptTcpClient();
                Console.WriteLine("Incoming client ... ");
                Task.Run(() => DoIt(client));
            }
        }

        private static void DoIt(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            while (true)
            {
                string request = reader.ReadLine();
                if (request == "Stop") { break; }
                else
                {
                    Console.WriteLine("Request: " + request);
                    string[] values = request.Split(' ');
                    string funcion = (string)values[0];

                    double _response = 0;

                    double a = Double.Parse(values[1]);

                    ConvertorClass c = new ConvertorClass();

                    if (funcion == "ToOunces")
                    {
                        _response = c.ConvertToOunces(a);
                    }
                    else if (funcion == "ToGrams")
                    {
                        _response = c.ConvertToGrams(a);
                    }

                    Console.WriteLine("Response: " + _response);
                    writer.WriteLine(_response);
                    writer.Flush();
                }
            }
            client.Close();
        }
    }
}
