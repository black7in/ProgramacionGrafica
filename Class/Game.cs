using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        private int vertexBufferObject;
        private int vertexArrayObject;

        Escenario escenario;
        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
           
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            

            Json json = new Json();

            //json.serializeObjeto(casa, "Casa");

            Objeto casa = json.deserializeObjeto("Casa1.json");

            Parte parte = casa.getParte("MuroFrontal");
            //parte.Escalar(1.0f, 2.0f, 1.0f);
            parte.Rotar(25.0f);

            casa.Escalar(1.0f, 1.0f);
            //casa.Trasladar(0.5f, 0.5f);

           // Objeto casa2 = json.deserializeObjeto("Casa2.json");
            

            escenario = new Escenario("Mi Primero Escenario");
            escenario.Add(casa);
            //escenario.Add(casa2);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);

            escenario.Dibujar();
            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e) {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload() {
            base.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
