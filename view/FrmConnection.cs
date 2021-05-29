using Sleave.control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sleave.view
{
    /// <summary>
    /// Interface graphique de la connexion
    /// </summary>
    public partial class FrmConnection : Form
    {
        /// <summary>
        /// Instance du controller
        /// </summary>
        private Controller controller;
        
        /// <summary>
        /// Initialise les éléments de l'interface de connexion
        /// </summary>
        public FrmConnection(Controller controller)
        {
            this.controller = controller;
            InitializeComponent();

        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            if (!txtLogin.Text.Equals("") && !txtPwd.Text.Equals(""))
            {
                if (!controller.ControlAccess(txtLogin.Text, txtPwd.Text))
                {
                    MessageBox.Show("Identifiant inconnu. Connexion échouée.", "Identification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLogin.Text = "";
                    txtPwd.Text = "";
                    txtLogin.Focus();
                }
            }
        }
    }
}
