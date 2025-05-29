using Raylib_cs;
using static Raylib_cs.Raylib;

namespace _3D_Fluid_simulation.code.initialisation
{
    public static class Window
    {
        /// <summary>
        /// Initialize window
        /// </summary>
        public static void Initialize()
        {
            //Set window title, size and position
            InitWindow(0, 0, "3D Fluid simulation");

            int screenWidth = GetMonitorWidth(0);
            int screenHeight = GetMonitorHeight(0);

            SetWindowSize(screenWidth, screenHeight);

            SetWindowPosition((GetMonitorWidth(0) - screenWidth) / 2, (GetMonitorHeight(0) - screenHeight) / 2);

            //Set max FPS
            SetTargetFPS(144);
        }
    }
}
