using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace _3D_Fluid_simulation.code.entities
{
    public class Particle
    {        
        public Vector3 Position;
        public Vector3 Velocity;
        public float Radius = 0.2f;

        // Fluid properties
        public float Mass;
        public float Viscosity;
        public float Temperature;
        public float Density;

        public Particle(Vector3 position)
        {
            Position = position;
            Velocity = new Vector3(0, -0.5f, 0);

            // Default fluid properties
            Mass = 1.0f;
            Viscosity = 0.1f; // Small value for water-like fluid
            Temperature = 20f; // Celsius
            Density = 1000.0f; // kg/m^3 for water
        }

        public void Update(BoundingBox box)
        {
            float dt = GetFrameTime();
            Console.WriteLine($"dt={dt}, pos={Position}, vel={Velocity}");

            // Gravity
            Velocity.Y -= 9.81f * GetFrameTime();

            // Apply viscosity damping
            Velocity *= (1.0f - Viscosity * GetFrameTime());

            Position += Velocity * GetFrameTime();

            // Collision with bottom of container
            if (Position.Y - Radius < box.Min.Y)
            {
                Position.Y = box.Min.Y + Radius;
                Velocity.Y *= -0.5f;
            }
        }

        public void Draw()
        {
            Color color = ColorFromTemperature(Temperature);
            Raylib.DrawSphere(Position, Radius, color);
        }

        private Color ColorFromTemperature(float temp)
        {
            // Blue (cold) to red (hot) gradient
            float t = Math.Clamp((temp - 0) / 100.0f, 0.0f, 1.0f);
            int r = (int)(255 * t);
            int b = (int)(255 * (1.0f - t));
            return new Color(r, 100, b, 255);
        }
    }
}
