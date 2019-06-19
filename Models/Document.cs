using DeviceLibrary.Models.Enums;
namespace DeviceLibrary.Models
{
    /// <summary>
    /// Represents a bill or a coin and has the following propertie.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// The value of the document.
        /// </summary>
        public decimal Value { get; private set; }
        /// <summary>
        /// Whether this is a bill or a coin.
        /// </summary>
        public DocumentType Type { get; private set; }
        /// <summary>
        ///  The number of documents of this same type and value.
        /// </summary>
        public int Count { get; private set; }

        public Document(decimal value, DocumentType type, int count)
        {
            Value = value;
            Type = type;
            Count = count;
        }

        public override string ToString()
        {
            return $"Value : [{Value}] - Type : [{Type}] - Count : [{Count}].";
        }
    }
}