using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Test002Controller;

namespace Test001Controller;
[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{
   
    private readonly IBookService _animalService;
    public BookController()
    {
        _animalService = ServiceLocator.Instance.CreateScope().ServiceProvider.GetRequiredService<IBookService>();
    }

    [HttpGet]
    public string Show()
    {
        return _animalService.GetBookName();
    }
}