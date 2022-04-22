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
        private int elementBufferObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

        Objeto casa;
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

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(50.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(40.0f)); //* Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-5.5f));

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            casa = new Objeto("Casa");

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

            Parte techo1 = new Parte(shader, "Techo 1");
            techo1.Add(new Punto(-0.15f, 0.10f, -0.15f));
            techo1.Add(new Punto(-0.15f, 0.20f, 0.0f));
            techo1.Add(new Punto(0.15f, 0.20f, 0.0f));
            techo1.Add(new Punto(0.15F, 0.10f, -0.15f));

            Parte techo2 = new Parte(shader, "Techo 2");
            techo2.Add(new Punto(-0.15f, 0.10f, 0.15f));
            techo2.Add(new Punto(-0.15f, 0.20f, 0.0f));
            techo2.Add(new Punto(0.15f, 0.20f, 0.0f));
            techo2.Add(new Punto(0.15f, 0.10f, 0.15f));

            Parte muroFrontal = new Parte(shader, "Muro Frontal");
            muroFrontal.Add(new Punto(-0.15f, -0.20f, 0.15f));
            muroFrontal.Add(new Punto(-0.15f, -0.20f, -0.15f));
            muroFrontal.Add(new Punto(-0.15f, 0.10f, -0.15f));
            muroFrontal.Add(new Punto(-0.15f, 0.10f, 0.15f));

            Parte muroTrasero = new Parte(shader, "Muro Trasero");
            muroTrasero.Add(new Punto(0.15f, -0.20f, 0.15f));
            muroTrasero.Add(new Punto(0.15f, 0.10f, 0.15f));
            muroTrasero.Add(new Punto(0.15F,  0.10f, -0.15f));
            muroTrasero.Add(new Punto(0.15F, -0.20f, -0.15f));

            Parte muroLateralDerecho = new Parte(shader, "Muro Lateral");
            muroLateralDerecho.Add(new Punto(-0.15f, -0.20f, 0.15f));
            muroLateralDerecho.Add(new Punto(-0.15f, 0.10f, 0.15f));
            muroLateralDerecho.Add(new Punto(0.15f, 0.10f, 0.15f));
            muroLateralDerecho.Add(new Punto(0.15f, -0.20f, 0.15f));

            Parte muroLateralIzquiero = new Parte(shader, "Muro Lateral Izquierdo");
            muroLateralIzquiero.Add(new Punto(-0.15f, -0.20f, -0.15f));
            muroLateralIzquiero.Add(new Punto(-0.15f, 0.10f, -0.15f));
            muroLateralIzquiero.Add(new Punto(0.15F, 0.10f, -0.15f));
            muroLateralIzquiero.Add(new Punto(0.15F, -0.20f, -0.15f));

            Parte puerta = new Parte(shader, "Puerta");
            puerta.Add(new Punto(-0.15f, -0.20f, 0.08f));
            puerta.Add(new Punto(-0.15f,  0.05f, 0.08f));
            puerta.Add(new Punto(-0.15f, 0.05f, -0.08f));
            puerta.Add(new Punto(-0.15f, -0.20f, -0.08f));

            Parte ventanaLateralDerecho = new Parte(shader, "Ventana Lateral Derecho");
            ventanaLateralDerecho.Add(new Punto(-0.10f, -0.15f, 0.15f));
            ventanaLateralDerecho.Add(new Punto(-0.10f,  0.05f, 0.15f));
            ventanaLateralDerecho.Add(new Punto( 0.10f,  0.05f, 0.15f));
            ventanaLateralDerecho.Add(new Punto( 0.10f, -0.15f, 0.15f));

            casa.Add(techo1);
            casa.Add(techo2);
            casa.Add(muroFrontal);
            casa.Add(muroTrasero);
            casa.Add(muroLateralDerecho);
            casa.Add(muroLateralIzquiero);
            casa.Add(puerta);
            casa.Add(ventanaLateralDerecho);
        }

        protected override void OnRenderFrame(FrameEventArgs args) {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

            casa.Dibujar();

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
