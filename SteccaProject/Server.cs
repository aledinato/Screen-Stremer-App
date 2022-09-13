using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;

namespace SteccaProject
{
    class Server
    {
        private int knownPort = 11002;
        private IPAddress ipServer;
        private IPEndPoint localEndPoint;
        private IPEndPoint remoteEndPoint;
        private UdpClient talker;
        private Graphics g;
        private Thread sender;

        public Server(Graphics g)
        {
            ipServer = IPAddress.Parse("127.0.0.1");
            localEndPoint=new IPEndPoint(ipServer, knownPort);
            this.g = g;
            sender = new Thread(CondividiSchermo);
            CreaHost();
        }

        public async void CreaHost()
        {
            talker = new UdpClient(localEndPoint);
            var data = await talker.ReceiveAsync();
            if (Encoding.ASCII.GetString(data.Buffer) == "?")
            {
                remoteEndPoint = data.RemoteEndPoint;
                try
                {
                    //talker.Client.Connect(remoteEndPoint);
                    //byte[] confirm = Encoding.ASCII.GetBytes("OK");
                    //talker.Send(confirm, confirm.Length, remoteEndPoint);
                    //EndPoint ip = talker.Client.RemoteEndPoint;
                    sender.Start();
                }
                catch (Exception e)
                {
                    string x = e.Message;
                }
            }
        }

        public (byte[][],int) ArrToMat(Bitmap bm)
        {
            ImageConverter im = new ImageConverter();
            byte[] arr = (byte[])im.ConvertTo(bm, typeof(byte[]));
            int i = 0;  
            int j = 0;
            int capacity = 65000;
            int count=0;
            int nRighe = (int) Math.Ceiling(arr.Length / Convert.ToDouble(capacity));
            byte[][] mat = new byte[nRighe][];
            while (i < nRighe)
            {
                if (arr.Length- i * capacity < capacity)
                { capacity = arr.Length % capacity; }
                mat[i] = new byte[capacity];
                while (j < capacity&&count<arr.Length)
                {
                    mat[i][j] = arr[count];
                    j++;
                    count++;
                }
                j = 0;
                i++;
            }
            return (mat,count);
        }

        public void CondividiSchermo()
        {
            Bitmap bm;
            int o = 0;
            while (o==0)
            {
                bm = Registratore.DammiSchermata(g);
                var response = ArrToMat(bm);
                byte[][] mat=response.Item1;
                int countItems = response.Item2;
                byte[] cByte = BitConverter.GetBytes(countItems);
                try
                {
                    talker.Send(cByte,cByte.Length,remoteEndPoint);
                    for (int i=0;i<mat.Length;i++)
                    {
                        talker.Send(mat[i], mat[i].Length,remoteEndPoint);
                    }
                }
                catch(Exception e)
                { string x=e.Message; }
                Thread.Sleep(50);
            }
        }

    }
}
