using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace DZ1.Models
{
    public class Singelton
    {
        private static Singelton _instens;
        public static Singelton Instens => _instens ?? (_instens = new Singelton());

        public Singelton()
        {
            //Index_Name(Client("index"));
        }
        public List<Index> IndeList = new List<Index>();
        public List<Street> StreetList = new List<Street>();
        public List<Index> Indices => IndeList;
        public List<Street> Streets => StreetList;

        private void Add_Index(string str)
        {
            Index ind = new Index();
            ind.Name_Index = str;
            ind.Id = (IndeList.LastOrDefault()?.Id ?? 0) + 1;
            IndeList.Add(ind);
        }

        private void Add_Street(string str)
        {
            Street street = new Street
            {
                Name = str
            };
            StreetList.Add(street);
        }

        public string Client(string strSend)
        {
            IPAddress ip = IPAddress.Parse("192.168.0.107");
            IPEndPoint endP = new IPEndPoint(ip, 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            string str = null;
            try
            {
                socket.Connect(endP);
                if (socket.Connected)
                {

                    socket.Send(System.Text.Encoding.Unicode.GetBytes(strSend));
                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = socket.Receive(buffer);
                        str += Encoding.Unicode.GetString(buffer, 0, l);
                    } while (l > 0);
                }
                return str;
            }
            catch (Exception e)
            {
                //Console.WriteLine("Не вышло!!!");
                //Console.WriteLine(e.Message);

            }

            return str;
        }

        public void Index_Name(string str)
        {
            string[] name = str.Split('*');
            IndeList.Clear();
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != "")
                {
                    Add_Index(name[i]);
                }
            }
        }
        public void Street_Name(string str)
        {
            string[] name = str.Split('*');
            StreetList.Clear();
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != "")
                {
                    Add_Street(name[i]);
                }
            }
        }
    }
}