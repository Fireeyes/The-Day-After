using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheDayAfter_XNA_Project.Database;

namespace TheDayAfter_XNA_Project
{
    static class Player
    {
        public static Dictionary<ItemSlots, Item> Inventory = new Dictionary<ItemSlots, Item>();
        // Inventory[itemToBeEquipped.slot]=itemToBeEquipped;                   <---- EXEMPLU
        // public static Item Headpiece = new Item(ItemSlots.head, "Helmet");   <---- EXEMPLU

    }
}
