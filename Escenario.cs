using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Escenario {
        private string _name;
        private Dictionary<string, Objeto> listObjeto;


        public string name {
            set { _name = value; }
            get { return _name; }
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
    }
}
