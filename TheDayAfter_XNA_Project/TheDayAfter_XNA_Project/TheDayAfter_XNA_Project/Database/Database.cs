using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Packaging;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Reflection;
using System.Collections;


namespace TheDayAfter_XNA_Project.Database
{
    static class Database
    {
        /* crap
        public static Dictionary<string, Item > ItemList  = new Dictionary<string,Item> ();
        public static Dictionary<string, Skill> SpellList = new Dictionary<string,Skill>();
        public static Dictionary<string, TileMap> TileMapList= new Dictionary<string, TileMap>();
         * end of crap*/
        public static Package World;
        static void Load()
        {
            World=Package.Open("Data/World.wif");
        }
        static void Test()
        {
            PackagePart test=World.GetPart(PackUriHelper.ResolvePartUri("/test.png",
       
    }
}
