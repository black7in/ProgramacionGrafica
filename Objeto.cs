using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Objeto
    {
        private string _name;
        private Dictionary<string, Parte> lisParte;
        private Punto origen;
        string name {
            get { return _name; }
            set { _name = value; }
        }

        public Objeto(string name, Punto origen) {
            this.name = name;
            this.origen = origen;
            lisParte = new Dictionary<string, Parte>();
        }

        public Objeto(string name, Punto origen, Dictionary<string, Parte> lisParte) {
            this.name = name;
            this.origen = origen;
            this.lisParte = new Dictionary<string, Parte>();
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                this.lisParte.Add(k.Key, k.Value);
            }
        }

        public Objeto(Objeto objeto) {
            name = objeto.name;
            origen = objeto.origen;
            lisParte = objeto.lisParte;
        }

        public void Add(Parte parte) {
            lisParte.Add(parte.getName(), parte);
        }

        public void Delete(string key) {
            lisParte.Remove(key);
        }

        public void Dibujar() {
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                k.Value.MoveTo(origen);
                k.Value.Dibujar();
            }
        }
        public void Imprimir() {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Objeto Nombre: " + name);
            Console.WriteLine("Lista de Partes: ");
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                k.Value.ImprimirLista();
            }
            Console.WriteLine("----------------------------------");
        }
    }
}
