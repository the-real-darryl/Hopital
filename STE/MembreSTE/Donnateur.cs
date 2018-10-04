using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembreSTE
{
    public class Donnateur : Personne
    {
        private string IDDonateur;
        private string Adresse;
        private string Telephone;
        private char TypeDeCarte;
        private string NumeroDeCarte;
        private string DateExpiration;

        private static string all_file;
        public static string All_file { get => all_file; set => all_file = value; }

        public string IDDonateur1
        {
            get
            {
                return IDDonateur;
            }

            set
            {
                IDDonateur = value;
            }
        }

        public string Adresse1
        {
            get
            {
                return Adresse;
            }

            set
            {
                Adresse = value;
            }
        }

        public string Telephone1
        {
            get
            {
                return Telephone;
            }

            set
            {
                Telephone = value;
            }
        }

        public char TypeDeCarte1
        {
            get
            {
                return TypeDeCarte;
            }

            set
            {
                TypeDeCarte = value;
            }
        }

        public string NumeroDeCarte1
        {
            get
            {
                return NumeroDeCarte;
            }

            set
            {
                NumeroDeCarte = value;
            }
        }

        public string DateExpiration1
        {
            get
            {
                return DateExpiration;
            }

            set
            {
                DateExpiration = value;
            }
        }

        public Donnateur(string iDDonateur, string prenom, string surnom,string adresse, string telephone, char typeDeCarte, string numeroDeCarte, string dateExpiration):base(prenom, surnom)
        {
            IDDonateur = iDDonateur;
            Adresse = adresse;
            Telephone = telephone;
            TypeDeCarte = typeDeCarte;
            NumeroDeCarte = numeroDeCarte;
            DateExpiration = dateExpiration;
        }

        public Donnateur(string[] rawInput) : base(rawInput)
        {
            IDDonateur = rawInput[rawInput.Length - 8].Trim();
            Adresse = rawInput[rawInput.Length - 7].Trim();
            Telephone = rawInput[rawInput.Length - 6].Trim();
            char.TryParse(rawInput[rawInput.Length - 5].Trim(), out TypeDeCarte);
            NumeroDeCarte = rawInput[rawInput.Length - 4].Trim();
            DateExpiration = rawInput[rawInput.Length - 3].Trim();
        }

        public override string ToString()
        {
            return "ID Donnateur: " + IDDonateur1 + " " + base.ToString() + ", Adresse: " + Adresse1 + ", telephone: " + Telephone1 + 
                        ",\n Carte: " + TypeDeCarte1 + ", Numero: " + NumeroDeCarte1 + ", Date d'expiration: " + DateExpiration1;
        }

        public string ToFile()
        {
            return IDDonateur + ", " + Adresse + ", " + Telephone + ", " + TypeDeCarte.ToString() + ", " + NumeroDeCarte +
                ", " + DateExpiration + ", " + base.ToFile();
        }
    }
}
