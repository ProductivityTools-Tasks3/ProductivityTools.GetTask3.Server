using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Task : Component
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Finished { get; set; }


        public Task(int id, string name, int orderId, Status status) : base(name)
        {
            this.Id = id;
            this.OrderId = orderId;
            this.Status = status;
        }
    }
}
