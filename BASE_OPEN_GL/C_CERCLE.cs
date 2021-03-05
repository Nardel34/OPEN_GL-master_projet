using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
    class C_CERCLE : C_OBJET_GRAPHIQUE
    {
        protected override void dessine_toi()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(Position_objet_X, Position_objet_Y, 0);
            Glut.glutSolidSphere(1, 20, 20);
            Gl.glPopMatrix();

        }
    }
}
