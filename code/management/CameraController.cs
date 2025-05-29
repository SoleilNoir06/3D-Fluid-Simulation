using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace _3D_Fluid_simulation.code.management
{
    public class CameraController
    {
        private const float _MIN_Y = 0.1f;
        
        public Camera3D Camera;

        private float _baseMoveSpeed = 5.0f;
        private float _sprintMultiplier = 2.5f;
        private float _rotateSpeed = 0.003f;
        private Vector3 _forward;
        private Vector3 _right;

        public CameraController(Vector3 position, Vector3 target)
        {
            Camera = new Camera3D
            {
                Position = position,
                Target = target,
                Up = new Vector3(0, 1, 0),
                FovY = 45.0f,
                Projection = CameraProjection.Perspective
            };
        }

        public void Update()
        {
            float delta = GetFrameTime();

            // Direction
            _forward = Vector3.Normalize(Camera.Target - Camera.Position);
            _right = Vector3.Normalize(Vector3.Cross(_forward, Camera.Up));

            // Sprint
            float moveSpeed = _baseMoveSpeed;
            if (IsKeyDown(KeyboardKey.LeftShift))
                moveSpeed *= _sprintMultiplier;

            // Mouvement
            if (IsKeyDown(KeyboardKey.W)) Camera.Position += _forward * moveSpeed * delta;
            if (IsKeyDown(KeyboardKey.S)) Camera.Position -= _forward * moveSpeed * delta;
            if (IsKeyDown(KeyboardKey.A)) Camera.Position -= _right * moveSpeed * delta;
            if (IsKeyDown(KeyboardKey.D)) Camera.Position += _right * moveSpeed * delta;
            if (IsKeyDown(KeyboardKey.Space)) Camera.Position += Camera.Up * moveSpeed * delta;
            if (IsKeyDown(KeyboardKey.LeftControl)) Camera.Position -= Camera.Up * moveSpeed * delta;

            // Stop camera from going through the floor
            if (Camera.Position.Y < _MIN_Y) Camera.Position.Y = _MIN_Y;

            // Rotate if middle button clicked
            if (IsMouseButtonDown(MouseButton.Middle))
            {
                Vector2 mouseDelta = GetMouseDelta();
                float yaw = -mouseDelta.X * _rotateSpeed;
                float pitch = -mouseDelta.Y * _rotateSpeed;

                // Rotation on vertical (yaw) and horizontal (pitch) axis
                Matrix4x4 yawMatrix = Matrix4x4.CreateFromAxisAngle(Camera.Up, yaw);
                Matrix4x4 pitchMatrix = Matrix4x4.CreateFromAxisAngle(_right, pitch);

                _forward = Vector3.Normalize(Vector3.Transform(_forward, yawMatrix * pitchMatrix));
            }

            Camera.Target = Camera.Position + _forward;
        }
    }
}
