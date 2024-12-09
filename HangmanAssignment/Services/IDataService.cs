

using HangmanAssignment.Models;

namespace HangmanAssignment.Services
{
    public interface IDataService
    {
        Task<Question> LoadDataAsync();
    }
}
