namespace Alessa.Core.Entities.Results
{
    /// <summary>
    /// Provides a geleral message.
    /// </summary>
    public class GeneralMessage
    {
        /// <summary>
        /// Gets or ses the message code.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the message source. Is often used for property names.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the message type.
        /// </summary>
        public EMessageType MessageType { get; set; }
    }
}
