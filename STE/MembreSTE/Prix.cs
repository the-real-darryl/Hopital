using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembreSTE
{
    public class Prix
    {
        private string IDPrix;
        private string Description;
        private double Valeur;
        private double DonMinimum;
        static private int Qnte_Originale;
        private int Qnte_Disponible;
        private string IDCommanditaire;
        private static string all_file;

        public string IDPrix1
        {
            get
            {
                return IDPrix;
            }

            set
            {
                IDPrix = value;
            }
        }

        public string Description1
        {
            get
            {
                return Description;
            }

            set
            {
                Description = value;
            }
        }

        public double Valeur1
        {
            get
            {
                return Valeur;
            }

            set
            {
                Valeur = value;
            }
        }

        public double DonMinimum1
        {
            get
            {
                return DonMinimum;
            }

            set
            {
                DonMinimum = value;
            }
        }

        public int Qnte_Originale1
        {
            get
            {
                return Qnte_Originale;
            }

            set
            {
                Qnte_Originale = value;
            }
        }

        public int Qnte_Disponible1
        {
            get
            {
                return Qnte_Disponible;
            }

            set
            {
                Qnte_Disponible = value;
            }
        }

        public string IDCommanditaire1
        {
            get
            {
                return IDCommanditaire;
            }

            set
            {
                IDCommanditaire = value;
            }
        }

        public static string All_file { get => all_file; set => all_file = value; }

        public Prix(string iDPrix, string description, double valeur, double donMinimum, int qnte_Originale, string iDCommanditaire)
        {
            IDPrix = iDPrix;
            Description = description;
            Valeur = valeur;
            DonMinimum = donMinimum;
            Qnte_Originale = qnte_Originale;
            Qnte_Disponible = qnte_Originale;
            IDCommanditaire = iDCommanditaire;
        }

        public Prix(string iDPrix, string description, double valeur, double donMinimum, int qnte_Originale,int qnte_Disponible, string iDCommanditaire)
        {
            IDPrix = iDPrix;
            Description = description;
            Valeur = valeur;
            DonMinimum = donMinimum;
            Qnte_Originale = qnte_Originale;
            Qnte_Disponible = qnte_Disponible;
            IDCommanditaire = iDCommanditaire;
        }


        public Prix(string[] rawInput)
        {
            IDPrix = rawInput[rawInput.Length - 7].Trim();
            Description = rawInput[rawInput.Length - 6].Trim();
            double.TryParse(rawInput[rawInput.Length - 5], out Valeur);
            double.TryParse(rawInput[rawInput.Length - 4], out DonMinimum);
            int.TryParse(rawInput[rawInput.Length - 3], out Qnte_Originale);
            int.TryParse(rawInput[rawInput.Length - 2], out Qnte_Disponible);
            IDCommanditaire = rawInput[rawInput.Length - 1].Trim();
        }

        public override string ToString()
        {
            return "ID Prix: " + IDPrix1 + ", Description: " + Description1 + ", Valeur: " + Valeur1 + ", Don minmum: " +  DonMinimum1 + 
                ",\n Quantite originale: " + Qnte_Originale1 + ", Quantie disponible: " + Qnte_Disponible1 + ", ID Commanditaire: " + IDCommanditaire1 + "\n";
        }

        public string ToFile()
        {
            string ToFile_ = IDPrix + ", " + Description + ", " + Valeur + ", " + DonMinimum +
                ", " + Qnte_Originale + ", " + Qnte_Disponible + ", " + IDCommanditaire;

            return ToFile_;
        }
        public bool deduire(int a)
        {
            if((Qnte_Disponible1-a )>= 0)
            {
                Qnte_Disponible1 -= a;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
