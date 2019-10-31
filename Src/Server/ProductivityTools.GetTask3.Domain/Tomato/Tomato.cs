using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Tomato : BaseEntity
    {
        public int TomatoId { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public List<Element> Elements { get; protected set; }

        public void Finish()
        {
            base.AddNotification(new TomatoFinished());
            this.Status = CoreObjects.Tomato.Status.Finished;
        }
    }
}
