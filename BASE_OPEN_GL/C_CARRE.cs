using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
    class C_CARRE : C_OBJET_GRAPHIQUE
    {
        public C_CARRE()
        {
            Nature = 1;
        }
        protected override void dessine_toi()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef((float)Position_objet_X, (float)Position_objet_Y, 0);
            Glut.glutSolidCube(1.5f);
            Gl.glPopMatrix();
        }
    }
}
