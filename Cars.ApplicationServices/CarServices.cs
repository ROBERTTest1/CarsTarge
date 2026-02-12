using Cars.Core.Domain;
using Cars.Core.ServiceInterfaces;
using Cars.Data;
using Microsoft.EntityFrameworkCore;

namespace Cars.ApplicationServices
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationDbContext _context;

        public CarServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            car.CreatedAt = DateTime.UtcNow;
            car.ModifiedAt = DateTime.UtcNow;

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            car.ModifiedAt = DateTime.UtcNow;

            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            return await _context.Cars.AnyAsync(c => c.Id == id);
        }
    }
}
