﻿using System.Runtime.Serialization;

namespace BankOfSuccess.BuisnessLogicLayer
{
    [Serializable]
    internal class TransferLimitExceededException : Exception
    {
        public TransferLimitExceededException()
        {
        }

        public TransferLimitExceededException(string? message) : base(message)
        {
        }

        public TransferLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TransferLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}