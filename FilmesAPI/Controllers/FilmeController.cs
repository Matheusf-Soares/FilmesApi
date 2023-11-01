using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : Controller
{
    public static List<Filme> Filmes = new List<Filme>();
    public static int id = 0;
    [HttpPost]
    public void AdicionarFilme([FromBody] Filme filme)
    {
        Filmes.Add(filme);
        Console.WriteLine(filme.Titulo);
        Console.WriteLine(filme.Genero);
        Console.WriteLine(filme.Duracao);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes()
    {
        return Filmes;
    }
}
