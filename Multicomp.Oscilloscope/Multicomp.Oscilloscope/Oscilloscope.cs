using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multicomp.Oscilloscope
{
    public class Oscilloscope
    {
        private readonly string _ip;
        private readonly int _port;

        public Oscilloscope(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public async Task WriteRawCommand(string command)
        {
            using (var client = new TcpClient())
            {
                client.Connect(_ip, _port);
                using (var stream = client.GetStream())
                using (var writeStream = new StreamWriter(stream))
                    writeStream.Write(Encoding.ASCII.GetBytes(command));
            }
            await Task.Delay(450);
        }

        public async Task<string> GetDataRawCommand(string command)
        {
            int counter = 0;
            using (var client = new TcpClient())
            {
                client.Connect(_ip, _port);
                using (var stream = client.GetStream())
                {
                    using (var writeStream = new StreamWriter(stream))
                    {
                        using (var readStream = new StreamReader(stream))
                        {
                            writeStream.Write(Encoding.ASCII.GetBytes(command));
                        Read:
                            var data = new byte[256];
                            stream.ReadTimeout = 5;
                            int bytes;
                            if (counter >= 3) throw new Exception("Error de lectura");
                            try
                            {
                                counter++;
                                bytes = stream.Read(data, 0, data.Length);
                            }
                            catch (Exception)
                            {
                                await Task.Delay(400);
                                writeStream.Write(Encoding.ASCII.GetBytes("1"));
                                goto Read;
                            }
                            var response = Encoding.ASCII.GetString(data, 0, bytes);
                            Disconnect();
                            return response ?? "Sin respuesta";
                        }
                    }
                }
            }
        }
    }
}