using APBD01_Test.Models.DTOs;

namespace APBD01_Test.Repositories;

public interface ILibraryRepository
{
    Task<bool> DoesPublishingHouseExist(int id);
    Task<bool> DoesBookExist(int id);
    Task<List<EditionDto>> GetBookEditions(int id);
}