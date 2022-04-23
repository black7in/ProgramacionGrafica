﻿using System;

namespace OpentkProyect
{
    public class Punto {

        private float _x, _y, _z;

        public float x {
            get { return _x; }
            set { _x = value; }
        }

        public float y {
            get { return _y; }
            set { _y = value; }
        }

        public float z {
            get { return _z; }
            set { _z = value; }
        }

        public Punto(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Punto(Punto p) {
            this.x = p.x;
            this.y = p.y;
            this.z = p.z;
        }

        public void Imprimir() {
            Console.WriteLine("Punto[ X: " + x + " Y: " + y + " Z: " + z + " ]");
        }

    }
}
