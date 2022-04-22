using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpenTKCasa3D
{
    public class Casa
    {
        private float[] vertices = {
            -0.15f, -0.20f,  0.15f, //punto 0
            -0.15f, -0.20f, -0.15f, //punto 1
             0.15F, -0.20f, -0.15f, //punto 2
             0.15f, -0.20f,  0.15f, //punto 3

            -0.15f,  0.10f,  0.15f, //punto 4
            -0.15f,  0.10f, -0.15f, //punto 5
             0.15F,  0.10f, -0.15f, //punto 6
             0.15f,  0.10f,  0.15f, //punto 7

            -0.15f,  0.20f,  0.0f,  //punto 8
             0.15f,  0.20f,  0.0f,  //punto 9

            -0.1f, -0.12f,  0.15f, //punto 10
            -0.1f,  0.02f,  0.15f, //punto 11
             0.1f,  0.02f,  0.15f, //punto 12
             0.1f, -0.12f,  0.15f, //punto 13

            -0.15f, -0.20f,  0.05f, //punto 14
            -0.15f, -0.20f, -0.05f, //punto 15
            -0.15f,   0.0f, -0.05f, //punto 16
            -0.15f,  0.0f,  0.05f, //punto 17
        };

        Shader shader;
        private Vector3 origen;

        public Casa(Shader shader, Vector3 origen) {
            this.shader = shader;
            this.origen = origen;
        }

        void dibujarMurosLaterales() {
            uint[] index = { 0, 4, 7, 0, 7, 3, 1, 5, 6, 1, 6, 2 };
            shader.SetVector3("objectColor", new Vector3(0.2f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarMurosFrontales() {
            uint[] index = { 1, 5, 4, 1, 4, 0, 2, 6, 7, 2, 7, 3 };
            shader.SetVector3("objectColor", new Vector3(0.3f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarBase() {
            uint[] index = { 0, 1, 2, 0, 2, 3};
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarTecho() {
            uint[] index = { 4, 8, 9, 4, 9, 7, 5, 8, 6, 8, 9, 6 };
            shader.SetVector3("objectColor", new Vector3(1.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        void dibujarPuertaVentana() {
            uint[] index = { 10, 11, 12, 10, 12, 13, 14, 15, 16, 14, 16, 17 };
            shader.SetVector3("objectColor", new Vector3(1.0f, 1.0f, 1.0f));
            GL.BufferData(BufferTarget.ElementArrayBuffer, index.Length * sizeof(uint), index, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, index.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void dibujar() {
            shader.Use();

            var move = Matrix4.Identity;
            move = move * Matrix4.CreateTranslation(origen.X, origen.Y, origen.Z);

            shader.SetMatrix4("origen", move);

            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            dibujarPuertaVentana();
            dibujarBase();
            dibujarMurosLaterales();
            dibujarMurosFrontales();
            dibujarTecho();
        }
    }
}
