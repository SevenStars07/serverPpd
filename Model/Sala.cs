namespace Model;

public class Sala
{
    public int Id { get; set; }
    
    public int NrLocuri { get; set; }

    public List<Spectacol> Spectacole { get; set; }
    
    public List<Vanzare> Vanzari { get; set; }
}