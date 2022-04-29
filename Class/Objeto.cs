using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Objeto
    {
        private string _name;
        private Dictionary<string, Parte> _lisParte;
        private Punto _centro;
        public string name {
            get { return _name; }
            set { _name = value; }
        }
        public Punto centro {
            get { return _centro; }
            set { _centro = value; }
        }
        public Dictionary<string, Parte> lisParte {
            get { return _lisParte; }
            set { _lisParte = value; }
        }

        public Objeto() {
            name = "";
            centro = new Punto(0.0f, 0.0f, 0.0f);
            lisParte = new Dictionary<string, Parte>();
        }
        public Objeto(string name, Punto centro) {
            this.name = name;
            this.centro = centro;
            lisParte = new Dictionary<string, Parte>();
        }

        public Objeto(string name, Punto centro, Dictionary<string, Parte> lisParte) {
            this.name = name;
            this.centro = centro;
            this.lisParte = new Dictionary<string, Parte>();
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                this.lisParte.Add(k.Key, k.Value);
            }
        }

        public Objeto(Objeto objeto) {
            name = objeto.name;
            centro = objeto.centro;
            lisParte = objeto.lisParte;
        }
        public void Add(Parte parte) {
            lisParte.Add(parte.getName(), parte);
        }

        public void Delete(string key) {
            lisParte.Remove(key);
        }

        public void setCentro(Punto centro) {
            this.centro = centro;
        }

        public void setName(string name) {
            this.name = name;
        }
        public void Dibujar(Shader shader) {
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                k.Value.Dibujar(shader, centro);
            }
        }
    }
}
