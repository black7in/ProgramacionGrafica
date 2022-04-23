using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Objeto
    {
        private string _name;
        private List<Parte> lisParte;
        private Punto origen;

        string name {
            get { return _name; }
            set { _name = value; }
        }

        public Objeto(string name, Punto origen) {
            this.name = name;
            this.origen = origen;
            lisParte = new List<Parte>();
        }

        public Objeto(string name, Punto origen, List<Parte> lisParte) {
            this.name = name;
            this.origen = origen;
            this.lisParte = new List<Parte>();
            foreach (Parte k in lisParte) {
                this.lisParte.Add(k);
            }

        }

        public Objeto(Objeto objeto) {
            name = objeto.name;
            lisParte = objeto.lisParte;
        }

        public void Add(Parte parte) {
            lisParte.Add(parte);
        }

        public void Delete() { 
        
        }

        public void Dibujar() {
            foreach (Parte k in lisParte) {
                k.MoveTo(origen);
                k.Dibujar();
            }
        }

        public void Imprimir() {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Objeto Nombre: " + name);
            Console.WriteLine("Lista de Partes: ");
            foreach (Parte k in lisParte) {
                k.ImprimirLista();
            }
            Console.WriteLine("----------------------------------");
        }
    }
}
