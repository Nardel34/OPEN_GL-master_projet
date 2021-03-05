using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
    class C_OBJET_GRAPHIQUE
    {
        public float Position_objet_X { get; set; }
        public float Position_objet_Y { get; set; }
        public float Delta_X { get; set; }
        public float Delta_Y { get; set; }
        public float Taille { get; set; }

        public C_OBJET_GRAPHIQUE()
        {
            // trajectoire aleatoire + apparition
            Random Generateur = new Random();

            int valeur_random_direction_X = Generateur.Next(-2000, 2000);
            float resultat_X = (float)(valeur_random_direction_X * 0.00001);

            int valeur_random_direction_Y = Generateur.Next(-2000, 2000);
            float resultat_Y = (float)(valeur_random_direction_Y * 0.00001);

            int valeur_random_X = Generateur.Next(-10, 11);
            int valeur_random_Y = Generateur.Next(-10, 11);
            Position_objet_X = valeur_random_X;
            Position_objet_Y = valeur_random_Y;
            Delta_X = resultat_X;
            Delta_Y = resultat_Y;
        }
        public void rebondie_bord()
        {
            Position_objet_X = Position_objet_X + Delta_X;
            if (Position_objet_X > 14.5f || Position_objet_X < -14.5f)
            {
                Delta_X = -Delta_X;
            }

            Position_objet_Y = Position_objet_Y + Delta_Y;
            if (Position_objet_Y > 10.7f || Position_objet_Y < -11)
            {
                Delta_Y = -Delta_Y;
            }  
        }
        public void affiche_toi()
        {
            dessine_toi();
        }
        virtual protected void dessine_toi()
        {
            Console.WriteLine($"{Position_objet_X}; {Position_objet_Y}");
        }
    }
}
