namespace biblioteca.Models;

public class Musica {
    
    private int id;
    private string? titulo;
    private string? compositor;
    private string? genero;
    private int ano;
    private string? artista;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string? Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }
    public string Compositor
    {
        get { return compositor; }
        set { compositor = value; }
    }
    public string? Genero
    {
        get { return genero; }
        set { genero = value; }
    }
    public int Ano
    {
        get { return ano; }
        set { ano = value; }
    }
    public string? Artista
    {
        get { return artista; }
        set { artista = value; }
    }
}
