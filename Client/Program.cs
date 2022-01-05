using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Model;

var rnd = new Random();
const int noClients = 100;
var threads = new Thread[noClients];

for (var i = 0; i < noClients; i++)
{
    try
    {
        var client = new TcpClient("127.0.0.1", 55555);
        threads[i] = new Thread(() => ThreadWork(client, i));
        threads[i].Start();
    }
    catch (Exception e)
    {
        Console.WriteLine("server stopped" + e.Message);
        break;
    }
    
    Thread.Sleep(200);
}

void ThreadWork(TcpClient client, int clientId)
{
    var ns = client.GetStream();
    while (true)
    {
        try
        {

            var nrLocuriVandute = rnd.Next(1,6);

            var locuriVandute = new List<int>();
            for (int i = 0; i < nrLocuriVandute; i++)
            {
                var nrLoc = rnd.Next(100);
                while (locuriVandute.Any(x => x == nrLoc))
                {
                    nrLoc = rnd.Next(100);
                }
                locuriVandute.Add(nrLoc);
            }
            
            var vanzare = new Vanzare
            {
                NrBileteVandute = nrLocuriVandute,
                DataVanzare = DateTime.Now,
                LocuriVandute = locuriVandute,
                SpectacolId = rnd.Next(4)
            };
            
            var json = JsonSerializer.Serialize(vanzare);
            
            var bytesToSend = Encoding.ASCII.GetBytes(json);
            // Console.WriteLine($"sending {json}");
            ns.Write(bytesToSend, 0, bytesToSend.Length);
            
            var bytes = new byte[1024];
            var bytesRead = ns.Read(bytes, 0, bytes.Length);
            var response = Encoding.ASCII.GetString(bytes, 0, bytesRead);
            
            Console.WriteLine($"sent {json} and got resposnse {response}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{clientId} is stopping {e.Message}");
            break;
        }
        Thread.Sleep(2000);
    }
}