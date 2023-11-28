using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class SingletonEmploye
    {

        ObservableCollection<Employe> liste;
        static SingletonEmploye instance = null;
        public SingletonEmploye()
        {
            liste = new ObservableCollection<Employe>();
        }


        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();

            return instance;
        }


        public ObservableCollection<Employe> getEmployeListe()
        {
            return liste;
        }

        public Employe GetEmploye(int position)
        {
            if (position >= 0)
            {
                return liste[position];
            }
            return liste[0];
        }

        public void ajouter(Employe employe)
        {
            liste.Add(employe);
        }

        public void modifier(int position, Employe employe)
        {
            liste[position] = employe;
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
