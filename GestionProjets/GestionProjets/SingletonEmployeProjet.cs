using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets
{
    internal class SingletonEmployeProjet
    {
        ObservableCollection<EmployeProjet> liste;
        static SingletonEmployeProjet instance = null;


        public SingletonEmployeProjet()
        {
            liste = new ObservableCollection<EmployeProjet>();
        }


        public static SingletonEmployeProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonEmployeProjet();

            return instance;
        }


        public ObservableCollection<EmployeProjet> GetEmployeProjetListe()
        {
            return liste;
        }

        public EmployeProjet GetEmployeProjet(int position)
        {
            return liste[position];
        }

        public void ajouter(EmployeProjet employeProjet)
        {
            liste.Add(employeProjet);
        }


        public void modifier(int position, EmployeProjet employeProjet)
        {
            liste[position] = employeProjet;
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
