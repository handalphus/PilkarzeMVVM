using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace PilkarzeMVVM.Model
{
    class Archiwizacja
    {
       
        public static void ZapisPilkarzyDoPliku(string plik, Pilkarz[] pilkarze)
        {
            var ds = new DataContractSerializer(typeof(Pilkarz[]));
            using (Stream s =File.Create("archiwum.xml"))
            {
                ds.WriteObject(s, pilkarze);
               
            }
        }
        public static Pilkarz[] CzytajPilkarzyZPliku(string plik)
        {
            Pilkarz[] pilkarze = null;
            if (File.Exists("archiwum.xml"))
            {
                var ds = new DataContractSerializer(typeof(Pilkarz[]));
                Stream s = File.OpenRead("archiwum.xml");
                using (XmlReader x = XmlReader.Create(s))
                {
                    pilkarze = (Pilkarz[])ds.ReadObject(x, true);
                }

                    

            }
            return pilkarze;
        }

    }
}
