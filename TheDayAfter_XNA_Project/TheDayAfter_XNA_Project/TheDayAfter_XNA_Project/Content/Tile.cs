using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDayAfter_XNA_Project
{
    public class TileRow
    {
        public List<Tile> Y = new List<Tile>();
        public TileRow(List<Tile> input)
        {
            Y = input;
        }
    }
    public class Tile
    {
        public int Environment;
        public int Id;
        public Tile(int environment, int ID)
        {
            Environment = environment;
            Id = ID;
        }
    }
}
