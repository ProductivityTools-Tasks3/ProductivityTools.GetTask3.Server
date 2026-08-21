using System;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class SetInboxNameRequest
    {
        public int ElementId { get; set; }
        public string InboxName { get; set; }
    }
}
