using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        delegate void ConnectDelegate(Socket os);
        delegate void StartNetwork(Socket os);
        private IPEndPoint endP;
        private Socket socket;
        private string index;

        public Server(string strAddres, int point)
        {
            endP = new IPEndPoint(IPAddress.Parse(strAddres), point);
        }
        public void Start()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Bind(endP);
            socket.Listen(10);
            StartNetwork start = new StartNetwork(Server_Begin);
            start.BeginInvoke(socket, null, null);
        }
        private void Server_Begin(Socket s)
        {
            int i;
            while (true)
            {
                try
                {
                    Socket ns = s.Accept();
                    byte[] buffer = new byte[1024];
                    i = ns.Receive(buffer);
                    index = Encoding.Unicode.GetString(buffer, 0, i);
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    ConnectDelegate cd = new ConnectDelegate(Server_Connect);
                    cd.BeginInvoke(ns, null, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
        }
        private void Server_Connect(Socket s)
        {
            string str = null;
            if (XMLfile.Read_xmlfiel(index).Count != 0)
            {
                foreach (string VARIABLE in XMLfile.Read_xmlfiel(index))
                {
                    str += VARIABLE;
                    str += "*";
                }

            }
            else
            {
                str = "";
            }
            s.Send(Encoding.Unicode.GetBytes(str.ToString()));
            s.Shutdown(SocketShutdown.Both);
            s.Close();
        }

        public void Stop()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
