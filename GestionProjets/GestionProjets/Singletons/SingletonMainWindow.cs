using GestionProjets.Objets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProjets.Singletons
{
    internal class SingletonMainWindow
    {
        static SingletonMainWindow instance = null;
        MainWindow mainWindow;

        public SingletonMainWindow(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
        }


        public static SingletonMainWindow getInstance(MainWindow _mainWindow)
        {
            if (instance == null)
                instance = new SingletonMainWindow(_mainWindow);

            return instance;
        }

        public static SingletonMainWindow getInstance()
        {
            return instance;
        }

        public MainWindow MainWindow { get => mainWindow;}
    }
}
