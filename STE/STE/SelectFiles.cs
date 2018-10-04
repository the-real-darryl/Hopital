using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MembreSTE;

namespace STE
{
    public partial class SelectFiles : Form
    {
        GestionnaireSTE gestionnaireSTE;

        public SelectFiles(GestionnaireSTE gestionnaireSTE)
        {
            InitializeComponent();
            this.gestionnaireSTE = gestionnaireSTE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string nom = comboBox1.Text;


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string nomFichier = saveFileDialog1.FileName;

                switch (nom)
                {
                    case "prix":
                        gestionnaireSTE.Exporter(nomFichier, Prix.All_file);
                        break;
                    case "donateurs":
                        gestionnaireSTE.Exporter(nomFichier, Donnateur.All_file);
                        break;
                    case "dons":
                        gestionnaireSTE.Exporter(nomFichier, Don.All_file);
                        break;
                    case "commanditaires":
                        gestionnaireSTE.Exporter(nomFichier, Commandiatire.All_file);
                        break;
                }
            }
            this.Close();
        }

    }
}
