using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace OpentkProyect
{
    public class Parte: IDrawable{

        private Dictionary<string, Punto> _vertices;
        private string _name;
        private Punto _center;

        private Shader shader;
        private Matrix4 projection;
        private Matrix4 trans;
        private Matrix4 model;
        private Matrix4 scale;
        private Matrix4 rotar;

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
            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

            trans = Matrix4.CreateTranslation(0.0f, 0.0f, -1.5f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(50.0f), 1000 / (float)800, 0.1f, 100.0f);
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-30.0f));
            scale = Matrix4.CreateScale(1.0f, 1.0f, 1.0f);
            rotar = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(0.0f));
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

        public Parte(Parte p) {
            shader = p.shader;
            name = p.name;
            center = p.center;
            vertices = new Dictionary<string, Punto>();
            vertices = p.vertices;
        }

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
        public void Dibujar() {
        }
        public void Dibujar( Punto p) {
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("trans", trans);
            shader.SetMatrix4("scale", scale);
            //shader.SetMatrix4("rotar", rotar);

            var origenObjeto = Matrix4.Identity;
            origenObjeto = origenObjeto * Matrix4.CreateTranslation(p.x, p.y, p.z);
            shader.SetMatrix4("origenObjeto", origenObjeto);

            var origenParte = Matrix4.Identity;
            origenParte = origenParte * Matrix4.CreateTranslation(center.x, center.y, center.z);
            shader.SetMatrix4("origenParte", origenParte);

            shader.SetVector3("objectColor", new Vector3(1.0f, 1.0f, 1.0f));
            
            float[] array = CopyToArray();
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }
        

        public void Rotar(float grado){
            rotar = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(grado));
        }

        public void Escalar(float width_x, float height_y) {
            scale = Matrix4.CreateScale(width_x, height_y, 1.0f);
        }

        public void Trasladar(float position_x, float position_y){
            trans = Matrix4.CreateTranslation(position_x, position_y, -1.5f);
        }

        public string toString()
        {
            string result = "";
            result += "----------------------------------\n";
            result += "Nombre: " + name + "\n";
            foreach (KeyValuePair<string, Punto> k in vertices)
            {
                result += "Punto " + k.Key + ": " + k.Value.toString() + "\n";
            }
            result += "----------------------------------\n";

            return result;
        }
    }
}
