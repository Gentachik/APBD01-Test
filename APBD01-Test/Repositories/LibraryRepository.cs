using System.Data.SqlClient;
using APBD01_Test.Models.DTOs;

namespace APBD01_Test.Repositories;

public class LibraryRepository : ILibraryRepository
{
    private readonly IConfiguration _configuration;
    public LibraryRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesPublishingHouseExist(int id)
    {
        var query = "SELECT 1 FROM [publishing_houses] WHERE PK = @ID";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();

        var book = await command.ExecuteScalarAsync();

        return book is not null;
    }

    public async Task<bool> DoesBookExist(int id)
    {
        var query = "SELECT 1 FROM [Books] WHERE PK = @ID";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();

        var book = await command.ExecuteScalarAsync();

        return book is not null;
    }

    public async Task<List<EditionDto>> GetBookEditions(int id)
    {
        var query = @"  SELECT books_editions.PK AS id, books.title AS bookTitle, 
                        books_editions.release_date AS releaseDate, books_editions.edition_title AS editionTitle, 
                        publishing_houses.name AS publishingHouseName FROM books_editions
                        JOIN publishing_houses ON books_editions.FK_publishing_house = publishing_houses.PK
                        JOIN books on books.PK = books_editions.FK_book WHERE books.PK = @ID";
        
        await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);
        
        await connection.OpenAsync();

        var reader = await command.ExecuteReaderAsync();
        List<EditionDto> editionsDto = new List<EditionDto>();
        var idOrdinal = reader.GetOrdinal("id");
        var BookTitleOrdinal = reader.GetOrdinal("bookTitle");
        var EditionTitle = reader.GetOrdinal("editionTitle");
        var PublishingHouseName = reader.GetOrdinal("publishingHouseName");
        var ReleaseDate = reader.GetOrdinal("releaseDate");
        while (await reader.ReadAsync())
        {
            editionsDto.Add(new EditionDto()
                {
                    Id = reader.GetInt32(idOrdinal),
                    BookTitle = reader.GetString(BookTitleOrdinal),
                    EditionTitle = reader.GetString(EditionTitle),
                    PublishingHouseName = reader.GetString(PublishingHouseName),
                    ReleaseDate = reader.GetDateTime(ReleaseDate)
                });
        }
        return editionsDto;
    }

    
}