using FluentAssertions;
using motorcycle.domain.entities;
using motorcycle.domain.ValueObject;
using motorcycle.shared.CreationalBase;

namespace motorcycle.domain.unittest.Entities
{
    public class DriverUnitTest
    {
        [Fact]
        public void Driver_Creation_ValidProperties()
        {
            string name = "John Doe";
            long cnpj = 12345678901234;
            DateTime birthDay = new DateTime(1990, 5, 15);
            CNH cnh = new CNH();

            Driver driver = Driver.Create(cnh, cnpj, birthDay, name);

            driver.Name.Should().Be(name);
            driver.CNPJ.Should().Be(cnpj);
            driver.BirthDay.Should().Be(birthDay);
            driver.CNH.Should().Be(cnh);
        }

        [Fact]
        public void Driver_ChangeName_ValidName()
        {
            string name = "Alice Smith";
            Driver driver = Driver.Create(new CNH(), 98765432109876, new DateTime(1985, 10, 20), "Bob Johnson");

            driver.ChangeName(name);

            driver.Name.Should().Be(name);
        }

        [Fact]
        public void Driver_ChangeName_InvalidName()
        {
            string name = "";
            Driver driver = Driver.Create(new CNH(), 12345678901234, new DateTime(1990, 5, 15), "John Doe");

            Assert.Throws<BaseError>(()=> driver.ChangeName(name));

            driver.Name.Should().NotBe(name);
        }

        [Fact]
        public void Driver_ChangeCNPJ_ValidCNPJ()
        {
            long cnpj = 98765432101234;
            Driver driver = Driver.Create(new CNH(), 12345678901234, new DateTime(1990, 5, 15), "Jane Smith");

            driver.ChangeCNPJ(cnpj);

            driver.CNPJ.Should().Be(cnpj);
        }

        [Fact]
        public void Driver_ChangeCNPJ_InvalidCNPJ()
        {
            long cnpj = 0;
            Driver driver = Driver.Create(new CNH(), 12345678901234, new DateTime(1990, 5, 15), "James Bond");

            Assert.Throws<BaseError>(() => driver.ChangeCNPJ(cnpj));

            driver.CNPJ.Should().NotBe(cnpj);
        }

        [Fact]
        public void Driver_ChangeBirthDay_ValidBirthDay()
        {
            DateTime birthDay = new DateTime(1988, 3, 25);
            Driver driver = Driver.Create(new CNH(), 12345678901234, new DateTime(1990, 5, 15), "Jessica Parker");

            driver.ChangeBirthDay(birthDay);

            driver.BirthDay.Should().Be(birthDay);
        }

        [Fact]
        public void Driver_ChangeBirthDay_InvalidBirthDay()
        {
            DateTime birthDay = new DateTime(2020, 5, 15); // Future birth date
            Driver driver = Driver.Create(new CNH(), 98765432101234, new DateTime(1988, 3, 25), "Michael Johnson");

            Assert.Throws<BaseError>(() => driver.ChangeBirthDay(birthDay));
        }

        [Fact]
        public void Driver_ChangeCNH_ValidCNH()
        {
            CNH cnh = new CNH();
            Driver driver = new Driver();

            driver.ChangeCNH(cnh);

            driver.CNH.Should().Be(cnh);
        }
    }
}
