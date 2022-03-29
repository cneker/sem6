using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sever
{
    internal class Program
    {
        private static int _port = 35768;
        private static List<Socket> _clients = new List<Socket>();

        private static int _turn;
        private static string _number;

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, _port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipPoint);
            listenSocket.Listen(10);

            _number = CreateStartNumbers();

            Console.WriteLine("Server was started...");
            while (true)
            {
                Socket handler = listenSocket.Accept();
                if (_clients.Count < 2)
                {
                    Console.WriteLine("New connection!");
                    var data = Encoding.Unicode.GetBytes(_number);
                    handler.Send(data);
                    _clients.Add(handler);
                }

                if (_clients.Count == 2)
                {
                    Console.WriteLine("Game started!!!");

                    ChooseTheFirst();

                    ListenForMessage();
                }
            }
        }

        static void ChooseTheFirst()
        {
            Random rand = new Random();
            _turn = rand.Next(0, 2);
            Console.WriteLine($"First: {_turn}");
            var data = Encoding.Unicode.GetBytes("go");
            _clients[_turn].Send(data);
            data = Encoding.Unicode.GetBytes("wait");
            _clients[1^_turn].Send(data);
        }

        static string CreateStartNumbers()
        {
            var rand = new Random();
            string numbers = string.Empty;
            for (int i = 0; i < 20; i++)
            {
                numbers += rand.Next(0, 10) + ";";
            }
            return numbers;
        }

        static void SendMessageToClients(string message, Socket sender)
        {
            var data = Encoding.Unicode.GetBytes(message);
            var receivers = _clients.Where(c => c != sender).ToList();

            foreach (var receiver in receivers)
            {
                Console.WriteLine($"Send data: {message}");
                receiver.Send(data);
            }
        }

        static void GetMessage(Socket client, out StringBuilder message)
        {
            var data = new byte[256];
            message = new StringBuilder();
            var bytes = client.Receive(data, data.Length, 0);
            message.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }

        private static void ListenForMessage()
        {
            while (true)
            {
                foreach (var client in _clients)
                {
                    if (client.Available > 0)
                    {
                        GetMessage(client, out StringBuilder builder);
                        Console.WriteLine($"Get data: {builder}");

                        SendMessageToClients(builder.ToString(), client);
                        builder.Clear();
                    }
                }
            }
        }
    }
}
