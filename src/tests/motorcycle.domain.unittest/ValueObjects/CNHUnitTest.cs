using FluentAssertions;
using motorcycle.domain.Enums;
using motorcycle.domain.ValueObject;
using motorcycle.shared.CreationalBase;

namespace motorcycle.domain.unittest.ValueObjects
{
    public class CNHUnitTest
    {
        [Fact]
        public void CNH_Creation_ValidProperties()
        {
            long documentNumber = 123456789;
            DocumentType type = DocumentType.A;

            CNH cnh = CNH.Create(documentNumber, type);

            cnh.DocumentNumber.Should().Be(documentNumber);
            cnh.DocumentType.Should().Be(type);
            cnh.ExpirationDate.Should().BeNull();
            cnh.CNHisValid.Should().BeTrue();
        }

        [Fact]
        public void CNH_ChangeDocumentNumber_ValidDocumentNumber()
        {
            long documentNumber = 987654321;
            CNH cnh = new CNH();

            cnh.ChangerDocumentNumber(documentNumber);

            cnh.DocumentNumber.Should().Be(documentNumber);
        }

        [Fact]
        public void CNH_ChangeDocumentNumber_InvalidDocumentNumber()
        {
            long documentNumber = 0;
            CNH cnh = new CNH();

            Assert.Throws<BaseError>(() => cnh.ChangerDocumentNumber(documentNumber ));
        }

        [Fact]
        public void CNH_ChangeDocumentType_ValidDocumentType()
        {
            DocumentType type = DocumentType.A;
            CNH cnh = new CNH();

            cnh.ChangerDocumentType(type);

            cnh.DocumentType.Should().Be(type);
        }

        [Fact]
        public void CNH_ChangeDocumentType_InvalidDocumentType()
        {
            DocumentType type = DocumentType.B;
            CNH cnh = new CNH();

            Assert.Throws<BaseError>(() => cnh.ChangerDocumentType(type));

        }

        [Fact]
        public void CNH_ChangeExpiration_ValidExpirationDate()
        {
            DateOnly expirationDate = DateOnly.FromDateTime(DateTime.Today.AddDays(10));
            CNH cnh = new CNH();

            cnh.ChangeExpiration(expirationDate);

            cnh.ExpirationDate.Should().Be(expirationDate);
        }

        [Fact]
        public void CNH_ChangeExpiration_NullExpirationDate()
        {
            CNH cnh = new CNH();

            cnh.ChangeExpiration(null);

            cnh.ExpirationDate.Should().BeNull();
        }
    }
}
