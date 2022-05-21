using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    interface IDrawable {
        void Dibujar();
        void Rotar(float angulo, float x, float y, float z);
        void Escalar(float x, float y, float z);
        void Trasladar(float position_x, float position_y, float position_z);
    }
}
