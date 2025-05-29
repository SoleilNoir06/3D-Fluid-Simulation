using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace _3D_Fluid_simulation.code.management
{
    public class FluidContainer
    {
        public BoundingBox Bounds;

        public FluidContainer(Vector3 min, Vector3 max)
        {
            Bounds = new BoundingBox(min, max);
        }

        public void Draw()
        {
            Vector3 size = Bounds.Max - Bounds.Min;
            Vector3 center = (Bounds.Max + Bounds.Min) / 2.0f;
            DrawCubeWires(center, size.X, size.Y, size.Z, Color.White);
        }
    }
}
