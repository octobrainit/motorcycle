using FluentAssertions;
using motorcycle.domain.Entities;
using motorcycle.shared.CreationalBase;

namespace motorcycle.domain.unittest.Entities
{
    public class PlanUnitTest
    {
        [Fact]
        public void Plan_Creation_ValidProperties()
        {
            int days = 30;
            decimal price = 100.50m;

            Plan plan = Plan.Create(days, price);

            plan.DaysWithVehicle.Should().Be(days);
            plan.Price.Should().Be(price);
        }

        [Fact]
        public void Plan_ChangePrice_ValidPrice()
        {
            decimal price = 150.75m;
            Plan plan = Plan.Create(20, 90.25m);

            plan.ChangePrice(price);

            plan.Price.Should().Be(price);
        }

        [Fact]
        public void Plan_ChangePrice_InvalidPrice()
        {
            decimal price = -10.00m;
            Plan plan = Plan.Create(15, 80.50m);

            Assert.Throws<BaseError>(()=> plan.ChangePrice(price));
        }

        [Fact]
        public void Plan_ChangeDaysWithVehicle_ValidDays()
        {
            int days = 60;
            Plan plan = Plan.Create(30, 100.00m);

            plan.ChangeDaysWithVehicle(days);

            plan.DaysWithVehicle.Should().Be(days);
        }

        [Fact]
        public void Plan_ChangeDaysWithVehicle_InvalidDays()
        {
            int days = 0;
            Plan plan = Plan.Create(20, 75.25m);

            Assert.Throws<BaseError>(() => plan.ChangeDaysWithVehicle(days));
        }
    }
}
