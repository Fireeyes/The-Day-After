using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheDayAfter_XNA_Project
{
    static class Database
    {
        public static Dictionary<string, Item > Items  = new Dictionary<string,Item> ();
        public static Dictionary<string, Spell> Spells = new Dictionary<string,Spell>();
    }
}
