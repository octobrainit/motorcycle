using FluentAssertions;
using motorcycle.domain.entities;
using motorcycle.domain.Entities;
using motorcycle.domain.Enums;
using motorcycle.domain.ValueObject;
using motorcycle.shared.CreationalBase;

namespace motorcycle.domain.unittest.Entities
{
    public class RentVehicleUnitTest
    {
        [Fact]
        public void RentVehicle_Creation_ValidProperties()
        {
            DateTime dateToRent = DateTime.Today;
            Plan plan = Plan.Create(7, 200);
            CNH cnh = CNH.Create(19000, DocumentType.A_B);
            Driver driver = Driver.Create(cnh, 12345678901234, new DateTime(1990, 5, 15), "John Doe");
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Toyota Camry", "ABC123", 2020);

            RentVehicle rentVehicle = RentVehicle.Create(plan, driver, vehicle, dateToRent);

            rentVehicle.DateToRent.Should().Be(dateToRent.AddDays(1));
            rentVehicle.EndDate.Should().BeNull();
            rentVehicle.TaxApplied.Should().Be(0);
            rentVehicle.Cause.Should().Be("");
            rentVehicle.ForecastValue.Should().Be(plan.Price * plan.DaysWithVehicle);
            rentVehicle.FinalValue.Should().Be(0);
            rentVehicle.Plan.Should().Be(plan);
            rentVehicle.Driver.Should().Be(driver);
            rentVehicle.Vehicle.Should().Be(vehicle);
        }

        [Fact]
        public void RentVehicle_ChangePlan_ValidPlan()
        {
            Plan plan = Plan.Create(5, 150);
            RentVehicle rentVehicle = new RentVehicle();

            rentVehicle.ChangePlan(plan);

            rentVehicle.Plan.Should().Be(plan);
        }

        [Fact]
        public void RentVehicle_ChangeDriver_ValidDriver()
        {
            Driver driver = Driver.Create(new CNH(), 98765432101234, new DateTime(1985, 10, 20), "Alice Smith");
            RentVehicle rentVehicle = new RentVehicle();

            rentVehicle.ChangeDriver(driver);

            rentVehicle.Driver.Should().Be(driver);
        }

        [Fact]
        public void RentVehicle_ChangeDriver_InvalidDriver()
        {
            Driver driver = new Driver(); // No CNH
            RentVehicle rentVehicle = new RentVehicle();

            Assert.Throws<BaseError>(()=> rentVehicle.ChangeDriver(driver));

        }

        [Fact]
        public void RentVehicle_ChangeVehicle_ValidVehicle()
        {
            // Arrange
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Ford F-150", "XYZ789", 2020);
            RentVehicle rentVehicle = new RentVehicle();

            // Act
            rentVehicle.ChangeVehicle(vehicle);

            // Assert
            rentVehicle.Vehicle.Should().Be(vehicle);
        }

        [Fact]
        public void RentVehicle_ChangeDateToRent_ValidDateToRent()
        {
            DateTime dateToRent = DateTime.Today;
            RentVehicle rentVehicle = new RentVehicle();

            rentVehicle.ChangeDateToRent(dateToRent);

            rentVehicle.DateToRent.Should().Be(dateToRent.AddDays(1));
        }

        [Fact]
        public void RentVehicle_ChangeDateToRent_InvalidDateToRent()
        {
            DateTime dateToRent = DateTime.Today.AddDays(-1); // Past date
            RentVehicle rentVehicle = new RentVehicle();

            Assert.Throws<BaseError>(() => rentVehicle.ChangeDateToRent(dateToRent));
        }

        [Fact]
        public void RentVehicle_ChangeEndDate_ValidEndDate()
        {
            DateTime endDate = DateTime.Today.AddDays(7);
            RentVehicle rentVehicle = new RentVehicle();

            rentVehicle.ChangeEndDate(endDate);

            rentVehicle.EndDate.Should().Be(endDate);
        }

        [Fact]
        public void RentVehicle_ChangeEndDate_InvalidEndDate()
        {
            DateTime endDate = DateTime.Today.AddDays(-1); // Past date
            RentVehicle rentVehicle = new RentVehicle();

            Assert.Throws<BaseError>(()=> rentVehicle.ChangeEndDate(endDate));

        }

        [Fact]
        public void RentVehicle_ChangeCause_ValidCause()
        {
            string cause = "Accident";
            RentVehicle rentVehicle = new RentVehicle();

            rentVehicle.ChangeCause(cause);

            rentVehicle.Cause.Should().Be(cause);
        }

        [Fact]
        public void RentVehicle_CancelRent_ValidCancellation()
        {
            DateTime dateToRent = DateTime.Today.AddDays(1);
            Plan plan = Plan.Create(7, 200);
            Driver driver = Driver.Create(new CNH(), 12345678901234, new DateTime(1990, 5, 15), "John Doe");
            Vehicle vehicle = Vehicle.Create(VehicleType.Motorcycle, "Toyota Camry", "ABC123", 2020);
            RentVehicle rentVehicle = RentVehicle.Create(plan, driver, vehicle, dateToRent);
            DateTime endDate = dateToRent.AddDays(plan.DaysWithVehicle);
            string cause = "Accident";

            rentVehicle.CancelRent(endDate, cause);

            rentVehicle.EndDate.Should().Be(endDate);
            rentVehicle.Cause.Should().Be(cause);
        }
    }
}
