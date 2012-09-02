using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDayAfter_XNA_Project
{
    public static class ParticleSystem
    {
        public static List<Emitter> EmitterList;
        static Vector2 position;
        public static Vector2 Position
        {
            get { return position; }
            set { LastPos = position; position = value; }
        }
        public static Vector2 LastPos;
        static Random random;

        public static void Initialize(Vector2 pos)
        {
            Position = pos;
            LastPos = pos;
            random = new Random();
            EmitterList = new List<Emitter>();
        }

        public static void Update(float dt)
        {
            for (int i = 0; i < EmitterList.Count; i++)
            {
                if (EmitterList[i].Budget > 0)
                {
                    EmitterList[i].Update(dt);
                    if (Vector2.Distance(EmitterList[i].RelPosition, new Vector2(512, 512)) > 5000)
                    {
                        EmitterList[i].Clear();
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch, int Scale, Vector2 Offset)
        {
            for (int i = 0; i < EmitterList.Count; i++)
            {
                if (EmitterList[i].Budget > 0)
                {
                    EmitterList[i].Draw(spriteBatch, Scale, Offset);
                }
            }
        }

        public static void Clear()
        {
            for (int i = 0; i < EmitterList.Count; i++)
            {
                if (EmitterList[i].Budget > 0)
                {
                    EmitterList[i].Clear();
                }
            }
        }

        public static void AddEmitter(Vector2 SecPerSpawn, Vector2 SpawnDirection, Vector2 SpawnNoiseAngle, Vector2 StartLife, Vector2 StartScale,
                    Vector2 EndScale, Color StartColor1, Color StartColor2, Color EndColor1, Color EndColor2, Vector2 StartSpeed,
                    Vector2 EndSpeed, int Budget, Vector2 RelPosition, Texture2D ParticleSprite)
        {
            Emitter emitter = new Emitter(SecPerSpawn, SpawnDirection, SpawnNoiseAngle,
                                        StartLife, StartScale, EndScale, StartColor1,
                                        StartColor2, EndColor1, EndColor2, StartSpeed,
                                        EndSpeed, Budget, RelPosition, ParticleSprite, random);
            EmitterList.Add(emitter);
        }

        public static void AddEmitter(Vector2 SecPerSpawn, Vector2 SpawnDirection, Vector2 SpawnNoiseAngle, Vector2 StartLife, Vector2 StartScale,
                   Vector2 EndScale, Color StartColor1, Color StartColor2, Color EndColor1, Color EndColor2, Vector2 StartSpeed,
                   Vector2 EndSpeed, int Budget, Vector2 RelPosition, Texture2D ParticleSprite, float InitialLife)
        {
            Emitter emitter = new Emitter(SecPerSpawn, SpawnDirection, SpawnNoiseAngle,
                                        StartLife, StartScale, EndScale, StartColor1,
                                        StartColor2, EndColor1, EndColor2, StartSpeed,
                                        EndSpeed, Budget, RelPosition, ParticleSprite, random, InitialLife);
            EmitterList.Add(emitter);
        }
    }
}
