using _3D_Fluid_simulation.code.entities;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace _3D_Fluid_simulation.code.management
{
    public class ParticleSpawner
    {
        private List<Particle> _particles = new List<Particle>();
        private FluidContainer _container;
        private Random _random = new Random();

        public ParticleSpawner(FluidContainer container, int count)
        {
            _container = container;
        }

        public void UpdateParticles()
        {
            foreach (Particle p in _particles)
                p.Update(_container.Bounds);

            ResolveParticleCollisions();
        }

        public void DrawParticles()
        {
            foreach (Particle p in _particles)
                p.Draw();
        }

        public void SpawnParticleAt(Vector3 position)
        {
            _particles.Add(new Particle(position));
        }

        public void ResolveParticleCollisions()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                for (int j = i + 1; j < _particles.Count; j++)
                {
                    Particle a = _particles[i];
                    Particle b = _particles[j];

                    Vector3 delta = b.Position - a.Position;
                    float distance = delta.Length();
                    float minDist = a.Radius + b.Radius;

                    if (distance < minDist && distance > 0.0001f)
                    {
                        // Normal vector
                        Vector3 normal = Vector3.Normalize(delta);

                        // Calculate penetration depth
                        float penetration = minDist - distance;

                        // Separate particles to avoid overlap (50% / 50%)
                        a.Position -= normal * (penetration / 2f);
                        b.Position += normal * (penetration / 2f);

                        // Relative velocity
                        Vector3 relativeVelocity = b.Velocity - a.Velocity;

                        // Velocity along normal
                        float velAlongNormal = Vector3.Dot(relativeVelocity, normal);

                        // Only respond if they are moving towards each other
                        if (velAlongNormal < 0)
                        {
                            float restitution = 0.5f; // Bounciness [0 = inelastic, 1 = perfectly elastic]
                            float impulse = -(1 + restitution) * velAlongNormal / 2;

                            Vector3 impulseVector = impulse * normal;
                            a.Velocity -= impulseVector;
                            b.Velocity += impulseVector;
                        }
                    }
                }
            }
        }

    }
}
