using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain.Events
{
    public class TomatoFinished : INotification
    {
        public Tomato Tomato { get; private set; }

        public TomatoFinished(Tomato tomato)
        {
            this.Tomato = tomato;
        }
    }
}
