﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            this.Value = value;
        }

        public T Value 
        {
            get; 
            private set;
        }

    }
}
