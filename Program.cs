using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace OpenTKCasa3D
{
    class Program
    {
        static void Main(string[] args) {
            var nativeWindowSettings = new NativeWindowSettings() {
                Size = new Vector2i(1000, 800),
                Title = "Primera practica.",
            };

            using (var game = new Game(GameWindowSettings.Default, nativeWindowSettings)) {
                game.Run();
            }
        }
    }
}
