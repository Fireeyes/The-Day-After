using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using System.Collections;


namespace TheDayAfter_XNA_Project
{
    static class Database
    {
        /* crap
        public static Dictionary<string, Item > ItemList  = new Dictionary<string,Item> ();
        public static Dictionary<string, Skill> SpellList = new Dictionary<string,Skill>();
        public static Dictionary<string, TileMap> TileMapList= new Dictionary<string, TileMap>();
         * end of crap*/
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        public static void Load(ContentManager Content)
        {
            Fonts.Add("debug", Content.Load<SpriteFont>(@"Fonts\debug"));

        }
       
    }
}
