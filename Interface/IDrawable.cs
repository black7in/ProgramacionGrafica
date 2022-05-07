using System;
using System.Collections.Generic;
using System.Text;

namespace OpentkProyect
{
    interface IDrawable {
        void Dibujar();
        void Rotar(float grado);
        void Escalar( float width_x, float height_y);
        void Trasladar(float position_x, float position_y);
    }
}
