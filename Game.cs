using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;


namespace OpenTKCasa3D
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        Shader shader;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

        Casa casa1;
        Casa casa2;
        Casa casa3;

        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.0f, 1.0f, 0.0f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
           
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            
            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -2.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45.0f));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            casa1 = new Casa(shader, new Vector3( -0.5f, 0.0f, 0.0f));
            casa2 = new Casa(shader, new Vector3( 0.5f, 0.0f, 0.0f));
            casa3 = new Casa(shader, new Vector3( 0.0f, -0.5f, 0.0f));
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

            casa1.dibujar();
            casa2.dibujar();
            casa3.dibujar();

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
