using FluentAssertions;
using motorcycle.domain.entities;
using motorcycle.domain.Enums;
using motorcycle.shared.CreationalBase;

namespace motorcycle.domain.unittest.Entities
{
    public class VehicleUnitTest
    {
        [Fact]
        public void Vehicle_Creation_ValidProperties()
        {
            VehicleType type = VehicleType.Motorcycle;
            string model = "Toyota Camry";
            string plate = "ABC123";

            Vehicle vehicle = Vehicle.Create(type, model, plate, 2020);

            vehicle.Type.Should().Be(type);
            vehicle.Model.Should().Be(model);
            vehicle.Plate.Should().Be(plate);
        }

        [Fact]
        public void Vehicle_ChangeModel_ValidModel()
        {
            string model = "Toyota Corolla";
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Honda", "XYZ789", 2020);

            vehicle.ChangeModel(model);

            vehicle.Model.Should().Be(model);
        }

        [Fact]
        public void Vehicle_ChangeModel_InvalidModel()
        {
            string model = "";
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Ford", "DEF456", 2020);

            Assert.Throws<BaseError>(() => vehicle.ChangeModel(model));
        }

        [Fact]
        public void Vehicle_ChangePlate_ValidPlate()
        {
            string plate = "ZYX987";
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Tesla", "EFG321", 2020);

            vehicle.ChangePlate(plate);

            vehicle.Plate.Should().Be(plate);
        }

        [Fact]
        public void Vehicle_ChangePlate_InvalidPlate()
        {
            string plate = "";
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Volvo", "GHI654", 2020);

            Assert.Throws<BaseError>(() => vehicle.ChangePlate(plate));

            vehicle.Plate.Should().NotBe(plate);
        }

        [Fact]
        public void Vehicle_ChangeType_ValidType()
        {
            VehicleType type = VehicleType.Motorcycle;
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Harley Davidson", "KLM987", 2020);

            vehicle.ChangeType(type);

            vehicle.Type.Should().Be(type);
        }

        [Fact]
        public void Vehicle_ChangeType_InvalidType()
        {
            VehicleType type = (VehicleType)99;
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "BMW", "MNO123", 2020);

            Assert.Throws<BaseError>(() => vehicle.ChangeType(type));
        }
        [Fact]
        public void Vehicle_ChangeType_InvalidYear()
        {
            VehicleType type = (VehicleType)99;
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "BMW", "MNO123", DateTime.Today.Year);

            Assert.Throws<BaseError>(() => vehicle.ChangeYear(DateTime.Today.Year + 2));
        }
    }
}
