using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public class SessionMetadata
    {
        public Dictionary<int, SessionElementMetadata> ItemOrder { get; set; }

        public SessionMetadata()
        {
            this.ItemOrder = new Dictionary<int, SessionElementMetadata>();
        }

        //private int? _curentNodeElementId;
        //public int? CurentNodeElementId
        //{
        //    get
        //    {
        //        return _curentNodeElementId;
        //    }
        //}


        public int? SelectedNodeOrder { get; set; }

        //private int _selectedNodeElementId { get; set; }
        //public int? SelectedNodeElementId
        //{
        //    get
        //    {
        //        if (_selectedNodeOrder.HasValue )
        //        {
        //            var currentElement = this.ItemOrder.SingleOrDefault(x => x.Value.Order == _selectedNodeOrder);
        //            {
        //                return currentElement.Key;
        //            }
        //        }
        //        else
        //        {
        //            return _curentNodeElementId;
        //        }
        //    }
        //}

        //public void SelectNodeByElementId(int value)
        //{
        //    _curentNodeElementId = value;
        //}

        //public void SelectNodeByOrder(int value)
        //{
        //    _selectedNodeOrder = value;
        //}
    }
}
