using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Timers;
using DataAccess;
using Model;
using Timer = System.Timers.Timer;


namespace PpdServer;

public class Server
{
    private const int PortNum = 55555;
    private bool _done;
    private const int MinutesToWork = 2;
    private const int SecondsForVerification = 5;
    private TcpListener? _listener;
    private readonly SpectacolDbContext _context;
    public Server()
    {
        _context = new SpectacolDbContext();
        Task.Run(StopListening);
        Task.Run(VerificationThread);
        Init();
    }

    private void VerificationThread()
    {
        Console.WriteLine("verification");

        var timer = new Timer(SecondsForVerification * 1000);
        timer.Elapsed += VerifySoldSeats;
        timer.AutoReset = true;
        timer.Enabled = true;
        timer.Start();
    }

    private void VerifySoldSeats(object? source, ElapsedEventArgs e)
    {
        Console.WriteLine("starting verification");
        var isValid = true;
        using var file = new StreamWriter("validation.txt",true);
        file.WriteLine($"Validation | {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

        lock (_context)
        {
            var spectacole = _context.Spectacols.ToList();
            spectacole.ForEach(spectacol =>
            {
                //verificam daca locurile vandute pe bilete corespund cu cele din spectacol
                var vanzari = _context.Vanzari.Where(vanzare => vanzare.SpectacolId == spectacol.Id)
                    .ToList();

                var locuriVandute = vanzari.SelectMany(v => v.LocuriVandute).ToList();

                isValid = locuriVandute.All(spectacol.LocuriVandute.ToList().Contains);
            
                //verificam daca pretul din toate vanzarile corespunde cu soldul spectacolului
                var preturiBilete = vanzari.Sum(vanzare => vanzare.Suma);

                if (preturiBilete != spectacol.Sold)
                {
                    isValid = false;
                }

                var locuriVanduteString = string.Join(", ", locuriVandute);
                
                file.WriteLine($"Spectacol: {spectacol.Titlu}, | Pret Spectacol: {spectacol.Sold} " +
                               $"| Pret Bilete: {preturiBilete} | Locuri vandute: {locuriVanduteString}");
            });
            
            file.WriteLine($"Validation is {(isValid ? "Correct" : "Incorrect")}");
            file.WriteLine();
            file.WriteLine();
        }
    }

    private void StopListening()
    {
        Thread.Sleep(MinutesToWork * 60 * 1000);
        Console.WriteLine("should stop");
        _done = true;
        _listener?.Stop();
    }

    private void ProcessClient(TcpClient client)
    {
        var ns = client.GetStream();

        while (!_done)
        {
            try
            {
                var bytes = new byte[1024];
                var bytesRead = ns.Read(bytes, 0, bytes.Length);
                var json = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                var vanzare = JsonSerializer.Deserialize<Vanzare>(json);
                
                if (vanzare is not null)
                {
                    var sold = SellTicket(vanzare);

                    var bytesToSend = Encoding.ASCII.GetBytes(sold.ToString());
                    ns.Write(bytesToSend, 0, bytesToSend.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("client closed" + e.Message);
                break;
            }
        }
        ns.Close();
        client.Close();
    }

    private bool SellTicket(Vanzare vanzare)
    {
        lock (_context)
        {
            var spectacol = _context.Spectacols.First(spectacol => spectacol.Id == vanzare.SpectacolId);
            
            //verificam daca locurile de pe bilet nu sunt deja ocupate
            if (vanzare.LocuriVandute.Any(locBilet => spectacol.LocuriVandute.Contains(locBilet)))
            {
                return false;
            }
            
            //putem vinde locurile
            var newList = new List<int>(spectacol.LocuriVandute);
            newList.AddRange(vanzare.LocuriVandute);
            var x =newList.OrderBy(x => x).ToList();
            spectacol.LocuriVandute = x;
            
            //calculam pretul biletului
            var pret = vanzare.NrBileteVandute * spectacol.PretBilet;
            spectacol.Sold += pret;
            vanzare.Suma = pret;
            
            _context.Vanzari.Add(vanzare);
            _context.SaveChanges();

            return true;
        }
    }

    private void Init()
    {
        ThreadPool.SetMaxThreads(16, 16);
        _listener = new TcpListener(IPAddress.Any, PortNum);
        
        _listener.Start();

        /*_context.Spectacols.Add(new Spectacol
        {
            PretBilet = 100,
            Titlu = "S1",
            DataSpectacol = new DateTime(2021, 11, 19),
            LocuriVandute = new List<int>(),
            SalaId = 1
        });
        _context.Spectacols.Add(new Spectacol
        {
            PretBilet = 200,
            Titlu = "S2",
            DataSpectacol = new DateTime(2021, 12, 27),
            LocuriVandute = new List<int>(),
            SalaId = 1
        });
        _context.Spectacols.Add(new Spectacol
        {
            PretBilet = 150,
            Titlu = "S3",
            DataSpectacol = new DateTime(2022, 1, 3),
            LocuriVandute = new List<int>(),
            SalaId = 1
        });
        _context.SaveChanges();*/
        
        while (!_done)
        {
            try
            {
                var client = _listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(_ => ProcessClient(client));
                // Task.Run(()=> ProcessClient(client));
            }
            catch (SocketException e) when (e.SocketErrorCode == SocketError.Interrupted)
            {
                Console.WriteLine("stopping server");
                break;
            }
        }
        _listener.Stop();
    }
}

internal static class Program
{
    private static void Main(string[] args)
    {
        var server = new Server();
    }
}