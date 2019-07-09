using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
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

        private int _selectedNodeElementId { get; set; }
        public int? SelectedNodeElementId
        {
            get
            {
                if (_sessionMetadata.SelectedNodeOrder.HasValue)
                {
                    var currentElement = this._sessionMetadata.ItemOrder.SingleOrDefault(x => x.Value.Order == _sessionMetadata.SelectedNodeOrder);
                    {
                        return currentElement.Key;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private ElementView structure;
        private ElementView Structure
        {
            get
            {
                if (structure == null)
                {
                    structure = GetStructure(SelectedNodeElementId);
                }
                return structure;
            }
        }

        private ElementView GetStructure(int? currentNode)
        {
            var rootElement = GetTaskHttpClient.Get<Contract.ElementView>("List", currentNode.ToString());
            CreateViewMetadata(rootElement);
            return rootElement;
        }

        System.Management.Automation.PSCmdlet cmdlet;

        internal TaskStructure(System.Management.Automation.PSCmdlet pSVariableIntrinsics)
        {
            this.cmdlet = pSVariableIntrinsics;
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






        public void SelectNodeByElementId(int value)
        {
            //this._sessionMetadata.
        }

        public void SelectNodeByOrder(int value)
        {
            this._sessionMetadata.SelectedNodeOrder = value;
        }
    }
}
