namespace Model;

public class Vanzare
{
    public int Id { get; set; }
    
    public int SpectacolId { get; set; }
    
    public Spectacol Spectacol { get; set; }
    
    public DateTime DataVanzare { get; set; }
    
    public int NrBileteVandute { get; set; }
    
    public List<int> LocuriVandute { get; set; }
    
    public int Suma { get; set; }

    public override string ToString()
    {
        return
            $"Id:{Id},SpectacolId:{SpectacolId},DataVanzare:{DataVanzare}," +
            $"NrBileteVandute{NrBileteVandute},LocuriVandute:{LocuriVandute}, Suma:{Suma}";
    }
}