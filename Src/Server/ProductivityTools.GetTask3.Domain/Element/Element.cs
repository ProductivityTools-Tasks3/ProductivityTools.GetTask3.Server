using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain.Policy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Element
    {
        public string Name { get; protected set; }
        //pw:change it to Id  
        public int ElementId { get; protected set; }
        public int? ParentId { get; protected set; }
        public ElementType Type { get; protected set; }
        public Status Status { get; protected set; }
        public DateTime Created { get; protected set; }
        //pw:change to started
        public DateTime? Start { get; protected set; }
        public DateTime? Finished { get; protected set; }
        public string Category { get; protected set; }
        public bool Cleared { get; protected set; }

        //pw: change this public
        public List<Tomato> Tomatoes { get; set; }

        public List<Element> Elements { get; protected set; }

        public Element(string name, ElementType type)
        {
            this.Name = name;
            this.Type = type;
            this.Elements = new List<Element>();
            this.Tomatoes = new List<Tomato>();
        }

        public Element(string name, ElementType type, string category) : this(name, type)
        {
            this.Category = category;
        }

        public Element(int id, ElementType type, string name, Status status)
        {
            this.ElementId = id;
            this.Type = type;
            this.Name = name;
            this.Status = status;
        }

        public void Update(int? parentId, ElementType type)
        {
            new OneCoreInTree().Evaluate(parentId, type);
            //pw: change this dates
            Created = DateTime.Now;
            Start = DateTime.Now;
            Status = Status.New;
            Type = type;
            ParentId = parentId;
        }

        public void Finish(DateTime finishDate)
        {
            Status = Status.Finished;
            Finished = finishDate;
        }

        public void Undone()
        {
            Status = Status.New;
            Finished = null;
        }

        public void Delay(DateTime startDate)
        {
            Start = startDate;
        }

        public void AddToTomato(Tomato currentTomato)
        {
            this.Tomatoes.Add(currentTomato);
        }

        private DateTime AddDeadline(DateTime? startDate)
        {
            if (startDate.HasValue == false)
            {
                throw new Exception("If you want to set deadline you need to define start");
            }
            var r = startDate.Value.AddDays(1);
            return r;
        }
    }
}
