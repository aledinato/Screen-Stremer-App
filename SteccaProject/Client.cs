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
    class Client
    {
        private int knownPort = 11002;
        private IPEndPoint remoteServer;
        private UdpClient listener;

        public async Task<bool> CreaClient()
        {
            dynamic data;
            IPAddress ipServer = IPAddress.Parse("127.0.0.1");
            remoteServer = new IPEndPoint(ipServer, knownPort);
            listener = new UdpClient();

            byte[] ask = Encoding.ASCII.GetBytes("?");
            listener.Send(ask, ask.Length, remoteServer);
            try
            { data = await listener.ReceiveAsync(); }
            catch
            { return false; }
            if (Encoding.ASCII.GetString(data.Buffer == "OK"))
            { return true; }
            else
            { return false; }
        }

        public async Task<Bitmap> AcquisisciSchermo()
        {
            ImageConverter im = new ImageConverter();
            var data = await listener.ReceiveAsync();
            byte[] imageByte = data.Buffer;
            return (Bitmap)im.ConvertFrom(imageByte);
        }

    }
}
