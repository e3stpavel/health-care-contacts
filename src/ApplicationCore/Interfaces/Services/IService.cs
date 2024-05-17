using ErrorOr;

namespace UtterlyComplete.ApplicationCore.Interfaces.Services
{
    /// <typeparam name="T">must be DTO</typeparam>
    public interface IService<T>
    {
        Task<ErrorOr<T>> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> CreateAsync(T dto);

        Task<ErrorOr<T>> UpdateAsync(int id, T dto);

        Task<ErrorOr<Deleted>> DeleteAsync(int id);
    }
}
