using Cars.ApplicationServices;
using Cars.Core.Domain;
using Cars.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cars.CarTest
{
    public class CarServicesTests
    {
        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        /// <summary>
        /// Tests that CreateCarAsync saves a new car to the database, assigns an Id, and sets CreatedAt/ModifiedAt timestamps.
        /// </summary>
        [Fact]
        public async Task CreateCarAsync_SavesCarAndReturnsWithId()
        {
            await using var context = CreateInMemoryContext();
            var service = new CarServices(context);

            var car = new Car
            {
                Brand = "BMW",
                Model = "X5",
                Year = 2024,
                Price = 65000,
                Color = "Black",
                Notes = "Test car"
            };

            var result = await service.CreateCarAsync(car);

            Assert.True(result.Id > 0);
            Assert.Equal("BMW", result.Brand);
            Assert.NotEqual(default(DateTime), result.CreatedAt);
            Assert.NotEqual(default(DateTime), result.ModifiedAt);
        }

        /// <summary>
        /// Tests that GetAllCarsAsync returns all cars stored in the database.
        /// </summary>
        [Fact]
        public async Task GetAllCarsAsync_ReturnsAllCars()
        {
            await using var context = CreateInMemoryContext();
            context.Cars.AddRange(
                new Car { Brand = "Audi", Model = "A4", Year = 2023, Price = 45000, Color = "White", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow },
                new Car { Brand = "BMW", Model = "X5", Year = 2024, Price = 65000, Color = "Black", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow }
            );
            await context.SaveChangesAsync();

            var service = new CarServices(context);
            var cars = await service.GetAllCarsAsync();

            Assert.Equal(2, cars.Count());
        }

        /// <summary>
        /// Tests that GetCarByIdAsync returns the correct car when it exists in the database.
        /// </summary>
        [Fact]
        public async Task GetCarByIdAsync_ReturnsCar_WhenExists()
        {
            await using var context = CreateInMemoryContext();
            var car = new Car { Brand = "Mercedes", Model = "E-Class", Year = 2023, Price = 55000, Color = "Silver", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow };
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var service = new CarServices(context);
            var result = await service.GetCarByIdAsync(car.Id);

            Assert.NotNull(result);
            Assert.Equal("Mercedes", result.Brand);
            Assert.Equal("E-Class", result.Model);
        }

        /// <summary>
        /// Tests that DeleteCarAsync removes the car from the database.
        /// </summary>
        [Fact]
        public async Task DeleteCarAsync_RemovesCarFromDatabase()
        {
            await using var context = CreateInMemoryContext();
            var car = new Car { Brand = "Volvo", Model = "XC90", Year = 2023, Price = 70000, Color = "Blue", CreatedAt = DateTime.UtcNow, ModifiedAt = DateTime.UtcNow };
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            var service = new CarServices(context);
            await service.DeleteCarAsync(car.Id);

            var deleted = await context.Cars.FindAsync(car.Id);
            Assert.Null(deleted);
        }
    }
}
