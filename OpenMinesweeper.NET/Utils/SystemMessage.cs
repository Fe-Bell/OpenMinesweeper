using System;

namespace OpenMinesweeper.NET.Utils
{
    /// <summary>
    /// A basic class to represent system wide messages.
    /// </summary>
    public class SystemMessage
    {
        /// <summary>
        /// The message sender.
        /// </summary>
        public object Sender { get; private set; }
        /// <summary>
        /// The target type of the message.
        /// </summary>
        public Type Target { get; private set; }
        /// <summary>
        /// The message header.
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Extra data sent over.
        /// </summary>
        public object ExtendedData { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <param name="extendedData"></param>
        public SystemMessage(object sender, Type target, string message, object extendedData = null)
        {
            Sender = sender;
            Target = target;
            Message = message;
            ExtendedData = extendedData;
        }
    }
}
