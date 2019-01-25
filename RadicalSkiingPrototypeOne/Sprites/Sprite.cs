using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RadicalSkiingPrototypeOne.Core;

namespace RadicalSkiingPrototypeOne.Sprites
{
    public class Sprite : Component
    {
        protected Texture2D _texture;
        public Vector2 Position;// { get; set; } //need to be set
        public Vector2 Origin { get; set; } //used for rotation
        public float Rotation = 0f; //default
        public float Scale = 1f;  //default
        public Rectangle Rectangle  //need to be set
        {
            get;
            //{
             //   return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
           // }
            set;
        }
        
        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
         
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool rotate)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
