using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Objeto
    {
        private string _name;
        private List<Parte> lisParte;

        string name {
            get { return _name; }
            set { _name = value; }
        }

        public Objeto(string name) {
            this.name = name;
            lisParte = new List<Parte>();
        }

        public Objeto(string name, List<Parte> lisParte) {
            this.name = name;
            this.lisParte = lisParte;
        }

        public Objeto(Objeto objeto) {
            name = objeto.name;
            lisParte = objeto.lisParte;
        }

        public void Add(Parte parte) {
            lisParte.Add(parte);
        }

        public void Dibujar() {
            foreach (Parte k in lisParte) {
                k.Dibujar();
            }
        }
    }
}
