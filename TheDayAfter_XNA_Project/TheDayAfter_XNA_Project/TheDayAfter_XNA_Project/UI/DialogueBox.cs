using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheDayAfter_XNA_Project.UI
{
    public class DialogueBox
    {
        public Rectangle box;
        public Texture2D debugColor;
        public SpriteFont font;
        public String text;
        public String parsedText;
        public String typedText;
        public double typedTextLength;
        public int delayInMilliseconds;
        public bool isDoneDrawing;
        public string type;

        public DialogueBox(Rectangle box, SpriteFont font, Texture2D debugColor, String text, int delayInMilliseconds)
        {
            this.box = box;
            this.font = font;
            this.debugColor = debugColor;
            this.text = text;
            this.parsedText = parseText(text);
            this.delayInMilliseconds = delayInMilliseconds;
            this.isDoneDrawing = false;
        }

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > box.Width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, typedText, new Vector2(box.X, box.Y), Color.White);
        }

        internal void Update(GameTime gameTime)
        {
            if (!isDoneDrawing)
            {
                if (delayInMilliseconds == 0)
                {
                    typedText = parsedText;
                    isDoneDrawing = true;
                }
                else if (typedTextLength < parsedText.Length)
                {
                    typedTextLength = typedTextLength + gameTime.ElapsedGameTime.TotalMilliseconds / delayInMilliseconds;

                    if (typedTextLength >= parsedText.Length)
                    {
                        typedTextLength = parsedText.Length;
                        isDoneDrawing = true;
                    }

                    typedText = parsedText.Substring(0, (int)typedTextLength);
                }
            }
        }
    }
}