﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Item : Component
    {
        public int TaskOrderId { get; set; }
        public string Name { get; set; }
    }
}
