using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class SingletonClient
    {
        ObservableCollection<Client> liste;
        static SingletonClient instance = null;


        public SingletonClient()
        {
            liste = new ObservableCollection<Client>();
        }


        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();

            return instance;
        }


        public ObservableCollection<Client> getClientListe()
        {
            return liste;
        }

        public Client GetClient(int position)
        {
            return liste[position];
        }

        public void ajouter(Client client)
        {
            liste.Add(client);
        }


        public void modifier(int position, Client client)
        {
            liste[position] = client;
        }
        public void supprimer(int position)
        {
            liste.RemoveAt(position);
        }
    }
}
