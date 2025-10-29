using Application.Models.ExternalDTO;

public interface IJokeService
{
    Task<List<JokeDTO>> GetJokesAsync();
}