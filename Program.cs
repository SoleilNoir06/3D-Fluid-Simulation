using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using _3D_Fluid_simulation.code.management;
using _3D_Fluid_simulation.code.initialisation;

class Program
{
    static void Main()
    {
        Window.Initialize();

        CameraController camera = new CameraController(new Vector3(10.0f, 10.0f, 10.0f), Vector3.Zero);
        FluidContainer container = new FluidContainer(new Vector3(-3, 0, -3), new Vector3(3, 6, 3));
        ParticleSpawner spawner = new ParticleSpawner(container, 50);
        SimulationManager simulation = new SimulationManager(camera, container, spawner);

        while (!WindowShouldClose())
        {
            camera.Update();
            spawner.UpdateParticles();
            simulation.Update(); 

            BeginDrawing();
            ClearBackground(new Color(50, 50, 50, 255));

            BeginMode3D(camera.Camera);

            DrawGrid(20, 1);
            container.Draw();
            spawner.DrawParticles();

            EndMode3D();

            DrawText("Clic gauche pour ajouter une particule dans le bac", 10, 10, 20, Color.LightGray);

            EndDrawing();
        }

        CloseWindow();
    }
}
