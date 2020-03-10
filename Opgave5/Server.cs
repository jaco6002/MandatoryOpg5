using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TCPServer
{
    public class Server
    {
        private static List<Book> books = new List<Book>()
        {
            new Book("tit1","auth1",123,"1234567890123"),
            new Book("tit2","auth2",120,"123456ab90123"),
            new Book("tit3","auth3",12,"12345ok890123"),
            new Book("tit4","auth4",13,"1kkk567890123")
        };

        public void Start()
        {
            IPAddress localIp = IPAddress.Parse("192.168.24.105");
            TcpListener listener = new TcpListener(localIp, 3001);
            listener.Start();
            Console.WriteLine("Server Started");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Connected: " + socket.Client.RemoteEndPoint);
                Task.Run(() =>
                {

                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
        }

        public void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            var reader = new StreamReader(ns);
            var writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            string message = reader.ReadLine();

            while (message != null && message != "")
            {
                string[] messageArray = message.Split(' ');
                string param = message.Substring(message.IndexOf(' ') + 1);
                string command = messageArray[0];

                switch (command)
                {
                    case "GetAll":
                        writer.WriteLine(JsonConvert.SerializeObject(books));
                        break;

                    case "Get":
                        writer.WriteLine(books.Find(b => b.Isbn13 == param));
                        break;

                    case "Save":
                        Book savedBook = JsonConvert.DeserializeObject<Book>(param);
                        books.Add(savedBook);
                        break;
                }

                message = reader.ReadLine();
            }
        }
    }
}