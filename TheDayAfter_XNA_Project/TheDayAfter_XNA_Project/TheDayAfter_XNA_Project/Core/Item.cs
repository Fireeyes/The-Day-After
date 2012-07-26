using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheDayAfter_XNA_Project
{
    public class Item
    {
        public ItemSlots slot;
        public string name;
        public Item(ItemSlots slot, string name)
        {
            this.name = name;
            this.slot = slot;
        }
    }
}
