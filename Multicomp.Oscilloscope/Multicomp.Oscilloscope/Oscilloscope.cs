using System.Net.Sockets;
using System.Text;

namespace Sciodesk.Zamtest.Multicomp;

public class Oscilloscope : IDisposable
{
    private TcpClient? _tcpClient;
    private readonly string _ip;
    private readonly int _port;

    public Oscilloscope(string ip, int port)
    {
        _ip = ip;
        _port = port;
        _tcpClient = new TcpClient();
    }

    public void Dispose()
    {
        _tcpClient?.Close();
        _tcpClient?.Dispose();
    }

    private NetworkStream Connect()
    {
        try
        {
            _tcpClient = new TcpClient();
            _tcpClient?.Connect(_ip, _port);
            return _tcpClient?.GetStream() ?? throw new Exception("No se conectó el osciloscopio");
        }
        catch (Exception)
        {
            Dispose();
            throw;
        }
    }

    private void Disconnect()
    {
        if (_tcpClient != null)
        {
            _tcpClient.Close();
            _tcpClient.Dispose();
        }
    }

    public async Task WriteRawCommand(string command)
    {
        var stream = Connect();
        stream.Write(Encoding.ASCII.GetBytes(command));
        Disconnect();
        await Task.Delay(450);
    }

    public async Task<string> GetDataRawCommand(string command)
    {
        int counter = 0;                                                        
        var stream = Connect();
        stream.Write(Encoding.ASCII.GetBytes(command));
        Read:
        var data = new Byte[256];
        stream.ReadTimeout = 5;
        Int32 bytes;
        if (counter >= 3) throw new Exception("Error de lectura");
        try
        {
            counter++;
            bytes = stream.Read(data, 0, data.Length);
        }
        catch (Exception)
        {
            await Task.Delay(400);
            stream.Write(Encoding.ASCII.GetBytes("1"));
            goto Read;
        }
        var response = Encoding.ASCII.GetString(data, 0, bytes);
        Disconnect();
        return response ?? "Sin respuesta";
    }
}