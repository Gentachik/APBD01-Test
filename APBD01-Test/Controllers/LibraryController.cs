using APBD01_Test.Models.DTOs;
using APBD01_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APBD01_Test.Controllers;

[Route("api/books")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly ILibraryRepository _libraryRepository;
    public LibraryController(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }
    
    [HttpGet("{id:int}/editions")]
    public async Task<IActionResult> GetBookEditions(int id)
    {
        if (!await _libraryRepository.DoesBookExist(id))
            return NotFound($"Book with given ID - {id} doesn't exist");

        var editions = await _libraryRepository.GetBookEditions(id);
            
        return Ok(editions);
    }

    [HttpPost]
    public async Task<IActionResult> GetBookEdition(BookDto bookDto)
    {
        if (!await _libraryRepository.DoesPublishingHouseExist(bookDto.publishingHouseId))
            return NotFound($"Publishing house with given ID - {bookDto.publishingHouseId} doesn't exist");
        _libraryRepository.AddBook(bookDto);
        
        return Created();
    }
}