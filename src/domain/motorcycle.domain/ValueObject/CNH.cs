using motorcycle.domain.Enums;
using motorcycle.shared.CreationalBase;
using motorcycle.shared.domain.Entity;

namespace motorcycle.domain.ValueObject
{
    public class CNH : EntityBase<CNH>
    {
        public long DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public string ImagePath { get; set; } = "";
        public bool CNHisValid
        {
            get
            {
                return ExpirationDate.HasValue ?
                                ExpirationDate.Value >= DateOnly.FromDateTime(DateTime.Today) && DocumentType.ToString().Contains("A") :
                                DocumentType.ToString().Contains("A");
            }
        }

        public CNH ChangerDocumentNumber(long documentNumber)
        {
            if (documentNumber <= 0)
                AddMessage(BaseError.Create(MessageType.DomainValidation, "Document Number invalid"));
            DocumentNumber = documentNumber;
            return this;
        }

        public CNH ChangerDocumentType(DocumentType type)
        {
            if (!type.ToString().Contains("A"))
                AddMessage(BaseError.Create(MessageType.DomainValidation, "Document Type invalid, you must have A"));
            DocumentType = type;
            return this;
        }

        public CNH ChangeExpiration(DateOnly? dateOnly)
        {
            if (dateOnly is not null)
            {
                ExpirationDate = dateOnly;
            }
            return this;
        }

        public CNH ChangeImagePath(string url) {
            if (string.IsNullOrWhiteSpace(url))
                AddMessage(BaseError.Create(MessageType.FieldValidation, "File path is invalid for this CNH"));
            
            ImagePath = url;
            
            return this;
        } 


        public static CNH Create(long documentNumber, DocumentType type) =>
            new CNH()
                    .ChangerDocumentNumber(documentNumber)
                    .ChangerDocumentType(type);
    }
}
