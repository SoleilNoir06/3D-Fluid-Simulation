using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using _3D_Fluid_simulation.code.management;
using _3D_Fluid_simulation.code.initialisation;
using _3D_Fluid_simulation.code.entities;

class Program
{
    static void Main()
    {
        Window.Initialize();

        CameraController camera = new CameraController(new Vector3(10.0f, 10.0f, 10.0f), Vector3.Zero);
        FluidContainer container = new FluidContainer(new Vector3(-3, 0, -3), new Vector3(3, 6, 6));
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

            DrawGrid(26, 1);
            spawner.DrawParticles();
            container.Draw();

            EndMode3D();

            DrawText($"Number of particles : {ParticleSpawner.ParticleCount}", 10, 50, 40, Color.White);
            
            EndDrawing();
        }

        CloseWindow();
    }
}
