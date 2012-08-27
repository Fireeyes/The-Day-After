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
using TheDayAfter_XNA_Project.Player_Interface;

namespace TheDayAfter_XNA_Project
{
    //class TileMap
    //{
    //    //Color AmbientLight;  //dynamic Day/Night Ambient Light.
    //    List<LightSource> LightSourceList = new List<LightSource>();   //static lightsources- to be implemented
    //    int MaxX; //number of collumns
    //    int MaxY; //number of rows
    //    List<TileRow> X = new List<TileRow>();
    //    TileMap(String filename) //file reading
    //    {
    //        TextReader fileReader = new StreamReader(filename);
    //        //String line;
    //        //line = fileReader.ReadLine();
    //        MaxX = Convert.ToInt32(fileReader.ReadLine());
    //        MaxY = Convert.ToInt32(fileReader.ReadLine());
    //        int x, y;
    //        for (x = 0; x <= MaxX; x++)
    //        {
    //            List<Tile> currentY = new List<Tile>();
    //            for (y = 0; y <= MaxY; y++) //creting the vertical TileRow
    //                // structure X0-Y0  X1-Y0   X2-Y0
    //                //           X0-Y1  X1-Y1   X2-Y2
    //                //           X0-Y2  X1-Y2   X2-Y2
    //                //Generting the Y row first and then adding it to the X structure
    //            {
    //                currentY.Add(new Tile(
    //                   Convert.ToInt32(fileReader.ReadLine()), //Tile.Environment
    //                   Convert.ToInt32(fileReader.ReadLine())  //Tile.Id
    //                   ));
    //            }
    //            X.Add(new TileRow(currentY));   //Tabbing to a new X
    //        }
    //    }
    //    void Draw(SpriteBatch spriteBatch)  //Doesn't do anything
    //    {
    //        foreach(TileRow row in X)
    //        {
    //            foreach(Tile tile in row.Y)
    //                tile.Draw(spriteBatch);
    //        }
    //    }
    //}
    public class TileMap
    {
        int MaxX = 32; //number of collumns
        int MaxY = 32; //number of rows
        public List<TileRow> X = new List<TileRow>();
        Texture2D texture;
        public void Load(Texture2D pixelMap, Texture2D tilemap)
        {
            byte[] pixel = new byte[4];
            for (int i = 0; i <= MaxX-1; i++)
            {
                List<Tile> currentY = new List<Tile>();
                for (int j = 0; j <= MaxY-1; j++)
                {

                    pixelMap.GetData(0, new Rectangle(i, j, 1, 1), pixel, 0, 4);
                    //pixel[0] Red
                    //pixel[1] Green
                    //pixel[2] Blue
                    //pixel[3] Alpha
                    /*if (pixel[0] == 4278190080)     // BLACK
                    {
                        currentY.Add(new Tile(1,0));
                    }*/
                    if (pixel[0] == 255) // RED
                    {
                        currentY.Add(new Tile(1, 1));
                    }
                    else if (pixel[1] ==255 ) // Green
                    {
                        currentY.Add(new Tile(1, 0));
                    }
                }
                X.Add(new TileRow(currentY));
            }
            texture = tilemap;
        }

        public void Draw(SpriteBatch spriteBatch)  //Doesn't do anything
        {
            int x = 0, y = 0;
            
            foreach(TileRow row in X)
            {   x=0;
                foreach (Tile tile in row.Y)
                {
                    spriteBatch.Draw(texture, new Vector2(y * 64, x * 64) - new Vector2(Player.position.X - 320, Player.position.Y - 320), new Rectangle(tile.Id * 64, 0, 64, 64), Color.White);
                    
                    x++;
                }
                y++;
            }
        }
    }
}
