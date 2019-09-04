﻿using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{
    public class Order
    {
        internal void Format(ColorString input, PSElementView element)
        {
            var part = new ColorStringItem();
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    part.Value = $"T{GetOrder(viewMetadata)}. ";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    part.Value = $"B{GetOrder(viewMetadata)}. ";
                    break;
            }
            part.Color = 190;
            input.Add(part);
        }

        private string GetOrder(SessionElementMetadata metadata)
        {
            return metadata.Order.ToString().PadLeft(3, '0');
        }
    }
}