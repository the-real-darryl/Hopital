using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace MembreSTE
{
    public class GestionnaireSTE
    {
        List<Donnateur> donnateurs;
        List<Commandiatire> commanditaires;
        List<Don> dons;
        List<Prix> prix;
        String message = "";

        private readonly string[] NOM_FICHIERS = { "commanditaires.txt", "prix.txt", "donnateurs.txt", "dons.txt" };


        public string getNOM_FICHIERS(int coordonne)
        {
            return NOM_FICHIERS[coordonne];
        }

        public List<Donnateur> Donnateurs
        {
            get
            {
                return donnateurs;
            }

            set
            {
                donnateurs = value;
            }
        }

        public List<Commandiatire> Commanditaires
        {
            get
            {
                return commanditaires;
            }

            set
            {
                commanditaires = value;
            }
        }

        public List<Don> Dons
        {
            get
            {
                return dons;
            }

            set
            {
                dons = value;
            }
        }

        public List<Prix> Prix
        {
            get
            {
                return prix;
            }

            set
            {
                prix = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public GestionnaireSTE()
        {
            donnateurs = new List<Donnateur>();
            commanditaires = new List<Commandiatire>();
            dons = new List<Don>();
            prix = new List<Prix>();

            string ligne;
            string[] donne;
            ArrayList objet_pour_lecture = new ArrayList();
            objet_pour_lecture.Add(commanditaires);
            objet_pour_lecture.Add(prix);
            objet_pour_lecture.Add(donnateurs);
            objet_pour_lecture.Add(dons);

            for (byte lecteur_ = 0; lecteur_ < 4; lecteur_++)
            {
                if (File.Exists(NOM_FICHIERS[lecteur_]))
                {
                    StreamReader lecteur = new StreamReader(NOM_FICHIERS[lecteur_]);

                    switch (lecteur_)
                    {
                        case 0:
                            Commandiatire Commandiatire_pour_verification;
                            
                            while ((ligne = lecteur.ReadLine()) != null)
                            {
                                donne = ligne.Split(',');
                                Commandiatire_pour_verification = new Commandiatire(donne);
                                if (VerifierCommanditaire(Commandiatire_pour_verification))
                                {
                                    ((List<Commandiatire>)objet_pour_lecture[lecteur_]).Add(Commandiatire_pour_verification);
                                    Commandiatire.All_file += Commandiatire_pour_verification.ToFile() + "\r\n";
                                }
                            }
                            break;

                        case 1:
                            Prix Prix_pour_verification;
                           
                            while ((ligne = lecteur.ReadLine()) != null)
                            {
                                donne = ligne.Split(',');
                                Prix_pour_verification = new Prix(donne);
                                if (VerifierPrix(Prix_pour_verification))
                                {
                                    ((List<Prix>)objet_pour_lecture[lecteur_]).Add(Prix_pour_verification);
                                    // Prix. = Prix_pour_verification.ToFile();
                                    MembreSTE.Prix.All_file += Prix_pour_verification.ToFile() + "\r\n"; ;
                                }
                            }
                            break;

                        case 2:
                            Donnateur Donnateur_pour_verification;
                            while ((ligne = lecteur.ReadLine()) != null)
                            {
                                donne = ligne.Split(',');
                                Donnateur_pour_verification = new Donnateur(donne);
                                if (VerifierDonnateur(Donnateur_pour_verification))
                                {
                                    ((List<Donnateur>)objet_pour_lecture[lecteur_]).Add(Donnateur_pour_verification);
                                    Donnateur.All_file += Donnateur_pour_verification.ToFile() + "\r\n"; ;
                                }

                            }
                            break;

                        case 3:
                            Don Don_pour_verification;
                            while ((ligne = lecteur.ReadLine()) != null)
                            {
                                donne = ligne.Split(',');
                                Don_pour_verification = new Don(donne);
                                if (VerifierDon(Don_pour_verification))
                                {
                                    ((List<Don>)objet_pour_lecture[lecteur_]).Add(Don_pour_verification);
                                    Don.All_file += Don_pour_verification.ToFile() + "\r\n"; ;
                                }
                                else
                                {
                                    message += "Don " + Don_pour_verification + "\r\n";
                                }

                            }
                            break;
                    }

                    lecteur.Close();
                }

                donne = null;
            }


        }


        public bool VerifierDonnateur(Donnateur atester)
        {
            bool existedeja = false;
            foreach (Donnateur d in donnateurs)
            {
                if (d.IDDonateur1.Equals(atester.IDDonateur1))
                {
                    existedeja = true;
                }
            }
            if (!existedeja)
            {
                return true;
            }
            return false;

        }

        public bool VerifierCommanditaire(Commandiatire catester)
        {
            bool existdeja = false;
            foreach (Commandiatire c in commanditaires)
            {
                if (c.IDCommanditaire1.Equals(catester.IDCommanditaire1))
                {
                    existdeja = true;
                }
            }
            if (!existdeja)
            {
                return true;
            }
            return false;
        }

        public bool VerifierPrix(Prix patester)
        {
            bool existdeja = false;
            foreach (Prix p in prix)
            {
                if (p.IDPrix1.Equals(patester.IDPrix1))
                {
                    existdeja = true;
                }
            }
            bool commexist = false;
            foreach (Commandiatire c in commanditaires)
            {
                if (patester.IDCommanditaire1.Equals(c.IDCommanditaire1))
                {
                    commexist = true;
                }
            }
            if (!existdeja && commexist)
            {
                return true;
            }
            return false;
        }

        public bool VerifierDon(Don datester)
        {
            bool existdeja = false;
            foreach (Don d in dons)
            {
                if (d.IDDon1.Equals(datester.IDDon1))
                {
                    existdeja = true;
                }
            }
            bool prixexistant = false;
            foreach (Prix p in prix)
            {
                if (datester.IDPrix1.Equals(p.IDPrix1))
                {
                    prixexistant = true;
                }
            }
            bool donnateurexistant = false;
            foreach (Donnateur d in donnateurs)
            {
                if (datester.IDDonateur1.Equals(d.IDDonateur1))
                {
                    donnateurexistant = true;
                }
            }
            if (!existdeja && prixexistant && donnateurexistant)
            {
                // if(AttribuerPrix(datester.MontantDuDon1, datester.IDPrix1))
                // {
                return true;
                // }

            }
            return false;
        }

        public string AfficherDonnateur()
        {
            string ds = "";
            foreach (Donnateur d in Donnateurs)
            {
                ds += d.ToString() + "\n";
            }
            return ds;
        }

        public string AfficherCommanditaires()
        {
            string cs = "";
            foreach (Commandiatire c in Commanditaires)
            {
                cs += c.ToString() + "\n";
            }
            return cs;
        }

        public string AfficherPrix()
        {
            string ps = "";
            foreach (Prix p in Prix)
            {
                ps += p.ToString() + "\n";
            }
            return ps;
        }

        public string AfficherDons()
        {
            string ds = "";
            foreach (Don d in Dons)
            {
                ds += d.ToString() + "\n";
            }
            return ds;
        }

        public bool AttribuerPrix(double a, string idprix)
        {
            int nombreDePrix = 0;
            if (a > 50 && a <= 199)
            {
                nombreDePrix = 1;
            }
            else if (a <= 349)
            {
                nombreDePrix = 2;
            }
            else if (a <= 500)
            {
                nombreDePrix = 3;
            }
            else
            {
                nombreDePrix = 4;
            }

            foreach (Prix p in prix)
            {
                if (p.IDPrix1 == idprix)
                {
                    if (p.deduire(nombreDePrix))
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        /*public void Importer(string nomFichier)
        {
            string ligne;
            string[] donnats;
            if (!File.Exists(nomFichier))
            {
                throw new Exception($" Le fichier { nomFichier} n’existe pas dans mon répertoire.");
            }
            else
            {
                StreamReader lecteur = new StreamReader(nomFichier);
                while ((ligne = lecteur.ReadLine()) != null)
                {
                    donnats = ligne.Split(',');
                    donnateurs.Add(new Donnateur(donnats[0], donnats[1], donnats[2], donnats[3], donnats[4], (donnats[5])[0], donnats[6], donnats[7]));
                }
                lecteur.Close();
            }

        }*/

        public void Exporter(string nom_fichier, string contenue)
        {
            StreamWriter flux = new StreamWriter(nom_fichier);
            flux.Write(contenue);
            flux.Close();
        }

    }
}
