using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Resources;


namespace Biathlon_Esthivaukan.Helpers
{
    public static class LocalizationHelper
    {
        public static void SetLocale(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            
        }

        public static string GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture.Name;
        }
    }
}