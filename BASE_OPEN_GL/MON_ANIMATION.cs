using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{	
	partial class Program
	{
		static List<C_OBJET_GRAPHIQUE> Liste_objet = new List<C_OBJET_GRAPHIQUE>();
		static int compteur = 0;

		static float Position_curseur_X;
		static float Position_curseur_Y;

		static string Le_Message;

		static float[] Rouge = new float[4] { 0.8f, 0.2f, 0.2f, 1 };
		static float[] Bleu = new float[4] { 0.2f, 0.2f, 0.8f, 1 };


		static void Initialisation_Animation()
		{

			Le_Message = "voici un texte";
		}

		static void Afficher_Ma_Scene()
		{

			//.... DEBUT DE NE PAS TOUCHER
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT | Gl.GL_STENCIL_BUFFER_BIT);   // Effacer les buffer d'affichage, de profondeur et de masque
			Gl.glMatrixMode(Gl.GL_MODELVIEW);  // choisir la matrice de vue
			Gl.glLoadIdentity(); // initialiser la matrice vue en matrice identité Le repere est donc en 0,0,0
			Glu.gluLookAt(0.0, 0.0, 20.0, // La caméra est à  0,0,20 (x y z)
									0.0, 0.0, 0.0,   // regarde 0,0,0 (le centre)
									0.0, 1.0, 0.0);  // vecteur orientation  (vers le haut)

			//..... FIN DE NE PAS TOUCHER

            foreach (var item in Liste_objet)
            {
				item.affiche_toi();
			}

			Gl.glPushMatrix();
			Gl.glTranslatef(Position_curseur_X, Position_curseur_Y, 0);
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, Bleu);
			Glut.glutSolidSphere(1, 20, 20);
			Gl.glPopMatrix();


			Gl.glColor3f(0.9f, 0.5f, 0.9f); // choix de la couleur 90% de rouge 50% de vert 90% bleu
					
			OPENGL_Affiche_Chaine(-15, 10, Le_Message); // on affiche un texte en -10,-100

			Glut.glutSwapBuffers();
		}

		static void Animation_Scene()
		{
            //.......................Rebond de la balle sur les bord.......................

            foreach (var item in Liste_objet)
            {
				item.rebondie_bord();
			}


            //.......................Compteur.......................
            Random Generateur = new Random();
            int valeur_random_X = Generateur.Next(-10, 10);
			int valeur_random_Y = Generateur.Next(-10, 10);
			foreach (var item in Liste_objet)
            {
				if ((double)item.Position_objet_X > (double)Position_curseur_X - 0.3 && (double)item.Position_objet_X < (double)Position_curseur_X + 0.3 && ((double)item.Position_objet_Y > (double)Position_curseur_Y - 0.3 && (double)item.Position_objet_Y < (double)Position_curseur_Y + 0.3))
				{
					compteur++;
					item.Position_objet_X = valeur_random_X; item.Position_objet_Y = valeur_random_Y;
				}
			}
            Le_Message = $"Score : {compteur}";

            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}
		
		static void Gestion_Touches_Speciales(int P_Touche, int P_X, int P_Y)
		{
			Console.WriteLine($"Touche Spéciale : {P_Touche}. La souris est en {P_X} {P_Y}");


            

            Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}

		static void Gestion_Clavier(byte P_Touche, int P_X, int P_Y)
		{
			Console.WriteLine($"Touche Normale : {P_Touche}. La souris est en {P_X} {P_Y}");

			if (P_Touche == 27) // Echap
			{
				Glut.glutLeaveMainLoop();
			}

			if (P_Touche == 97) // A
			{
				C_OBJET_GRAPHIQUE objet1 = new C_CARRE();

				Liste_objet.Add(objet1);
			}

			if (P_Touche == 122) // Z
			{
				C_OBJET_GRAPHIQUE objet2 = new C_CERCLE();

				Liste_objet.Add(objet2);
			}

			Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )

		}


		static void Gestion_Bouton_Souris(int P_Bouton, int P_Etat, int P_X, int P_Y)
		{
			Console.WriteLine($"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}");
			//Le_Message = $"Bouton Souris : {P_Bouton} est {P_Etat}. La souris est en {P_X} {P_Y}";


			// Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}


		static void Gestion_Molette(int P_Molette, int P_Sens, int P_X, int P_Y)
		{
				Console.WriteLine($"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}");
			//Le_Message = $"Molette Souris : {P_Molette} tourne dans le sens {P_Sens}. La souris est en {P_X} {P_Y}";

			// Glut.glutPostRedisplay(); // demander d'afficher une Frame (cela invoquera Afficher_Ma_Scene )
		}

		static void Gestion_Souris_Libre(int P_X, int P_Y)
		{

			//.......................Calcul des coordonné du curseur pour la raquette.......................
			float x = ((P_X / 800.0f) * 2.0f - 1.0f);
			float y = -((P_Y / 600.0f) * 2.0f - 1.0f);
			Position_curseur_X = x * 15.0f;
			Position_curseur_Y = y * 15.0f;

			Glut.glutPostRedisplay();
		}

		static void Gestion_Souris_Clique(int P_X, int P_Y)
		{
			Le_Message=$"Souris cliqué en {P_X} {P_Y}";

			 Glut.glutPostRedisplay();
		}


	}
}
