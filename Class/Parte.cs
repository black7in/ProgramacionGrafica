using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Parte {

        private Dictionary<string, Punto> _vertices;
        private string _name;
        private Punto _center;

        public string name {
            get { return _name; }
            set { _name = value; }
        }

        public Punto center {
            get { return _center; }
            set { _center = value; }
        }
        public Dictionary<string, Punto> vertices {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Parte(){
            name = "";
            center = new Punto(0.0f, 0.0f, 0.0f);
            vertices = new Dictionary<string, Punto>();
        }

        public Parte(string name, Punto center) {
            this.name = name;
            this.center = center;
            vertices = new Dictionary<string, Punto>();
        }

        public Parte(string name, Punto center, Dictionary<string, Punto> vertices) {
            this.name = name;
            this.center = center;
            this.vertices = new Dictionary<string, Punto>();
            foreach (KeyValuePair<string, Punto> k in vertices) {
                this.vertices.Add(k.Key, k.Value);
            }
        }

        /*public Parte(Parte p) {
            //shader = p.shader;
            name = p.name;
            origen = p.origen;
            vertices = new Dictionary<string, Punto>();
            vertices = p.vertices;
        }*/

        public string getName() {
            return name;
        }

        public void setName(string name) {
            this.name = name;
        }
        public void Add(string key, Punto punto) {
            vertices.Add(key, punto);
        }

        public void Delete(string key) {
            vertices.Remove(key);
        }

        public Punto getCenter() {
            return center;
        }

        public void setCenter(Punto origen) {
            this.center = origen;
        }
        public float[] CopyToArray() {
            float[] result = new float[vertices.Count * 3];
            int pos = 0;
            foreach(KeyValuePair<string, Punto> k in vertices) {
                result[pos] = k.Value.x;
                pos++;
                result[pos] = k.Value.y;
                pos++;
                result[pos] = k.Value.z;
                pos++;
            }
            return result;
        }
        public void Dibujar( Shader shader, Punto p) {
            var origenObjeto = Matrix4.Identity;
            origenObjeto = origenObjeto * Matrix4.CreateTranslation(p.x, p.y, p.z);
            shader.SetMatrix4("origenObjeto", origenObjeto);

            var origenParte = Matrix4.Identity;
            origenParte = origenParte * Matrix4.CreateTranslation(center.x, center.y, center.z);
            shader.SetMatrix4("origenParte", origenParte);

            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            
            float[] array = CopyToArray();
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }
        public string toString() {
            string result = "";
            result += "----------------------------------\n";
            result += "Nombre: " + name + "\n";
            foreach ( KeyValuePair<string, Punto> k in vertices) {
                result += "Punto " + k.Key + ": " + k.Value.toString() + "\n";
            }
            result += "----------------------------------\n";

            return result;
        }

    }
}
