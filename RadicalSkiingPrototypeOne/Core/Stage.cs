using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RadicalSkiingPrototypeOne.Sprites;
using System.Collections.Generic;

namespace RadicalSkiingPrototypeOne.Core
{
    public class Stage
    {
        public List<Component> Components;

        public Stage()
        {
            Components = new List<Component>();
        }

        public void Add(Component component)
        {
            Components.Add(component);
        }

        public void Add(List<Component> components)
        {
            foreach(Component component in components)
            {
                this.Add(component);
            }
        }

        public void Add(List<Sprite> sprites)
        {
            foreach (Sprite sprite in sprites)
            {
                this.Add(sprite);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(Component component in Components)
            {
                component.Update(gameTime);
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix transformMatrix)
        {
            spriteBatch.Begin(transformMatrix: transformMatrix);

            foreach (Component component in Components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

        }

    }
}
