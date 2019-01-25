using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadicalSkiingPrototypeOne.Core
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(random.NextDouble() * 2 - 1),
                1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVeocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = Color.White;//new Color(
               // (float)random.NextDouble(),
               // (float)random.NextDouble(),
               // (float)random.NextDouble());
            float size = 1.5f + (float)random.NextDouble();
            int ttl = 20 + random.Next(40);
            return new Particle(texture, position, velocity, angle, angularVeocity, color, size, ttl);
        }

        public void Update()
        {

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Generate()
        {
            int total = 10;
            for (int i = 0; i < total; i++)
            {
                particles.Add(GenerateNewParticle());
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);

            }
            //spriteBatch.End();
        }
    }
}
