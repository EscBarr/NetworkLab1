using NetworkLab1;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        string request = "GET / HTTP/1.1\r\nHost: " + "google.com" +
            "\r\nConnection: Close\r\n\r\n";

        byte[] bytesSent = Encoding.Unicode.GetBytes(request);
        byte[] bytesReceived = new byte[1024];
        string page = "";

        IPHostEntry ipHostInfo = Dns.GetHostEntry("google.com");
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint hostep = new IPEndPoint(ipAddress, 80);
        Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        sendSocket.Connect(hostep);
        if (sendSocket.Connected)
        {
            sendSocket.Send(bytesSent, bytesSent.Length, 0);
            int bytes = 0;
            page = "Default HTML page on google.com" + ":\r\n";

            // The following will block until the page is transmitted.
            do
            {
                bytes = sendSocket.Receive(bytesReceived, bytesReceived.Length, 0);
                page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);
        }
        Console.WriteLine(page);
        sendSocket.Close();
        
        string site = "google.com";
        string dns = "1.1.1.1";
        WorkWithDns temp = new WorkWithDns();
        Console.WriteLine(temp.LookupRes(site, dns));
            
        


    }
}