using motorcycle.shared.CreationalBase;
using motorcycle.shared.domain.Entity;

namespace motorcycle.domain.Entities
{
    public class Plan : EntityBase<Plan>
    {
        public decimal Price { get; set; }
        public int DaysWithVehicle { get; set; }
        public bool IsActive { get; set; } = true;

        public Plan ChangePrice(decimal price)
        {
            if (price <= 0)
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Price is invalid on this plan"));
            Price = price;
            return this;
        }

        public Plan ChangeDaysWithVehicle(int days)
        {
            if (days <= 0) AddMessage(BaseError.Create(MessageType.FieldValidation, "Days with vehicle is invalid for this plan"));
            
            DaysWithVehicle = days; 
            return this;
        }

        public Plan ChangeActivation(bool isActive) {
            IsActive = isActive;
            return this;
        } 

        public static Plan Create(int days, decimal price) => 
            new Plan()
                    .ChangeDaysWithVehicle(days)
                    .ChangePrice(price)
                    .ChangeActivation(true);

    }
}
