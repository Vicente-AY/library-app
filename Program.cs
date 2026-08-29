using Data;
using Items;
using library_app.GUI.Login;
using Menus;
using System;
using System.Windows;
using Users;

public class Program{

    [STAThread]
    static void Main(string[] args)
    {
        using (var db = new LibraryContext())
        {
            db.Database.EnsureCreated();
        }
        /*
        MainMenu mm = new MainMenu();
        mm.MainMenuOptions();
        */

        var app = new Application
        {
            ShutdownMode = ShutdownMode.OnLastWindowClose
        };

        MainWindow main = new MainWindow();
        app.Run(main);
    }
}