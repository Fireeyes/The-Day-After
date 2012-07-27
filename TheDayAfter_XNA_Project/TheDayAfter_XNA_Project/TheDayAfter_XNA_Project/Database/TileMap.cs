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
using TheDayAfter_XNA_Project.Lighting;
using TheDayAfter_XNA_Project.Database;
using TheDayAfter_XNA_Project.Sprites;


namespace TheDayAfter_XNA_Project.Database
{
    class TileMap
    {
        Color AmbientLight;  //dynamic Day/Night Ambient Light.
        List<LightSource> LightSourceList=new List<LightSource>();   //static lightsources- to be implemented
        String Environment;   //set of tiles
        int MaxX;
        int MaxY; 

        List<Decal> DecaList = new List<Decal>();
        TileMap(String filename)
        {
            TextReader fileReader = new StreamReader(filename);
            String line;
            line = fileReader.ReadLine();
            Environment = line;
            MaxX = Convert.ToInt32(fileReader.ReadLine());
            MaxY = Convert.ToInt32(fileReader.ReadLine());
            int x, y;
            List<TileRow> X = new List<TileRow>();
            //add reading lines and rows

        }
    }
}
