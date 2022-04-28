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

        Objeto casa;
        Objeto casa2;
        protected override void OnLoad() {
            base.OnLoad();

            GL.ClearColor(0.0f, 1.0f, 0.0f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
           
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -1.5f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(50.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            //model = Matrix4.Identity * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(45.0f));

            //shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            /*  PARTES DEL OBJETO CASA.
             *  techo1
             *  techo2
             *  muroFrontal
             *  muroTrasero
             *  muroLateralIzquierdo
             *  muroLateralDerecho
             *  puerta
             *  ventanaLateraIzquiero
             *  ventanaLateralDerecho
             */

            Parte frontWall = new Parte(shader, "MuroFrontal", new Punto(0.0f, 0.0f, 0.15f), 40.0f);
            frontWall.Add(1, new Punto(-0.15f, -0.20f, 0.0f));
            frontWall.Add(2, new Punto(-0.15f,  0.20f, 0.0f));
            frontWall.Add(3, new Punto( 0.15f,  0.20f, 0.0f));
            frontWall.Add(4, new Punto( 0.15f, -0.20f, 0.0f));

            Parte rearWall = new Parte(shader, "MuroTrasero", new Punto(0.0f, 0.0f, -0.15f), 40.0f);
            rearWall.Add(1, new Punto(-0.15f, -0.20f, 0.0f));
            rearWall.Add(2, new Punto(-0.15f,  0.20f, 0.0f));
            rearWall.Add(3, new Punto( 0.15f,  0.20f, 0.0f));
            rearWall.Add(4, new Punto( 0.15f, -0.20f, 0.0f));
            
            Parte leftWall = new Parte(shader, "MuroIzquierdo", new Punto(-0.15f, 0.0f, 0.0f), 40.0f);
            leftWall.Add(1, new Punto(0.0f, -0.20f, -0.15f));
            leftWall.Add(2, new Punto(0.0f,  0.20f, -0.15f));
            leftWall.Add(3, new Punto(0.0f,  0.20f,  0.15f));
            leftWall.Add(4, new Punto(0.0f, -0.20f,  0.15f));
            
            Parte rightWall = new Parte(shader, "MuroDerecho", new Punto(0.15f, 0.0f, 0.0f), 40.0f);
            rightWall.Add(1, new Punto(0.0f, -0.20f, -0.15f));
            rightWall.Add(2, new Punto(0.0f,  0.20f, -0.15f));
            rightWall.Add(3, new Punto(0.0f,  0.20f,  0.15f));
            rightWall.Add(4, new Punto(0.0f, -0.20f,  0.15f));
            
            //Parte roof1 = new Parte(shader, "Techo1", new Punto(0.0f, 0.0f, 0.0f));
            //roof1.Add(1, new Punto());

            Parte test = new Parte(shader, "test", new Punto(0.0f, 0.0f, 0.0f), 0.0f);
            test.Add(1, new Punto(-1.0f, 0.0f, 0.0f));
            test.Add(2, new Punto(1.0f, 0.0f, 0.0f));

            Parte test2 = new Parte(shader, "test2", new Punto(0.0f, 0.0f, 0.0f), 0.0f);
            test2.Add(1, new Punto(0.0f, -1.0f, 0.0f));
            test2.Add(2, new Punto(0.0f, 1.0f, 0.0f));

            casa = new Objeto("Casa", new Punto(0.0f, 0.0f, 0.0f));

            casa.Add(frontWall);
            casa.Add(rearWall);
            casa.Add(leftWall);
            casa.Add(rightWall);

         

            casa2 = new Objeto("Casa 2", new Punto(0.0f, 0.0f, 0.0f));

            casa2.Add(test);
            casa2.Add(test2);
            //casa2.Add(frontWall);
            //casa2.Add(rearWall);
            //casa2.Add(leftWall);
            //casa2.Add(rightWall);
            //Ver Partes y Puntos
            //casa.Imprimir();

        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

            casa.Dibujar();
            //casa2.Dibujar();

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
