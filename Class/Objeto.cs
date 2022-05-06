﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    public class Objeto
    {
        private string _name;
        private Dictionary<string, Parte> _listParte;
        private Punto _centro;
        public string name {
            get { return _name; }
            set { _name = value; }
        }
        public Punto centro {
            get { return _centro; }
            set { _centro = value; }
        }
        public Dictionary<string, Parte> listParte {
            get { return _listParte; }
            set { _listParte = value; }
        }

        public Objeto() {
            name = "";
            centro = new Punto(0.0f, 0.0f, 0.0f);
            listParte = new Dictionary<string, Parte>();
        }
        public Objeto(string name, Punto centro) {
            this.name = name;
            this.centro = centro;
            listParte = new Dictionary<string, Parte>();
        }

        public Objeto(string name, Punto centro, Dictionary<string, Parte> lisParte) {
            this.name = name;
            this.centro = centro;
            this.listParte = new Dictionary<string, Parte>();
            foreach (KeyValuePair<string, Parte> k in lisParte) {
                this.listParte.Add(k.Key, k.Value);
            }
        }
        public Objeto(Objeto objeto) {
            name = objeto.name;
            centro = objeto.centro;
            listParte = objeto.listParte;
        }
        public void Add(Parte parte) {
            listParte.Add(parte.getName(), parte);
        }

        public void Delete(string key) {
            listParte.Remove(key);
        }

        public void setCentro(Punto centro) {
            this.centro = centro;
        }
        public void setName(string name) {
            this.name = name;
        }

        public void setShader(Shader shader) {
            foreach (KeyValuePair<string, Parte> k in listParte) {
                k.Value.setShader(shader);
            }
        }
        public void Dibujar() {
            foreach (KeyValuePair<string, Parte> k in listParte) {
                k.Value.Dibujar(centro);
            }
        }
    }
}
