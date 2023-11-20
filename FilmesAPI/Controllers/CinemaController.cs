using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class CinemaController : Controller
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id }, cinemaDto);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas()
    {
        return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
    }
    [HttpGet("{id}")]
    public IActionResult RecuperaCinemasPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema != null)
        {
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema == null)
        {
            return NotFound();
        }
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    //[HttpPatch("{id}")]
    //public IActionResult AtualizaFilmePatch(int id, JsonPatchDocument<UpdateCinemaDto> patch)
    //{
    //    var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
    //    if (cinema == null) return NotFound();

    //    var cinemaParaAtualizar = _mapper.Map<UpdateCinemaDto>(cinema);
    //    patch.ApplyTo(cinemaParaAtualizar, ModelState);

    //    if(!TryValidateModel(cinemaParaAtualizar))
    //    {
    //        return ValidationProblem(ModelState);
    //    }

    //    _mapper.Map(cinemaParaAtualizar, cinema);
    //    _context.SaveChanges();
    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    public IActionResult DeletaCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if(cinema == null)
        {
            return NotFound();
        }
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}
