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

        public Task(ISessionMetaDataProvider sessionMetaDataProvider, ITaskRepositoryCmd taskRepository, string from) : base(sessionMetaDataProvider)
        {
            this.Repository = taskRepository;
            this.From = from;
        }

        public int? SelectedNodeElementId
        {
            get
            {
                if (session.SelectedNodeElementId.HasValue)
                {
                    return session.SelectedNodeElementId.Value;
                }
                return null;
            }
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

            if (string.IsNullOrWhiteSpace(this.From))
            {
                SelectNodeByElementId(root?.ElementId);
            }

            fillOrder(ElementType.TaskBag);
            fillOrder(ElementType.Task);
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
            session.SelectedNodeElementId = value;
        }

        public void SelectNodeByOrder(int value)
        {
            session.SelectedNodeElementId = GetElementIdByOrder(value);
        }
    }
}
