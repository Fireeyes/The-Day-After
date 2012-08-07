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
        List<LightSource> LightSourceList = new List<LightSource>();   //static lightsources- to be implemented
        int MaxX; //number of collumns
        int MaxY; //number of rows
        List<TileRow> X = new List<TileRow>();
        List<Decal> DecaList = new List<Decal>(); //list of static decals
        TileMap(String filename) //file reading
        {
            TextReader fileReader = new StreamReader(filename);
            //String line;
            //line = fileReader.ReadLine();
            MaxX = Convert.ToInt32(fileReader.ReadLine());
            MaxY = Convert.ToInt32(fileReader.ReadLine());
            int x, y;
            for (x = 0; x <= MaxX; x++)
            {
                List<Tile> currentY = new List<Tile>();
                for (y = 0; y <= MaxY; y++)
                {
                    currentY.Add(new Tile(
                       Convert.ToInt32(fileReader.ReadLine()), //Tile.Environment
                       Convert.ToInt32(fileReader.ReadLine())  //Tile.Id
                       ));
                }
                X.Add(new TileRow(currentY));
            }
        }
        void Draw(SpriteBatch spriteBatch)
        {
            foreach(TileRow row in X)
            {
                foreach(Tile tile in row.Y)
                    tile.Draw(spriteBatch);
            }
        }
    }
}
