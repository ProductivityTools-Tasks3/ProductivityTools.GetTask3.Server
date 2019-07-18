using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.App
{
    class TaskStructure
    {
        string _sesisonKey = "ViewMetadata";

        private SessionMetadata _sessionMetadata
        {
            get
            {
                var r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                if (r == null)
                {
                    cmdlet.SessionState.PSVariable.Set(_sesisonKey, new SessionMetadata());
                    r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                }
                return (SessionMetadata)r.Value;
            }
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

        private int GetElementIdByOrder(int? elementOrderId)
        {
            var currentElement = this._sessionMetadata.ItemOrder.SingleOrDefault(x => x.Value.Order == elementOrderId);
            return currentElement.Key;

        }

        private int _selectedNodeElementId { get; set; }
        public int? SelectedNodeElementId
        {
            get
            {
                if (_sessionMetadata.SelectedNodeOrder.HasValue)
                {
                    var x = GetElementIdByOrder(_sessionMetadata.SelectedNodeOrder);
                    return x;
                }
                if (_sessionMetadata.SelectedNodeElementId.HasValue)
                {
                    return _sessionMetadata.SelectedNodeElementId.Value;
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
                    structure = repository.GetStructure(SelectedNodeElementId);
                    CreateViewMetadata(structure);
                }
                return structure;
            }
        }


        System.Management.Automation.PSCmdlet cmdlet;
        TaskStructureRepository repository;

        internal TaskStructure(System.Management.Automation.PSCmdlet pSVariableIntrinsics)
        {
            this.cmdlet = pSVariableIntrinsics;
            this.repository = new TaskStructureRepository();
        }

        private void CreateViewMetadata(Contract.ElementView root)
        {
            SessionMetadata View = new SessionMetadata();

            int taskcounter = 0;
            //int bagcounter = 0;
            this._sessionMetadata.ItemOrder.Clear();

            Action<ElementType> fillOrder = (type) =>
            {
                if (root != null)
                {
                    foreach (var element in root.Elements.Where(x => x.Type == type))
                    {

                        this._sessionMetadata.ItemOrder.Add(element.ElementId, new SessionElementMetadata()
                        {
                            ElementId = element.ElementId,
                            Order = taskcounter,
                            ChildCount = element.ChildElementsAmount()
                        });
                        taskcounter++;
                    }
                }
            };
            SelectNodeByElementId(root.ElementId);

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
                    var sessionelement = this._sessionMetadata.ItemOrder.First(x => x.Key == element.ElementId);
                    yield return new PSElementView() { Element = element, SessionElement = sessionelement.Value };
                }
            }
        }

        public void Add(string name, ElementType type)
        {
            this.repository.Add(name, this.SelectedNodeElementId, type);
        }

        public void Finish(int orderElementId)
        {
            var elementId = GetElementIdByOrder(orderElementId);
            this.repository.Finish(elementId);
        }

        public void SelectNodeByElementId(int value)
        {
            _sessionMetadata.SelectedNodeOrder = null;
            _sessionMetadata.SelectedNodeElementId = value;
        }

        public void SelectNodeByOrder(int value)
        {
            _sessionMetadata.SelectedNodeOrder = value;
            _sessionMetadata.SelectedNodeElementId = null;
        }
    }
}
