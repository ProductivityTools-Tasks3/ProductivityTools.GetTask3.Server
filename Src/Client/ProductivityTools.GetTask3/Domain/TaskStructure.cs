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

        private SessionMetadata SessionMetadata
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

        private Dictionary<int, SessionElementMetadata> ItemOrder
        {
            get
            {
                var r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                if (r == null)
                {
                    cmdlet.SessionState.PSVariable.Set(_sesisonKey, new Dictionary<int, SessionElementMetadata>());
                    r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                }
                return (Dictionary<int, SessionElementMetadata>)r.Value;
            }
        }

        private ElementView structure;
        private ElementView Structure
        {
            get
            {
                int? currentNode = null;
                if (structure == null)
                {
                    structure = GetStructure();
                }
                return structure;
            }
        }

        private ElementView GetStructure(int? currentNode = null)
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
            ItemOrder.Clear();

            Action<ElementType> fillOrder = (type) =>
            {
                if (root != null)
                {
                    foreach (var element in root.Elements.Where(x => x.Type == type))
                    {

                        ItemOrder.Add(element.ElementId, new SessionElementMetadata()
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
                    var sessionelement = this.ItemOrder.First(x => x.Key == element.ElementId);
                    yield return new PSElementView() { Element = element, SessionElement = sessionelement.Value };
                }
            }
        }

        private int? _curentNodeElementId;
        public int? CurentNodeElementId
        {
            get
            {
                return _curentNodeElementId;
            }
        }

        private int? _selectedNodeOrder { get; set; }

        private int _selectedNodeElementId { get; set; }
        public int? SelectedNodeElementId
        {
            get
            {
                if (_selectedNodeOrder.HasValue)
                {
                    var currentElement = this.ItemOrder.SingleOrDefault(x => x.Value.Order == _selectedNodeOrder);
                    {
                        return currentElement.Key;
                    }
                }
                else
                {
                    return _curentNodeElementId;
                }
            }
        }

        public void SelectNodeByElementId(int value)
        {
            _curentNodeElementId = value;
        }

        public void SelectNodeByOrder(int value)
        {
            _selectedNodeOrder = value;
        }
    }
}
