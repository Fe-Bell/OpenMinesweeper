﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenMinesweeper.NET.Utils
{
    public class SystemMessage
    {
        public object Sender { get; private set; }
        public Type Target { get; private set; }
        public string Message { get; private set; }
        public object ExtendedData { get; private set; }

        public SystemMessage(object sender, Type target, string message, object extendedData = null)
        {
            Sender = sender;
            Target = target;
            Message = message;
            ExtendedData = extendedData;
        }
    }
}
