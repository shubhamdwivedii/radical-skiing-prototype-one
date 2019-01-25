
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RadicalSkiingPrototypeOne.Core;
using System;
using System.Collections.Generic;

namespace RadicalSkiingPrototypeOne.Sprites
{
    public class Player : Sprite
    {
        public Vector2 CameraPosition;
        //private Vector2 _velocity;
        private float _angularVelocity = 6f;//3f;

        private float _maxHorizontalVelocity = 12f;
        
        

        private Vector2 _velocity;
        private float _slideVelocity;


        private float _minRotation = -1.5f, _maxRotation = 1.5f;   //  <- = 1.5  and -> = -1.5

        private Vector2 _defaultDirection;
       

        private Texture2D path;
        private List<Sprite> paths;
        private int pathCounter = 0;
        private int respawnCounter = 0;
        private Vector2 respawnPosition;

        private Sprite arrow;
        private Color _color = Color.White;

        private ParticleEngine particleEngine;
        private List<Texture2D> particles;

        public Player(Texture2D texture,Vector2 position,Game game) : base(texture)
        {
            Position = position;
            respawnPosition = Position;
            _velocity = new Vector2(0, 0);
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

            arrow = new Sprite(game.Content.Load<Texture2D>("Sprites/skiis64x96"));
            
            
            CameraPosition = new Vector2(Position.X, Position.Y + 50f);


            path = game.Content.Load<Texture2D>("Sprites/path_32x32");
            paths = new List<Sprite>();
            for (int i = 0; i < 150; i++)
            {
                paths.Add(new Sprite(path));
                paths[i].Position = new Vector2(Position.X - _texture.Width + path.Width / 2, Position.Y - _texture.Height);
            }
          //  _velocity = new Vector2(0f, 12f);
            _defaultDirection = new Vector2((float)Math.Cos(-MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(-MathHelper.ToRadians(90) - Rotation));

            particles = new List<Texture2D>();
            particles.Add(game.Content.Load<Texture2D>("Sprites/snow_particle_8x8"));
            particles.Add(game.Content.Load<Texture2D>("Sprites/gravel_particle_8x8"));
            particleEngine = new ParticleEngine(particles, Position);
        }

       

        public override void Update(GameTime gameTime)
        {


            var direction = new Vector2((float)Math.Cos(- MathHelper.ToRadians(90) - Rotation), -(float)Math.Sin(- MathHelper.ToRadians(90)- Rotation));


            CalculateVelocity();
            //Console.WriteLine("Acceleration :   " + _acceleration +"  Deceleration :  "+ _deceleration);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _velocity.Y -= 2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                Rotation = 0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Rotation += MathHelper.ToRadians(_angularVelocity);
                if (_slideVelocity > 0f)
                    _slideVelocity -= 0.4f;
                else
                    _slideVelocity = 0f;
                particleEngine.Generate();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Rotation -= MathHelper.ToRadians(_angularVelocity);
                if (_slideVelocity > 0f)
                    _slideVelocity -= 0.4f;
                else
                    _slideVelocity = 0f;
                particleEngine.Generate();
            }
            else //if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Rotation > 0f)
                    Rotation -= MathHelper.ToRadians(3f);
                else if(Rotation < 0f)
                    Rotation += MathHelper.ToRadians(3f);

                _slideVelocity += 0.2f;
            }
           
                 
            if (Rotation >= _maxRotation)
                Rotation = _maxRotation;
            else if (Rotation <= _minRotation)
                Rotation = _minRotation;


            if (_velocity.X >= _maxHorizontalVelocity)
                _velocity.X = _maxHorizontalVelocity;
            if (_velocity.X <= -_maxHorizontalVelocity)
                _velocity.X = -_maxHorizontalVelocity;

            Console.WriteLine("Velocity Y :  " + _velocity.Y + "  Velocity X :  " +_velocity.X);
            //Console.WriteLine("Rotation : " + Rotation + "  Direction X : " + direction.X);

           

            Position += _velocity; //direction * _velocity.Y;
            //Position += new Vector2(0, _altVelocity);
            //position updates//
            if (_slideVelocity >= 10f)
                _slideVelocity = 10f;
            Position.Y += _slideVelocity;

            particleEngine.EmitterLocation = new Vector2(Position.X - Origin.X, Position.Y - Origin.Y);
            particleEngine.Update();

            paths[pathCounter].Position = new Vector2(Position.X - _texture.Width + path.Width, Position.Y - _texture.Height + 64f);
            paths[pathCounter].Rotation = Rotation;
            pathCounter++;

            if (pathCounter >= 150)
                pathCounter = 0;

            CameraPosition.Y = Position.Y + 50f;
           
            arrow.Position = new Vector2(Position.X - _texture.Width/2, Position.Y - _texture.Height/2 + 30f);
            arrow.Rotation = Rotation;

        }

        private void CalculateVelocity()
        {
            var k = 1.8f;//1.6f;//2f;
            var v = 4f;//2f;
            var d = 2f;//3f;
            var coeff = 0f;

            if (Rotation > 0)
            {
                coeff = (k - Rotation) * v;
               
                _velocity.Y = ( d * coeff );
                _velocity.X = -12f * (Rotation);
                //_velocity.X = d * (-(4 - coeff));
            }
            else
            {
                coeff = (k + Rotation) * v;
                
                _velocity.Y =( d * coeff);
                _velocity.X = 12f * (-Rotation);
                //_velocity.X = d * (4 - coeff);
            }
        }

        public void CheckCollision(List<Sprite> sprites)
        {
            foreach(Sprite sprite in sprites)
            {
                
                if (sprite.Rectangle.Contains(Position.X, Position.Y))
                {
                    _color = Color.Red;
                    respawnCounter++;
                }
                if (respawnCounter >= 5)
                {
                    Position = respawnPosition;
                    _color = Color.White;
                    respawnCounter = 0;
                }

                
            }
        }
        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in paths)
            {
                sprite.Draw(gameTime, spriteBatch,true);
            }
            particleEngine.Draw(spriteBatch);
            arrow.Draw(gameTime, spriteBatch, true);
            spriteBatch.Draw(_texture, new Vector2(Position.X - Origin.X, Position.Y - Origin.Y), null, _color, 0f, Origin, 1f, SpriteEffects.None, 0f);
            // spriteBatch.Draw(_texture, Origin, Color.White);
            
            
            
        }
    }
}
