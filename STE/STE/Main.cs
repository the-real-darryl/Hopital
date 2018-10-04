using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MembreSTE;

namespace STE
{
    public partial class Form2 : Form
    {
        GestionnaireSTE gste;
        public Form2()
        {
            gste = new GestionnaireSTE();
            InitializeComponent();
            rtbArea.Text = gste.Message;
        }

        private void btnAjouterDon_Click(object sender, EventArgs e)
        {
            string iDDon = txtIDDon.Text;
            DateTime ahjourdui = DateTime.Now;
            string dateDuDon = ahjourdui.ToString("dd/MM/yyyy");
            string iDDonateur = txtIDDonneur.Text;
            double montantDuDon;
            if (!Double.TryParse(txtMontantDon.Text, out montantDuDon))
            {
                DialogResult reponse = MessageBox.Show("La montant du don doit etre en chiffres",
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string iDPrix = txtIDPrixDonneurs.Text;
            if (!String.IsNullOrEmpty(iDDon) && !String.IsNullOrEmpty(iDDonateur) && montantDuDon > 0 && !String.IsNullOrEmpty(iDPrix))
            {

                Don dentrainednetre = new Don(iDDon, dateDuDon, iDDonateur, montantDuDon, iDPrix);
                if (gste.VerifierDon(dentrainednetre))
                {
                    if (gste.AttribuerPrix(dentrainednetre.MontantDuDon1, dentrainednetre.IDPrix1))
                    {
                        gste.Dons.Add(dentrainednetre);
                        Don.All_file += dentrainednetre.ToFile() + "\r\n"; ;

                        rtbArea.Text = "Le dons est ajoute";
                        txtIDDon.Text = String.Empty;
                        txtIDDonneur.Text = String.Empty;
                        txtMontantDon.Text = String.Empty;
                        txtIDPrixDonneurs.Text = String.Empty;
                        txtIDDon.Focus();

                        rtbArea.Text = gste.AfficherDons();
                    }
                    else
                    {
                        DialogResult reponse = MessageBox.Show("La prix n'est pas disponible. Choisissez un autre prix",
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    rtbArea.Text = "Le dons n'est pas ajoute";
                }

            }
            else
            {
                DialogResult reponse = MessageBox.Show("Vous devez remplir tous les donnes",
            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAfficherDon_Click(object sender, EventArgs e)
        {
            rtbArea.Text = gste.AfficherDons();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            gste.Exporter(gste.getNOM_FICHIERS(0), Commandiatire.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(1), Prix.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(2), Donnateur.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(3), Don.All_file);

            Application.Exit();
        }

        private void btnAjouterDonneur_Click(object sender, EventArgs e)
        {
            string iDDonateur = txtIDDonneur.Text.Trim();
            string prenom = txtPrenomDonneur.Text.Trim();
            string surnom = txtNomDonneur.Text.Trim();
            string adresse = txtAdresseDonneur.Text.Trim();
            string telephone = txtTelDonneur.Text.Trim();
            char typeDeCarte;
            if (rbVisa.Checked)
            {
                typeDeCarte = 'V';
            }
            else if (rbMasterCard.Checked)
            {
                typeDeCarte = 'M';
            }
            else if (rbAmex.Checked)
            {
                typeDeCarte = 'A';
            }
            else
            {
                typeDeCarte = 'Z';
                DialogResult reponse = MessageBox.Show("Vous devez remplir tous les donnes et choisir le type de votre carte: ",
            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string numeroDeCarte = txtNumeroCarte.Text.Trim();
            //string dateExpiration = txtDateExpirationCarte.Text.Trim();
            string dateExpiration = dtpDateExp.Value.ToShortDateString();
            if (!String.IsNullOrEmpty(iDDonateur) && !String.IsNullOrEmpty(prenom) && !String.IsNullOrEmpty(surnom) && !String.IsNullOrEmpty(adresse) &&
                !String.IsNullOrEmpty(telephone) && !String.IsNullOrEmpty(numeroDeCarte) && !String.IsNullOrEmpty(dateExpiration) && !dateExpiration.Equals(" "))
            {
                if (typeDeCarte != 'Z')
                {
                    Donnateur dentraindentre = new Donnateur(iDDonateur, prenom, surnom, adresse, telephone, typeDeCarte, numeroDeCarte, dateExpiration);
                    if (gste.VerifierDonnateur(dentraindentre))
                    {
                        gste.Donnateurs.Add(dentraindentre);
                        Donnateur.All_file += dentraindentre.ToFile() + "\r\n"; ;

                        rtbArea.Text = "Le donnateur est ajoute";
                        txtIDDonneur.Text = String.Empty;
                        txtPrenomDonneur.Text = String.Empty;
                        txtNomDonneur.Text = String.Empty;
                        txtAdresseDonneur.Text = String.Empty;
                        txtTelDonneur.Text = String.Empty;
                        rbVisa.Checked = false;
                        rbMasterCard.Checked = false;
                        rbAmex.Checked = false;
                        txtNumeroCarte.Text = String.Empty;
                        //txtDateExpirationCarte.Text = String.Empty;
                        txtIDDonneur.Focus();

                        rtbArea.Text = gste.AfficherDonnateur();
                    }
                    else
                    {
                        rtbArea.Text = "Le donnateur n'est pas ajoute";
                    }


                }
            }
            else
            {
                DialogResult reponse = MessageBox.Show("Vous devez remplir tous les donnes et choisir le type de votre carte: ",
            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAfficherDonneur_Click(object sender, EventArgs e)
        {
            rtbArea.Text = gste.AfficherDonnateur();
        }

        private void btnAjouterCommanditaire_Click(object sender, EventArgs e)
        {
            string iDCommanditaire = txtIDCommanditaire.Text.Trim();
            string prenom = txtPrenomCommanditaire.Text.Trim();
            string surnom = txtNomCommanditaire.Text.Trim();
            if (!String.IsNullOrEmpty(iDCommanditaire) && !String.IsNullOrEmpty(prenom) && !String.IsNullOrEmpty(surnom))
            {
                Commandiatire centraindentre = new Commandiatire(iDCommanditaire, prenom, surnom);
                if (gste.VerifierCommanditaire(centraindentre))
                {
                    gste.Commanditaires.Add(centraindentre);
                    Commandiatire.All_file += centraindentre.ToFile() + "\r\n"; ;

                    rtbArea.Text = "Le commanditaire est ajoute";
                    txtIDCommanditaire.Text = String.Empty;
                    txtPrenomCommanditaire.Text = String.Empty;
                    txtNomCommanditaire.Text = String.Empty;
                    txtIDCommanditaire.Focus();

                    rtbArea.Text = gste.AfficherCommanditaires();
                }
                else
                {
                    rtbArea.Text = "Le commanditaire n'est pas ajoute";
                }
            }
            else
            {
                DialogResult reponse = MessageBox.Show("Vous devez remplir tous les donnes! ",
            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAfficherCommanditaire_Click(object sender, EventArgs e)
        {
            rtbArea.Text = gste.AfficherCommanditaires();

        }

        private void btnAjouterPrix_Click(object sender, EventArgs e)
        {
            string iDPrix = txtIDPrixCommanditaire.Text.Trim();
            string description = txtDescriptionPrix.Text.Trim();
            double valeur;
            if (!Double.TryParse(txtValeurPrix.Text, out valeur))
            {
                DialogResult reponse = MessageBox.Show("La valeur doit etre en chiffres",
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            double donMinimum;
            if (!Double.TryParse(txtDonMinimumPrix.Text, out donMinimum))
            {
                DialogResult reponse = MessageBox.Show("Le don minimum doit etre en chiffres",
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int qnte_Originale;
            if (!Int32.TryParse(txtQuantitePrix.Text, out qnte_Originale))
            {
                DialogResult reponse = MessageBox.Show("Le quantite doit etre en chiffres",
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string iDCommanditaire = txtIDCommanditaire.Text.Trim();
            if (!String.IsNullOrEmpty(iDPrix) && !String.IsNullOrEmpty(description) && valeur > 0 && donMinimum > 0 && qnte_Originale > 0 && !String.IsNullOrEmpty(iDCommanditaire))
            {
                Prix pentraindentre = new Prix(iDPrix, description, valeur, donMinimum, qnte_Originale, iDCommanditaire);
                if (gste.VerifierPrix(pentraindentre))
                {
                    gste.Prix.Add(pentraindentre);
                    Prix.All_file += pentraindentre.ToFile() + "\r\n"; ;

                    rtbArea.Text = "Le commanditaire est ajoute";
                    txtIDPrixCommanditaire.Text = String.Empty;
                    txtDescriptionPrix.Text = String.Empty;
                    txtValeurPrix.Text = String.Empty;
                    txtDonMinimumPrix.Text = String.Empty;
                    txtQuantitePrix.Text = String.Empty;
                    txtIDCommanditaire.Text = String.Empty;
                    txtIDPrixCommanditaire.Focus();

                    rtbArea.Text = gste.AfficherPrix();
                }
                else
                {
                    rtbArea.Text = "Le prix n'est pas ajoute";
                }
            }
            else
            {
                DialogResult reponse = MessageBox.Show("Vous devez remplir tous les donnes! ",
           "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnAfficherPrix_Click(object sender, EventArgs e)
        {
            rtbArea.Text = gste.AfficherPrix();
        }



        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFiles sf = new SelectFiles(gste);
            sf.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            gste.Exporter(gste.getNOM_FICHIERS(0), Commandiatire.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(1), Prix.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(2), Donnateur.All_file);
            gste.Exporter(gste.getNOM_FICHIERS(3), Don.All_file);

            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult response = MessageBox.Show("Désirez-vous réellement quitter cette application ? ", "Attention",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (response == DialogResult.No)
            {
                e.Cancel = true;

            }
        }

        private void AfficherPrixDon_Click(object sender, EventArgs e)
        {
            rtbArea.Text = gste.AfficherPrix();
        }

        private void dtpDateExp_ValueChanged(object sender, EventArgs e)
        {
            dtpDateExp.CustomFormat = "dd MMMM yyyy";
        }

        private void fichierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomFichier = openFileDialog1.FileName;
                StreamReader lecteur;
                string switch_ = nomFichier.Substring(nomFichier.LastIndexOf("\\"), nomFichier.Length - 1);

                //"commanditaires.txt", "prix.txt", "donnateurs.txt", "dons.txt"

                string[] donne;
                string ligne;

                switch (switch_)
                {
                    case "commanditaires.txt":
                        lecteur = new StreamReader(nomFichier);
                        Commandiatire Commandiatire_pour_verification;

                        while ((ligne = lecteur.ReadLine()) != null)
                        {
                            donne = ligne.Split(',');
                            Commandiatire_pour_verification = new Commandiatire(donne);
                            if (gste.VerifierCommanditaire(Commandiatire_pour_verification))
                            {
                                gste.Commanditaires.Add(Commandiatire_pour_verification);
                                Commandiatire.All_file += Commandiatire_pour_verification.ToFile() + "\r\n";
                            }
                        }
                        lecteur.Close();
                        break;

                    case "prix.txt":
                        lecteur = new StreamReader(nomFichier);
                        Prix Prix_pour_verification;

                        while ((ligne = lecteur.ReadLine()) != null)
                        {
                            donne = ligne.Split(',');
                            Prix_pour_verification = new Prix(donne);
                            if (gste.VerifierPrix(Prix_pour_verification))
                            {
                                gste.Prix.Add(Prix_pour_verification);
                                // Prix. = Prix_pour_verification.ToFile();
                                MembreSTE.Prix.All_file += Prix_pour_verification.ToFile() + "\r\n"; ;
                            }
                        }
                        lecteur.Close();
                        break;

                    case "donnateurs.txt":
                        lecteur = new StreamReader(nomFichier);
                        Donnateur Donnateur_pour_verification;
                        while ((ligne = lecteur.ReadLine()) != null)
                        {
                            donne = ligne.Split(',');
                            Donnateur_pour_verification = new Donnateur(donne);
                            if (gste.VerifierDonnateur(Donnateur_pour_verification))
                            {
                                gste.Donnateurs.Add(Donnateur_pour_verification);
                                Donnateur.All_file += Donnateur_pour_verification.ToFile() + "\r\n"; ;
                            }

                        }
                        lecteur.Close();
                        break;

                    case "dons.txt":
                        lecteur = new StreamReader(nomFichier);
                        Don Don_pour_verification;
                        while ((ligne = lecteur.ReadLine()) != null)
                        {
                            donne = ligne.Split(',');
                            Don_pour_verification = new Don(donne);
                            if (gste.VerifierDon(Don_pour_verification))
                            {
                                gste.Dons.Add(Don_pour_verification);
                                Don.All_file += Don_pour_verification.ToFile() + "\r\n"; ;
                            }

                        }
                        lecteur.Close();
                        break;

                    default:
                        DialogResult reponse = MessageBox.Show("Vous devez chois un fichiet possedant l'un des nom suivant : 'commanditaires.txt', 'prix.txt', 'donnateurs.txt', 'dons.txt' ",
"Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }
    }
}
