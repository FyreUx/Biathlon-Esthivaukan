using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace Biathlon_Esthivaukan.Helpers
{
    public static class LocalizationHelper
    {
        public static void SetLocale(string cultureCode)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // Reloader la page principale ou utiliser un mécanisme pour rafraîchir les textes dans l'interface
            App.Current.MainPage = new AppShell(); // Exemple de rechargement de la page principale (si utilisé)
        }
    }
}