using System.Collections.Generic;

namespace OpentkProyect
{
    public class Escene: IDrawable {
        private string _name;
        private Dictionary<string, Objeto> _listObjeto;

        public string name {
            set { _name = value; }
            get { return _name; }
        }

        public Dictionary<string, Objeto> listObjeto {
            get { return _listObjeto; }
            set { _listObjeto = value; }
        }

        public Escene() {
            name = "Test";
            listObjeto = new Dictionary<string, Objeto>();
        }
        public Escene(string name) {
            this.name = name;
            listObjeto = new Dictionary<string, Objeto>();
        }

        public void Add(Objeto objeto) {
            listObjeto.Add(objeto.name, objeto);   
        }

        public void Add(string name, Objeto objeto) {
            listObjeto.Add(name, objeto);
        }

        public void Delete(string key) {
            listObjeto.Remove(key);
        }

        public void Dibujar() {
            foreach (KeyValuePair<string, Objeto> k in listObjeto) {
                k.Value.Dibujar();
            }
        }

        public void Rotar(float angulo, float x, float y, float z) {
            foreach (KeyValuePair<string, Objeto> k in listObjeto){
                k.Value.Rotar(angulo, x, y, z);
            }
        }

        public void Trasladar(float position_x, float position_y, float position_z) {
            foreach(KeyValuePair<string, Objeto> k in listObjeto) {
                k.Value.Trasladar(position_x, position_y, position_z);
            }
        }

        public void Escalar( float x, float y, float z ) {
            foreach (KeyValuePair<string, Objeto> k in listObjeto) {
                k.Value.Escalar(x, y, z);
            }
        }
    }
}
