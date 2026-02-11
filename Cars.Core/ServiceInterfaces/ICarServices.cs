using Cars.Core.Domain;

namespace Cars.Core.ServiceInterfaces
{
    public interface ICarServices
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        
        Task<Car?> GetCarByIdAsync(int id);
        
        Task<Car> CreateCarAsync(Car car);
        
        Task<Car> UpdateCarAsync(Car car);
        
        Task DeleteCarAsync(int id);
        Task<bool> CarExistsAsync(int id);
    }
}
