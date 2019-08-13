using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiClientChatLast.Extensions
{
    public class AboutCultures
    {
        public static List<string> GetCountries()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            List<string> countries = new List<string>();

            foreach (var culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.Name);

                if (!(countries.Contains(region.EnglishName)))
                {
                    countries.Add(region.EnglishName);
                }
            }

            return countries;
        }
    }
}
