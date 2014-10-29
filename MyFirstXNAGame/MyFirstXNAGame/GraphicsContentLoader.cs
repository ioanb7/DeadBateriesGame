using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GraphicsContentLoader
    {
        Dictionary<string, Texture2D> graphicsCache;
        ContentManager contentManager;

        public GraphicsContentLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            graphicsCache = new Dictionary<string, Texture2D>();
        }

        public Texture2D Get(string name)
        {
            Load(name);
            return graphicsCache[name];
        }

        public void Load(string name)
        {
            if (graphicsCache.ContainsKey(name) == false)
                graphicsCache.Add(name, contentManager.Load<Texture2D>(name));
        }
    }
}
