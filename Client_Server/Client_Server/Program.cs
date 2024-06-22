﻿using MyLibrary;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;

public class ClientServMain {
    //16.06
    static void Main(string[] args)
    {
        
            FileStream server_Client = new FileStream("G:\\C# Stude\\Sapport_project\\ClientServConfig_0.json", FileMode.Open);
            Client_Server client_Server = JsonSerializer.Deserialize<Client_Server>(server_Client);
            IPEndPoint client_server_ipEndPoint = new IPEndPoint(IPAddress.Parse(client_Server?.ipAddr), client_Server.port);
            Socket client_server_soket = new Socket(client_server_ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server_Client.Close();

            // serverConnectTo2(client_server_ipEndPoint, client_server_soket, ';'); // через текущий проект
            client_Server.serverConnectTo(client_server_ipEndPoint, client_server_soket); // через библиотеку


    }
    //TEST
    static void serverConnectTo2(IPEndPoint server_ipEndPoint, Socket server_soket, char separator)
    {
        string? dataSplit = null;
        string? result = null;
        string[]? mess = null;
        string? messPart = null;
        try
        {

            Console.WriteLine("Ждем подключения  {0}", server_ipEndPoint);
            // Начинаем слушать соединения.
            server_soket.Bind(server_ipEndPoint);
            server_soket.Listen(10);

            //соединения с клиентом
            Socket handler = server_soket.Accept();
            string? data = null;

            //Мы дождались клиента, пытающегося с нами соединиться
            Console.WriteLine("Клиент подключился к  {0}", server_ipEndPoint);

            byte[] bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes);

            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

            dataSplit = data.Substring(13, 13);

            Console.WriteLine("dataSplit = " + dataSplit);

            //messPart1 = dataSplit.Substring(0, 13);
            //messPart2 = dataSplit.Substring(26, 3);

            //Console.WriteLine("messPart1 &  messPart2 = ", messPart1 + " & " + messPart2);

            for (int i = 0; i <= 1; i++)
            {

                mess = dataSplit.Split(';');

                if (mess[i] == "NoRead")
                {
                    result = "!!!NO READ!!!";
                    break;
                }
                result = "READ";
            }


            byte[] msg = Encoding.UTF8.GetBytes(result);
            handler.Send(msg);

            //Закрытие соединения и высвобождение ресурсов
            //Сделать на всех подключения?
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

} 