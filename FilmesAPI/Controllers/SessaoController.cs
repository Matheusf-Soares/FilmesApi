using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : Controller
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao(CreateSessaoDto sessaoDto)
    {
        Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = sessao.Id }, sessao);
    }

    [HttpGet]
    public IEnumerable<ReadSessaoDto> RecuperaSessoes()
    {
        return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaSessoesPorId(int id)
    {
        Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
        if(sessao == null)
        {
            return NotFound();
        }
        ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
        return Ok(sessaoDto);
    }

    //[HttpPut("{id}")]
    //public IActionResult AtualizaSessao(int id, )

    //[HttpDelete("{id}")]
    //public IActionResult DeleteSessao(int id)
    //{
    //    Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
    //    if(sessao == null)
    //    {
    //        return NotFound();
    //    }
    //    _context.Sessoes.Remove(sessao);
    //    _context.SaveChanges();
    //    return NoContent();
    //}

}
