using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.ConsoleColors
{
    public class ColorString : IEnumerable<ColorStringItem>
    {
        private List<ColorStringItem> Items { get; set; }

        public ColorString()
        {
            this.Items = new List<ColorStringItem>();
        }

        public void Add(ColorStringItem item)
        {
            this.Items.Add(item);
        }

        public IEnumerator<ColorStringItem> GetEnumerator()
        {
            foreach (var item in this.Items)
            {
                yield return item;
            }
            
        }

        public override string ToString()
        {
            var result = string.Concat(Items.Select(x => x.Value));
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
