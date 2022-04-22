using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Parte {
        private List<Punto> vertices;
        private string _name;

        Shader shader;

        string name{
            get { return _name; }
            set { _name = value; }        
        }
        public Parte(Shader shader, string name) {
            this.shader = shader;
            this.name = name;
            vertices = new List<Punto>();
        }

        public Parte(Shader shader, string name, List<Punto> vertices) {
            this.shader = shader;
            this.name = name;
            this.vertices = vertices;
        }

        public Parte(Parte p) {
            shader = p.shader;
            name = p.name;
            vertices = new List<Punto>();
            vertices = p.vertices;
        }

        public string getName() {
            return name;
        }

        public void setName(string name) {
            this.name = name;
        }
        public void Add(Punto punto) {
            vertices.Add(punto);
        }

        public void Delete(Punto punto) {
            vertices.Remove(punto);
        }

        public void DeleteAt(int index) {
            vertices.RemoveAt(index);
        }

        public float[] CopyToArray() {
            float[] result = new float[vertices.Count * 3];
            int pos = 0;
            foreach(Punto k in vertices) {
                result[pos] = k.x;
                pos++;
                result[pos] = k.y;
                pos++;
                result[pos] = k.z;
                pos++;
            }
            return result;
        }
        public void Dibujar() {
            float[] array = CopyToArray();
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }
        public void ImprimirLista() {
            Console.WriteLine("Nombre: " + name);
            
        }

    }
}
