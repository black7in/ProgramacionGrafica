using System.Collections.Generic;

namespace OpentkProyect
{
    public class Escenario: IDrawable {
        private string _name;
        private Dictionary<string, Objeto> listObjeto;

        public string name {
            set { _name = value; }
            get { return _name; }
        }

        public Escenario() {
            name = "Test";
            listObjeto = new Dictionary<string, Objeto>();
        }
        public Escenario(string name) {
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

        public void Rotar(float grado) { 
        
        }

        public void Trasladar(float position_x, float position_y) {
            foreach(KeyValuePair<string, Objeto> k in listObjeto) {
                k.Value.Trasladar(position_x, position_y);
            }
        }

        public void Escalar(float width_x, float height_y) {
            foreach (KeyValuePair<string, Objeto> k in listObjeto) {
                k.Value.Escalar(width_x, height_y);
            }
        }
    }
}
