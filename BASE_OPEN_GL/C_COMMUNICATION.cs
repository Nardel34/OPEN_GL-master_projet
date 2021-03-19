using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BASE_OPEN_GL
{
    class C_COMMUNICATION
    {
        
        const int Port = 999;
        const string Adresse_IP_Adress = "10.5.102.202";

        const int TimeOut_Reception = 10000;

        byte[] Buffer_reception = new byte[1000];

        Socket Le_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        IPEndPoint Adresse_Process_distant = new IPEndPoint(IPAddress.Parse(Adresse_IP_Adress), Port);

        IPEndPoint Adresse_Process_Local = new IPEndPoint(IPAddress.Any, Port);
        EndPoint Adresse_Process_Voisin = new IPEndPoint(IPAddress.Any, 0);

        UdpClient Le_Client = new UdpClient();

        public C_COMMUNICATION()
        {
            Le_Socket.Bind(Adresse_Process_Local);
        }
        

        public void emission(C_OBJET_GRAPHIQUE un_objet)
        {
            byte[] Data = un_objet.Serialise_toi();
            Le_Client.Send(Data, Data.Length, Adresse_Process_distant);
        }
        public C_OBJET_GRAPHIQUE reception()
        {
            C_OBJET_GRAPHIQUE nouvel_objet = null;

            bool Message_disponible = Le_Socket.Poll(TimeOut_Reception, SelectMode.SelectRead);

            if (Message_disponible)
            {
                Le_Socket.ReceiveFrom(Buffer_reception, ref Adresse_Process_Voisin);
                MemoryStream Flux_Memoire = new MemoryStream(Buffer_reception);
                BinaryReader Le_Serialisateur = new BinaryReader(Flux_Memoire);

                int Nature = Le_Serialisateur.ReadInt32();

                switch (Nature)
                {
                    case 0: break;
                    case 1: nouvel_objet = new C_CARRE(); break;
                    case 2: nouvel_objet = new C_CERCLE(); break;
                }

                if (nouvel_objet != null)
                {
                    nouvel_objet.Deserialise_toi(Buffer_reception);

                }
            }
            return nouvel_objet;
        }
    }
}
