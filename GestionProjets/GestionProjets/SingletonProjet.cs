using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class SingletonProjet
    {
        ObservableCollection<Projet> liste;
        static SingletonProjet instance = null;


        public SingletonProjet()
        {
            liste = new ObservableCollection<Projet>();
        }


        public static SingletonProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonProjet();

            return instance;
        }


        public ObservableCollection<Projet> getProjetListe()
        {
            return liste;
        }

        public Projet GetProjet(int position)
        {
            return liste[position];
        }

        public void ajouter(Projet projet)
        {
            liste.Add(projet);
        }


        public void modifier(int position, Projet projet)
        {
            liste[position] = projet;
        }
        public void supprimer(int position)
        {
            liste.RemoveAt(position);
        }
        public void refresh()
        {
            liste.Clear();
        }
    }
}
