using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain.Events;
using ProductivityTools.GetTask3.Domain.Policy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Element :BaseEntity
    {
        public string Name { get; protected set; }
        //pw:change it to Id  
        public int ElementId { get; protected set; }
        public int? ParentId { get; protected set; }
        public ElementType Type { get; protected set; }
        public Status Status { get; protected set; }
        public DateTime Created { get; protected set; }
        //pw:change to started
        public DateTime? Initialization { get; protected set; }
        public DateTime? Started { get; protected set; }
        public DateTime? Finished { get; protected set; }
        public string Category { get; protected set; }
        public bool Cleared { get; protected set; }

        //pw: change this public
        public List<Tomato> Tomatoes { get; set; }

        public List<Element> Elements { get; protected set; }

        public Element(string name, ElementType type, int? parentId)
        {
            new OneCoreInTree().Evaluate(parentId, type);
            this.Name = name;
            this.Type = type;
            this.ParentId = parentId;
            //pw: change this dates
            Created = DateTime.Now;
            Initialization = DateTime.Now;
            Status = Status.New;

            this.Elements = new List<Element>();
            this.Tomatoes = new List<Tomato>();
        }

        public Element(string name, ElementType type, int? parentId, Status status) : this(name, type, parentId)
        {
            this.Status = status;
        }

        public Element(string name, ElementType type, int? parentId, string category) : this(name, type, parentId)
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

        //public void Update(ElementType type)
        //{
        
        //    Type = type;
        //}

        public void Finish(DateTime finishDate)
        {
            Status = Status.Finished;
            Finished = finishDate;
        }

        public void Start(DateTime startDate)
        {
            Status = Status.InProgress;
            Started = startDate;
        }

        public void Undone()
        {
            Status = Status.New;
            Finished = null;
        }

        public void Delay(DateTime initializationDate)
        {
            Initialization = initializationDate;
        }

        public void Delete()
        {
            this.Status = Status.Deleted;
        }

        public void AddToTomato(Tomato currentTomato)
        {
            this.Tomatoes.Add(currentTomato);
            base.AddNotification(new TomatoAdded());
        }

        public void ChangeParent(int parentId)
        {
            this.ParentId = parentId;
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
