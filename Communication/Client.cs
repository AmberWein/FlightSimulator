using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Communication
{
    class Client
    {
        private IPAddress ipAddress;
        private int port;
        private IPEndPoint localEndPoint;
        private Socket sender;
        public Client()
        {
            ipAddress = IPAddress.Parse("127.0.0.1");
            port = 5404;
            localEndPoint = new IPEndPoint(ipAddress, port);
            // try?
            sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        //return 1 if connected
        public int Connect()
        {
            try
            {
                if (sender.Connected)
                    return 1;
                sender.Connect(localEndPoint);
                if (sender.Connected)
                    return 1;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                return 0;
            }
            return 0;
        }

        public void Send(string msg)
        {
            byte[] messageSent = Encoding.ASCII.GetBytes(msg + '\n');
            int byteSent = sender.Send(messageSent);
        }

        public void Disconnect()
        {
            // Close Socket using // the method Close()
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
