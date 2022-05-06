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

        Shader shader;

        private int vertexBufferObject;
        private int vertexArrayObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

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
            
            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -1.5f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(50.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-30.0f));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            Json json = new Json();

            //json.serializeObjeto(casa, "Casa");

            Objeto casa = json.deserializeObjeto("Casa.json"/*, shader*/);
            casa.setCentro(new Punto(0.5f, 0.0f, 0.0f));
            casa.setName("CASA 1");
            casa.setShader(shader);

            Objeto casa2 = json.deserializeObjeto("Casa.json"/*, shader*/);
            casa2.setCentro(new Punto(-0.5f, 0.0f, 0.0f));
            casa2.setName("CASA 2");
            casa2.setShader(shader);
            

            escenario = new Escenario("Mi Primero Escenario");
            escenario.Add(casa);
            escenario.Add(casa2);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

            escenario.Dibujar();

            Context.SwapBuffers();

            IDrawable test = new Escenario();
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
