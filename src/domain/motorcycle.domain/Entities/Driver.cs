using motorcycle.domain.Entities;
using motorcycle.domain.ValueObject;
using motorcycle.shared.CreationalBase;
using motorcycle.shared.domain.Entity;

namespace motorcycle.domain.entities
{
    public class Driver : EntityBase<Driver>
    {
        
        public string Name { get; set; } = "";
        public long CNPJ { get; set; }
        public DateTime BirthDay { get; set; }
        public CNH CNH { get; set; }
        public List<RentVehicle> RentVehicles { get; set; }

        public Driver ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Name is invalid for this driver"));
            Name = name;
            return this;
        }
        public Driver ChangeCNPJ(long cnpj)
        {
            if (cnpj <= 0) AddMessage(BaseError.Create(MessageType.FieldValidation, "CNPJ is invalid for this driver"));
            CNPJ = cnpj;
            return this;
        }
        public Driver ChangeBirthDay(DateTime date)
        {
            DateTime eighteenYearsAgo = DateOnly.FromDateTime(DateTime.Today).AddYears(-18).ToDateTime(TimeOnly.MinValue);

            if (date.AddYears(-18) > eighteenYearsAgo)
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Years Old is invalid for this driver"));
            if (DateTime.Now.Year - date.Year < 18 )
                AddMessage(BaseError.Create(MessageType.FieldValidation, "Birth day can't be in the futurre for this driver"));
            BirthDay = date;
            return this;
        }

        public Driver ChangeCNH(CNH cnh)
        {
            CNH = cnh;
            return this;
        }

        public static Driver Create(CNH cnh, long cnpj, DateTime birthDay, string name) =>
            new Driver()
                    .ChangeCNPJ(cnpj)
                    .ChangeName(name)
                    .ChangeBirthDay(birthDay)
                    .ChangeCNH(cnh);
    }
}
