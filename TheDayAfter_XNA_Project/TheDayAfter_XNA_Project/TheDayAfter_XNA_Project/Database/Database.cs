using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using System.Collections;
using TheDayAfter_XNA_Project.Sprites;

namespace TheDayAfter_XNA_Project.Database
{
    static class Database
    {
        public static Dictionary<string, Item > ItemList  = new Dictionary<string,Item> ();
        public static Dictionary<string, Spell> SpellList = new Dictionary<string,Spell>();
        public static Dictionary<string, TileMap> TileMapList= new Dictionary<string, TileMap>();
    }
}
