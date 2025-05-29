using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace _3D_Fluid_simulation.code.management
{
    public class FluidContainer
    {
        public BoundingBox Bounds;
        public float WallTickness = 0.1f;
        public float OverflowHeight => Bounds.Max.Y - 0.1f;

        public FluidContainer(Vector3 min, Vector3 max)
        {
            Bounds = new BoundingBox(min, max);
        }

        public void Draw()
        {
            Vector3 size = Bounds.Max - Bounds.Min;
            Vector3 center = (Bounds.Max + Bounds.Min) / 2.0f;

            // Murs gauche et droit
            DrawCube(new Vector3(Bounds.Min.X + WallTickness / 2, center.Y, center.Z), WallTickness, size.Y, size.Z, ColorAlpha(Color.LightGray, 0.5f));
            DrawCubeWires(new Vector3(Bounds.Min.X + WallTickness / 2, center.Y, center.Z), WallTickness, size.Y, size.Z, Color.DarkGray);
            DrawCube(new Vector3(Bounds.Max.X - WallTickness / 2, center.Y, center.Z), WallTickness, size.Y, size.Z, ColorAlpha(Color.LightGray, 0.5f));
            DrawCubeWires(new Vector3(Bounds.Max.X - WallTickness / 2, center.Y, center.Z), WallTickness, size.Y, size.Z, Color.DarkGray);

            // Murs avant et arrière
            DrawCube(new Vector3(center.X, center.Y, Bounds.Min.Z + WallTickness / 2), size.X, size.Y, WallTickness, ColorAlpha(Color.LightGray, 0.5f));
            DrawCubeWires(new Vector3(center.X, center.Y, Bounds.Min.Z + WallTickness / 2), size.X, size.Y, WallTickness, Color.DarkGray);
            DrawCube(new Vector3(center.X, center.Y, Bounds.Max.Z - WallTickness / 2), size.X, size.Y, WallTickness, ColorAlpha(Color.LightGray, 0.5f));
            DrawCubeWires(new Vector3(center.X, center.Y, Bounds.Max.Z - WallTickness / 2), size.X, size.Y, WallTickness, Color.DarkGray);

            // Fond
            DrawCube(new Vector3(center.X, Bounds.Min.Y + WallTickness / 2, center.Z), size.X, WallTickness, size.Z, ColorAlpha(Color.LightGray, 0.5f));
            DrawCubeWires(new Vector3(center.X, Bounds.Min.Y + WallTickness / 2, center.Z), size.X, WallTickness, size.Z, Color.DarkGray);
        }
    }
}
