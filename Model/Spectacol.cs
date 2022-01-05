namespace Model;
public class Spectacol
{
    public int Id { get; set; }
    
    public DateTime DataSpectacol { get; set; }

    public string Titlu { get; set; }
    
    public int PretBilet { get; set; }
    
    public List<int> LocuriVandute { get; set; }
    
    public int Sold { get; set; }
    
    public int SalaId { get; set; }
    
    public Sala Sala { get; set; }
}
