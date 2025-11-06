using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class JokesController : ControllerBase
{
    private readonly IJokeService _jokeService;

    public JokesController(IJokeService jokeService)
    {
        _jokeService = jokeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetJokes()
    {
        var jokes = await _jokeService.GetJokesAsync();
        return Ok(jokes);
    }
}