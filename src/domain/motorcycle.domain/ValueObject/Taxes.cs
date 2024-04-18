using motorcycle.domain.Entities;

namespace motorcycle.domain.ValueObject
{
    public class Taxes
    {
        public static decimal CalculateFinalValue(DateTime startDate, DateTime dateForecast, DateTime dateEnd, Plan plan)
        {
            var tax = 0.2;

            if (plan.DaysWithVehicle > 7)
                tax = 0.4;

            int remainingDays = (dateForecast.Date - dateEnd.Date).Days + 1;
            var daysRented = (dateEnd - startDate).Days + 1;

            return DateTime.Compare(dateEnd, dateForecast) switch
            {
                -1 => DaysRentedPrice(plan.Price, daysRented) + TaxesCalculated(tax, plan.Price, remainingDays),
                1 => DaysRentedPrice(plan.Price, plan.DaysWithVehicle) + ((daysRented - plan.DaysWithVehicle) * 50),
                _ => DaysRentedPrice(plan.Price, plan.DaysWithVehicle),
            };
        }

        public static decimal TaxesCalculated(double tax, decimal price, int days) =>
             Convert.ToDecimal(tax  * Convert.ToDouble(price) * days);
        public static decimal DaysRentedPrice(decimal price, int days) => 
            price * days;
    } 
}
