using FluentAssertions;
using motorcycle.domain.Entities;
using motorcycle.domain.ValueObject;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace motorcycle.domain.unittest.ValueObjects
{
    public class TaxesUnitTest
    {
        [Fact]
        public void CalculateTaxesWithLessDays()
        {
            var date = DateTime.Now;
            var response = Taxes.CalculateFinalValue(date.AddDays(-8), date.AddDays(7), date, Plan.Create(15, 28.00m));

            response.Should().Be(341.60m);
        }

        [Fact]
        public void CalculateTaxesWithMoreDays()
        {
            var date = DateTime.Now;
            var response = Taxes.CalculateFinalValue(date.AddDays(-16), date.AddDays(-1), date, Plan.Create(15, 28.00m));

            response.Should().Be(520.00m);
        }

        [Fact]
        public void CalculateTaxesWithExactlyDays()
        {
            var date = DateTime.Now;
            var response = Taxes.CalculateFinalValue(date.AddDays(-15), date, date, Plan.Create(15, 28.00m));

            response.Should().Be(420.00m);
        }
    }
}
