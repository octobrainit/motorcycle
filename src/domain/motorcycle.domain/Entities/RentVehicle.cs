using motorcycle.domain.entities;
using motorcycle.domain.ValueObject;
using motorcycle.shared.CreationalBase;
using motorcycle.shared.domain.Entity;

namespace motorcycle.domain.Entities
{
    public class RentVehicle : EntityBase<RentVehicle>
    {
        public DateTime DateToRent { get; set; } = new();
        public DateTime? EndDate { get; set; }
        public DateTime EndForecast { get { return DateToRent.AddDays(Plan.DaysWithVehicle -1); } }
        public decimal TaxApplied { get; set; } = 0;
        public string Cause { get; set; } = "";
        public decimal ForecastValue { get { return Plan.Price * Plan.DaysWithVehicle; } }
        public decimal FinalValue { get; set; }

        public Plan Plan { get; set; } = new();
        public Driver Driver { get; set; } = new();
        public Vehicle Vehicle { get; set; } = new();


        public RentVehicle ChangePlan(Plan plan)
        {
            Plan = plan;
            return this;
        }

        public RentVehicle ChangeDriver(Driver driver)
        {
            if (driver.CNH is null) AddMessage(BaseError.Create(MessageType.FieldValidation, "CNH must be informed, this is invalid for this driver to rent"));
            if (!driver.CNH.CNHisValid) AddMessage(BaseError.Create(MessageType.FieldValidation, "Cnh is not valid for this driver to rent"));

            Driver = driver;

            return this;
        }

        public RentVehicle ChangeVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            return this;
        }

        public RentVehicle ChangeDateToRent(DateTime date)
        {
            if (date < DateTime.Today)
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Date to rent is invalid for this rent"));
            DateToRent = date.AddDays(1);
            return this;
        }

        public RentVehicle ChangeEndDate(DateTime date)
        {
            if (date < DateTime.Today)
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Date to end is invalid for this rent"));
            EndDate = date;
            return this;
        }

        public RentVehicle ChangeCause(string cause)
        {
            if (string.IsNullOrWhiteSpace(cause)) AddMessage(BaseError.Create(MessageType.FieldValidation, "Cause is invalid for cancel this rent"));
            Cause = cause;
            return this;
        }

        public RentVehicle CancelRent(DateTime date, string cause)
        {
            FinalValue = Taxes.CalculateFinalValue(DateToRent, EndForecast, date, Plan);
            ChangeEndDate(date);
            ChangeCause(cause);

            return this;
        }

        public static RentVehicle Create(Plan plan, Driver driver, Vehicle vehicle, DateTime dateToRent) =>
            new RentVehicle()
                    .ChangeDriver(driver)
                    .ChangePlan(plan)
                    .ChangeVehicle(vehicle)
                    .ChangeDateToRent(dateToRent);
    }
}
