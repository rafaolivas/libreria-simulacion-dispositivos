using DeviceLibrary.Models.Enums;
namespace DeviceLibrary.Models
{
    public class Document
    {
        public decimal Value { get; private set; }
        public DocumentType Type { get; private set; }
        public int Count { get; private set; }
    }
}