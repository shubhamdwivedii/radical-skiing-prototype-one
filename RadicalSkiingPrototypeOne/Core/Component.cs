using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RadicalSkiingPrototypeOne.Core
{
    public abstract class Component
    {

        public abstract void Draw(GameTime gameTime, SpriteBatch spritreBatch);

        public abstract void Update(GameTime gameTime);
    }
}
