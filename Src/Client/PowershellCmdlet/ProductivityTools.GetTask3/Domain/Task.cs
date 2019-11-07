using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Repositories;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.App
{
    public class Task : DomainBase
    {
        ITaskRepositoryCmd Repository;
        string From;

        internal void GetPredefinedTask()
        {

        }

        //private Dictionary<int, SessionElementMetadata> ItemOrder
        //{
        //    get
        //    {
        //        var r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
        //        if (r == null)
        //        {
        //            cmdlet.SessionState.PSVariable.Set(_sesisonKey, new Dictionary<int, SessionElementMetadata>());
        //            r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
        //        }
        //        return (Dictionary<int, SessionElementMetadata>)r.Value;
        //    }
        //}

        public int? CurentNodeElementId
        {
            get
            {
                //if (_sessionMetadata.SelectedNodeOrder.HasValue)
                //{
                //    var orderElement = _sessionMetadata.ItemOrder.Single(x => x.Value.Order == _sessionMetadata.SelectedNodeOrder);
                //    return orderElement.Key;
                //}
                throw new Exception();
            }
        }

        private int[] GetElementsIdByOrder(int[] elementsOrderId)
        {
            int[] result = new int[elementsOrderId.Length];
            for (int i = 0; i < elementsOrderId.Length; i++)
            {
                result[i] = GetElementIdByOrder(elementsOrderId[i]);
            }
            return result;
        }

        private int GetElementIdByOrder(int? elementOrderId)
        {
            var currentElement = this.session.ElementOrder.SingleOrDefault(x => x.Value.Order == elementOrderId);
            return currentElement.Key;

        }

        private int _selectedNodeElementId { get; set; }
        public int? SelectedNodeElementId
        {
            get
            {
                if (session.SelectedNodeOrder.HasValue)
                {
                    var x = GetElementIdByOrder(session.SelectedNodeOrder);
                    return x;
                }
                if (session.SelectedNodeElementId.HasValue)
                {
                    return session.SelectedNodeElementId.Value;
                }
                return null;
            }
        }

        private ElementView structure;
        private ElementView Structure
        {
            get
            {
                if (structure == null)
                {
                    structure = Repository.GetStructure(SelectedNodeElementId, From);
                    CreateStructureMetadata(structure);
                }
                return structure;
            }
        }

        public Task(ISessionMetaDataProvider sessionMetaDataProvider, ITaskRepositoryCmd taskRepository, string from) : base(sessionMetaDataProvider)
        {
            this.Repository = taskRepository;
        }

        private void CreateStructureMetadata(Contract.ElementView root)
        {
            int taskcounter = 0;
            this.session.ElementOrder.Clear();

            Action<ElementType> fillOrder = (type) =>
            {
                if (root != null)
                {
                    foreach (var element in root.Elements.Where(x => x.Type == type))
                    {
                        this.session.ElementOrder.Add(element.ElementId, new ElementMetadata()
                        {
                            ElementId = element.ElementId,
                            Order = taskcounter,
                            ChildCount = element.ChildElementsAmount()
                        });
                        taskcounter++;
                    }
                }
            };
            SelectNodeByElementId(root?.ElementId);

            fillOrder(ElementType.TaskBag);
            fillOrder(ElementType.Task);
        }

        public Contract.ElementView CurrentElement
        {
            get
            {
                return this.Structure;
            }
        }

        public IEnumerable<PSElementView> Elements
        {
            get
            {
                var root = this.Structure;
                foreach (var element in root.Elements)
                {
                    var sessionelement = this.session.ElementOrder.First(x => x.Key == element.ElementId);
                    yield return new PSElementView() { Element = element, SessionElement = sessionelement.Value };
                }
            }
        }

        public void Add(string name, ElementType type)
        {
            this.Repository.Add(name, this.SelectedNodeElementId, type);
        }

        public void AddToTomato(int[] orderElementIds)
        {
            TomatoRepository TomatoRepository = new TomatoRepository();
            var elementIds = GetElementsIdByOrder(orderElementIds);
            TomatoRepository.AddToTomato(elementIds);
        }

        public void AddToTomato(string tomatoName)
        {
            TomatoRepository TomatoRepository = new TomatoRepository();
            TomatoRepository.AddToTomato(tomatoName, this.SelectedNodeElementId.Value);
        }

        public void FinishTomato(bool finishAlsoTasks)
        {
            TomatoRepository TomatoRepository = new TomatoRepository();
            TomatoRepository.Finish(finishAlsoTasks);
        }

        public void Finish(int orderElementId)
        {
            var elementId = GetElementIdByOrder(orderElementId);
            this.Repository.Finish(elementId);
        }

        public void Move(int[] orderElementsId, int targetId)
        {
            var elementIds = GetElementsIdByOrder(orderElementsId);
            this.Repository.Move(elementIds, targetId);
        }

        public void Delay(int orderElementId, DateTime date)
        {
            var elementId = GetElementIdByOrder(orderElementId);
            this.Repository.Delay(elementId, date);
        }

        public void Undone(int orderElementId)
        {
            var elementId = GetElementIdByOrder(orderElementId);
            this.Repository.Undone(elementId);
        }

        public void SelectNodeByElementId(int? value)
        {
            session.SelectedNodeOrder = null;
            session.SelectedNodeElementId = value;
        }

        public void SelectNodeByOrder(int value)
        {
            session.SelectedNodeOrder = value;
            session.SelectedNodeElementId = null;
        }
    }
}
