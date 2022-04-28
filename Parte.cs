using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Parte {
        private Dictionary<int, Punto> vertices;
        private string _name;
        private Punto _origen;
        private float grado;
        Shader shader;
        string name {
            get { return _name; }
            set { _name = value; }
        }

        Punto origen {
            get { return _origen; }
            set { _origen = value; }
        }
        public Parte(Shader shader, string name, Punto origen, float grado) {
            this.shader = shader;
            this.name = name;
            this.origen = origen;
            this.grado = grado;
            vertices = new Dictionary<int, Punto>();
        }

        public Parte(Shader shader, string name, Punto origen, Dictionary<int, Punto> vertices) {
            this.shader = shader;
            this.name = name;
            this.origen = origen;
            this.vertices = new Dictionary<int, Punto>();
            foreach (KeyValuePair<int, Punto> k in vertices) {
                this.vertices.Add(k.Key, k.Value);
            }
        }

        public Parte(Parte p) {
            shader = p.shader;
            name = p.name;
            origen = p.origen;
            vertices = new Dictionary<int, Punto>();
            vertices = p.vertices;
        }

        public string getName() {
            return name;
        }

        public void setName(string name) {
            this.name = name;
        }
        public void Add(int key, Punto punto) {
            vertices.Add(key, punto);
        }

        public void Delete(int key) {
            vertices.Remove(key);
        }

        public Punto getOrigen() {
            return origen;
        }

        public void setOrigen(Punto origen) {
            this.origen = origen;
        }
        public float[] CopyToArray() {
            float[] result = new float[vertices.Count * 3];
            int pos = 0;
            foreach(KeyValuePair<int, Punto> k in vertices) {
                result[pos] = k.Value.x;
                pos++;
                result[pos] = k.Value.y;
                pos++;
                result[pos] = k.Value.z;
                pos++;
            }
            return result;
        }
        public void Dibujar( Punto p) {
            var origenObjeto = Matrix4.Identity;
            origenObjeto = origenObjeto * Matrix4.CreateTranslation(p.x, p.y, p.z);
            shader.SetMatrix4("origenObjeto", origenObjeto);

            var origenParte = Matrix4.Identity;
            origenParte = origenParte * Matrix4.CreateTranslation(origen.x, origen.y, origen.z);
            shader.SetMatrix4("origenParte", origenParte);

            var model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(grado));
            shader.SetMatrix4("model", model);

            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            float[] array = CopyToArray();
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }
        public void ImprimirLista() {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Nombre: " + name);
            foreach ( KeyValuePair<int, Punto> k in vertices) {
                Console.WriteLine("Punto " + k.Key + ": " + k.Value.toString()); ;
            }
            Console.WriteLine("----------------------------------");
        }

        public void ImprimirOrigen() {
            Console.WriteLine("X: " + origen.x + " Y: " + origen.y + " Z: " + origen.z);
        }

    }
}
