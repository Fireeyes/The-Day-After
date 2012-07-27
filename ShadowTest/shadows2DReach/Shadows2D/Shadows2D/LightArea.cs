using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Shadows2D
{
    class LightArea
    {
        private GraphicsDevice graphicsDevice;

        public RenderTarget2D RenderTarget { get; private set; }
        public Vector2 LightPosition { get; set; }
        public Vector2 LightAreaSize { get; set; }
        public int baseSize;

        public LightArea(GraphicsDevice graphicsDevice, ShadowmapSize size)
        {
            baseSize = 2 << (int)size;
            LightAreaSize = new Vector2(baseSize);
            RenderTarget = new RenderTarget2D(graphicsDevice, baseSize, baseSize);
            this.graphicsDevice = graphicsDevice;
        }

        public Vector2 ToRelativePosition(Vector2 worldPosition)
        {
            return worldPosition - (LightPosition - LightAreaSize * 0.5f);
        }

        public void BeginDrawingShadowCasters()
        {
            graphicsDevice.SetRenderTarget(RenderTarget);
            graphicsDevice.Clear(Color.Transparent);
        }

        public void EndDrawingShadowCasters()
        {
            graphicsDevice.SetRenderTarget(null);
        }
    }
}
