using motorcycle.domain.Entities;
using motorcycle.domain.Enums;
using motorcycle.shared.CreationalBase;
using motorcycle.shared.domain.Entity;

namespace motorcycle.domain.entities
{
    public class Vehicle : EntityBase<Vehicle>
    {
        public VehicleType Type { get; set; }
        public string TypeDescription { get { return Type.ToString(); } }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public List<RentVehicle> RentVehicles { get; set; }

        public Vehicle ChangeModel(string model)
        {
            if (string.IsNullOrEmpty(model)) 
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Model is invalid on this vehicle"));
            Model = model;  
            return this;
        }

        public Vehicle ChangePlate(string plate)
        {
            if (string.IsNullOrEmpty(plate))
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Plate is invalid on this vechicle"));
            Plate = plate;
            return this;
        }

        public Vehicle ChangeType(VehicleType type)
        {
            if (!Enum.IsDefined(typeof(VehicleType), type)) 
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Vehicle type is invalid on this vehicle"));
            Type = type;
            return this;
        }

        public Vehicle ChangeYear(int year)
        {
            if (year > DateTime.Today.Year + 1) AddMessage(BaseError.Create(MessageType.FieldValidation, "Vehicle year is invalid on this vehicle"));
            Year = year;
            return this;
        }

        public static Vehicle Create(VehicleType type, string model, string plate, int year) =>
           new Vehicle()
                .ChangeModel(model)
                .ChangePlate(plate)
                .ChangeType(type)
                .ChangeYear(year);
    }
}
