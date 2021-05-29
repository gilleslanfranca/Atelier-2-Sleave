using Sleave.control;
using Sleave.model;
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
    /// /// Interface graphique pour la gestion du personnel
    /// </summary>
    public partial class FrmPersonnel : Form
    {
        /// <summary>
        /// Instance de controle
        /// </summary>
        Controller controller;

        /// <summary>
        /// Objet source gérant la liste des personnels
        /// </summary>
        BindingSource bdgPersonnel = new BindingSource();

        /// <summary>
        /// Objet source gérant la liste des services
        /// </summary>
        BindingSource bdgDepts = new BindingSource();


        /// <summary>
        /// Initialise les éléments de l'interface du personnel
        /// </summary>
        public FrmPersonnel(Controller controller)
        {
            this.controller = controller;
            InitializeComponent();
            BindDGVPersonnel();
            BindDGVDepts();
            BindActions();
            DrawDGVPersonnel();
            TogglePersFields();
            ToggleButtons();
        }

        /// <summary>
        /// Initialise la grille de données du personnel
        /// </summary>
        private void BindDGVPersonnel()
        {
            List<Personnel> personnel = controller.GetPersonnel();
            bdgPersonnel.DataSource = personnel;
            dgvPersonnel.DataSource = bdgPersonnel;
        }

        /// <summary>
        /// Initialise la grille de données des services
        /// </summary>
        private void BindDGVDepts()
        {
            List<Dept> depts = controller.GetDepts();
            bdgDepts.DataSource = depts;
            cboDept.DataSource = bdgDepts;
            cboDept.SelectedIndex = -1;
            cboDept.Text = "";
        }

        /// <summary>
        /// Initialise les commandes possibles dans l'interface
        /// </summary>
        private void BindActions()
        {
            cboAction.Items.Clear();
            cboAction.Text = "Gérer les personnels";
            cboAction.Items.Add("Ajouter");
            cboAction.Items.Add("Supprimer");
            cboAction.Items.Add("Modifier");
            cboAction.Items.Add("Afficher les absences");
        }

        /// <summary>
        /// Redessine la grille de données
        /// </summary>
        private void DrawDGVPersonnel()
        {
            dgvPersonnel.Columns["GetIdPersonnel"].HeaderText = "ID";
            dgvPersonnel.Columns["GetLastName"].HeaderText = "Nom";
            dgvPersonnel.Columns["GetFirstName"].HeaderText = "Prénom";
            dgvPersonnel.Columns["GetPhone"].HeaderText = "Téléphone";
            dgvPersonnel.Columns["GetMail"].HeaderText = "Adresse Email";
            dgvPersonnel.Columns["GetIdDept"].Visible = false;
            dgvPersonnel.Columns["GetDept"].HeaderText = "Service";
            dgvPersonnel.Columns["GetDept"].DisplayIndex = 1;
            dgvPersonnel.Columns["GetIdPersonnel"].Width = 50;
            dgvPersonnel.Columns["GetLastName"].Width = 125;
            dgvPersonnel.Columns["GetFirstName"].Width = 125;
            dgvPersonnel.Columns["GetPhone"].Width = 125;
            dgvPersonnel.Columns["GetMail"].Width = 175;
            dgvPersonnel.Columns["GetDept"].Width = 150;
            if (dgvPersonnel.RowCount < 10)
            {
                dgvPersonnel.Width = dgvPersonnel.Width - 17;
                Width = Width - 17;
            }
        }

        /// <summary>
        /// Active ou désactive le champs d'information du personnel
        /// </summary>
        private void TogglePersFields()
        {
            txtLastname.Enabled = !txtLastname.Enabled;
            txtFirstName.Enabled = !txtFirstName.Enabled;
            txtPhone.Enabled = !txtPhone.Enabled;
            txtMail.Enabled = !txtMail.Enabled;
            cboDept.Enabled = !cboDept.Enabled;
        }

        private void ToggleButtons()
        {
            btnCancel.Enabled = !btnCancel.Enabled;
            btnValid.Enabled = !btnValid.Enabled;
        }

        private void FrmPersonnel_Load(object sender, EventArgs e)
        {

        }
    }
}
