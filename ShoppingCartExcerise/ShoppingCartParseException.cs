using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ShoppingCartExcerise
{
    [Serializable]
    public class ShoppingCartParseException : Exception
    {
        private readonly char? sku;

        public ShoppingCartParseException()
        {

        }

        public ShoppingCartParseException(string message) : base(message)
        {

        }

        public ShoppingCartParseException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ShoppingCartParseException(char SKU, string message) : base(message)
        {
            sku = SKU;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ShoppingCartParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            sku = info.GetChar("SKU");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SKU", SKU);
            base.GetObjectData(info, context);
        }

        public char? SKU => sku;

        public override string ToString()
        {
            var message = $"Message: {Message}";

            if (SKU != null)
            {
                message += $", SKU: {SKU}";
            }

            return message;
        }
    }
}
