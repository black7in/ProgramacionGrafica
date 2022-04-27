using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Parte {
        private Dictionary<int, Punto> vertices;
        private string _name;

        Shader shader;
        string name{
            get { return _name; }
            set { _name = value; }        
        }
        public Parte(Shader shader, string name) {
            this.shader = shader;
            this.name = name;
            vertices = new Dictionary<int, Punto>();
        }

        public Parte(Shader shader, string name, Dictionary<int, Punto> vertices) {
            this.shader = shader;
            this.name = name;
            this.vertices = new Dictionary<int, Punto>();
            foreach (KeyValuePair<int, Punto> k in vertices) {
                this.vertices.Add(k.Key, k.Value);
            }
        }

        public Parte(Parte p) {
            shader = p.shader;
            name = p.name;
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

        public void MoveTo(Punto origen) {
            var move = Matrix4.Identity;
            move = move * Matrix4.CreateTranslation(origen.x, origen.y, origen.z);

            shader.SetMatrix4("origen", move);
        }
        public void Dibujar() {
            float[] array = CopyToArray();
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
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

    }
}
