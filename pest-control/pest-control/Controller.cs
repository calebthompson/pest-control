﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pest_control
{
    abstract class Controller
    {
        protected EventQueue eventQueue;

        public Controller(EventQueue eventQueue) { this.eventQueue = eventQueue; }

        public abstract void Update();

    }
}
