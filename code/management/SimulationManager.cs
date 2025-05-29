using _3D_Fluid_simulation.code.management;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public class SimulationManager
{
    private CameraController _camera;
    private FluidContainer _container;
    private ParticleSpawner _spawner;
    private float _spawnCooldown = 0.1f;
    private float _timeSinceLastSpawn = 0f;


    public SimulationManager(CameraController camera, FluidContainer container, ParticleSpawner spawner)
    {
        _camera = camera;
        _container = container;
        _spawner = spawner;
    }

    public void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        _timeSinceLastSpawn += GetFrameTime();

        if (IsMouseButtonDown(MouseButton.Left) && _timeSinceLastSpawn >= _spawnCooldown)
        {
            Ray ray = GetScreenToWorldRay(GetMousePosition(), _camera.Camera);

            if (RayIntersectsBox(ray, _container.Bounds, out Vector3 hitPoint))
            {
                _spawner.SpawnParticleAt(hitPoint);
                _timeSinceLastSpawn = 0f;
            }
        }
    }
    private bool RayIntersectsBox(Ray ray, BoundingBox box, out Vector3 hitPoint)
    {
        RayCollision collision = GetRayCollisionBox(ray, box);
        if (collision.Hit)
        {
            // Approximer le point d’impact en "marchant" le long du rayon
            for (float t = 0; t < 100; t += 0.01f)
            {
                Vector3 testPoint = ray.Position + ray.Direction * t;
                if (testPoint.X >= box.Min.X && testPoint.X <= box.Max.X &&
                    testPoint.Y >= box.Min.Y && testPoint.Y <= box.Max.Y &&
                    testPoint.Z >= box.Min.Z && testPoint.Z <= box.Max.Z)
                {
                    hitPoint = testPoint;
                    return true;
                }
            }
        }

        hitPoint = Vector3.Zero;
        return false;
    }
}

public struct Plane
{
    public Vector3 Normal;
    public float D;

    public Plane(Vector3 normal, float d)
    {
        Normal = normal;
        D = d;
    }
}
