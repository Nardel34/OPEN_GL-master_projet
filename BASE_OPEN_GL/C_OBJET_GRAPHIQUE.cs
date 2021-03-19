using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace BASE_OPEN_GL
{
    enum POSITION { Est_A_Gauche, Est_Au_Centre, Est_A_Droite, Est_En_Haut, Est_En_Bas };

    abstract class C_OBJET_GRAPHIQUE
    {

        const int Limite_Bord_Gauche = -10;
        const int Limite_Bord_Droit = 10;
        const int Limite_Bord_Haut = -10;
        const int Limite_Bord_Bas = 10;

        public int Nature = 0;

        public double Position_objet_X { get; set; }
        public double Position_objet_Y { get; set; }
        public double Delta_X { get; set; }
        public double Delta_Y { get; set; }
        public double Taille { get; set; }
        public POSITION Situation { get; set; }


        public C_OBJET_GRAPHIQUE()
        {
            // trajectoire aleatoire + apparition
            Random Generateur = new Random();

            int valeur_random_direction_X = Generateur.Next(-2000, 2000);
            float resultat_X = (float)(valeur_random_direction_X * 0.00001);

            int valeur_random_direction_Y = Generateur.Next(-2000, 2000);
            float resultat_Y = (float)(valeur_random_direction_Y * 0.00001);

            int valeur_random_X = Generateur.Next(-5, 6);
            int valeur_random_Y = Generateur.Next(-5, 6);
            Position_objet_X = valeur_random_X;
            Position_objet_Y = valeur_random_Y;
            Delta_X = 0.2f;
            Delta_Y = 0.1f;
        }

        public void Deplace_toi()
        {
            Position_objet_X += Delta_X;
            Position_objet_Y += Delta_Y;

            Situation = POSITION.Est_Au_Centre;

            if (Position_objet_X > Limite_Bord_Droit) Situation = POSITION.Est_A_Droite;
            if (Position_objet_X < Limite_Bord_Gauche) Situation = POSITION.Est_A_Gauche;

            if (Position_objet_Y < Limite_Bord_Haut) Situation = POSITION.Est_En_Haut;
            if (Position_objet_Y > Limite_Bord_Bas) Situation = POSITION.Est_En_Bas;
        }

        public void Rebondi_Horizontalement()
        {
            Delta_X = -Delta_X;
        }
        public void rebondi_Verticalement()
        {
            Delta_Y = -Delta_Y;
        }
        public byte[] Serialise_toi()
        {
            MemoryStream Flux_Memoire = new MemoryStream();
            BinaryWriter Le_Serialisateur = new BinaryWriter(Flux_Memoire);
            Le_Serialisateur.Write(Nature);
            Le_Serialisateur.Write(Position_objet_X);
            Le_Serialisateur.Write(Position_objet_Y);
            Le_Serialisateur.Write(Delta_X);
            Le_Serialisateur.Write(Delta_Y);


            return Flux_Memoire.ToArray();
        }
        public void Deserialise_toi(byte[] P_Data)
        {
            MemoryStream Flux_Memoire = new MemoryStream(P_Data);
            BinaryReader Le_Serialiseur = new BinaryReader(Flux_Memoire);

            Nature = Le_Serialiseur.ReadInt32();
            Position_objet_X = Le_Serialiseur.ReadDouble() + Limite_Bord_Gauche - 2;
            Position_objet_Y = Le_Serialiseur.ReadDouble();
            Delta_X = Le_Serialiseur.ReadDouble();
            Delta_Y = Le_Serialiseur.ReadDouble();

        }
        public void affiche_toi()
        {
            dessine_toi();
        }
        abstract protected void dessine_toi();
    }
}
