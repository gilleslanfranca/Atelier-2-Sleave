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
    /// Interface graphique pour la gestion des absences
    /// </summary>
    public partial class FrmAbsences : Form
    {
        /// <summary>
        /// Constante d'unité de largeur des champs de la grille de données
        /// </summary>
        private const int fieldWidthUnit = 25;

        /// <summary>
        /// Constante du nombre de ligne affichée sans barre déroulante
        /// </summary>
        private const int maxRows = 12;

        /// <summary>
        /// Instance de controle
        /// </summary>
        private Controller controller;

        /// <summary>
        /// instance du personnel géré dans l'interface
        /// </summary>
        private Personnel pers;

        /// <summary>
        /// Objet source gérant la liste des absences
        /// </summary>
        BindingSource bdgAbsences = new BindingSource();

        /// <summary>
        /// Objet source gérant la liste des raison
        /// </summary>
        BindingSource bdgReasons = new BindingSource();

        /// <summary>
        /// Interface appellant cette objet
        /// </summary>
        private FrmPersonnel frmPersonnel;

        /// <summary>
        /// 
        /// </summary>
        public FrmAbsences(Controller controller, Personnel persAbsence, FrmPersonnel frmPersonnel)
        {
            this.controller = controller;
            this.pers = persAbsence;
            this.frmPersonnel = frmPersonnel;
            InitializeComponent();
            txtLastname.Text = persAbsence.GetLastName;
            txtFirstName.Text = persAbsence.GetFirstName;
            ToggleButtons();
            DisableAbsFields();
            BindDGVAbsences();
            BindDGVReasons();
            BindActions();
            DrawDGVAbsences();
        }

        /// <summary>
        /// Ferme l'interface et ouvre l'interface "Gestion du personnel"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAbsences_FormClosing(Object sender, FormClosingEventArgs e)
        {
            controller.OpenFrmPersonnel(frmPersonnel);
        }

        private void BtnValid_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Redessine la grille de données
        /// </summary>
        private void DrawDGVAbsences()
        {
            dgvAbsences.Columns["GetIdPersonnel"].Visible = false;
            dgvAbsences.Columns["GetLastName"].Visible = false;
            dgvAbsences.Columns["GetFirstName"].Visible = false;
            dgvAbsences.Columns["GetDateStart"].HeaderText = "Date début";
            dgvAbsences.Columns["GetDateEnd"].HeaderText = "Date fin";
            dgvAbsences.Columns["GetIdReason"].Visible = false;
            dgvAbsences.Columns["GetReason"].HeaderText = "Motif";
            dgvAbsences.Columns["GetDateStart"].Width = fieldWidthUnit * 4;
            dgvAbsences.Columns["GetDateEnd"].Width = fieldWidthUnit * 4;
            dgvAbsences.Columns["GetReason"].Width = fieldWidthUnit * 6;
        }

        /// <summary>
        /// Defini la taille du champs Motifselon le nombre de ligne dans la grille de données
        /// </summary>
        private void ResizeDGVAbsences()
        {
            if (dgvAbsences.RowCount > maxRows)
            {
                dgvAbsences.Columns["GetReason"].Width = fieldWidthUnit * 6;
            }
            else
            {

                dgvAbsences.Columns["GetReason"].Width = fieldWidthUnit * 7;
            }
        }

        /// <summary>
        /// Initialise la grille de données du personnel
        /// </summary>
        private void BindDGVAbsences()
        {
            List<Absence> absences = controller.GetAbsences(pers);
            bdgAbsences.DataSource = absences;
            dgvAbsences.DataSource = bdgAbsences;
        }

        /// <summary>
        /// Initialise la grille de données des services
        /// </summary>
        private void BindDGVReasons()
        {
            List<Reason> reasons = controller.GetReasons();
            bdgReasons.DataSource = reasons;
            cboReason.DataSource = bdgReasons;
            cboReason.SelectedIndex = -1;
            cboReason.Text = "";
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

        private void EnableAbsFields()
        {
            dtpStart.Enabled = true;
            dtpEnd.Enabled = true;
            cboReason.Enabled = true;
        }

        private void DisableAbsFields()
        {
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
            cboReason.Enabled = false;
        }

        /// <summary>
        /// Désactive les dates d'absence
        /// </summary>
        private void DisableDateFields()
        {
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
        }

        /// <summary>
        /// Active ou désactive les champs de sélection
        /// </summary>
        private void ToggleSelection()
        {
            dgvAbsences.Enabled = !dgvAbsences.Enabled;
            cboAction.Enabled = !cboAction.Enabled;
        }

        /// <summary>
        /// Active ou désactive les boutons
        /// </summary>
        private void ToggleButtons()
        {
            btnCancel.Enabled = !btnCancel.Enabled;
            btnValid.Enabled = !btnValid.Enabled;
        }

    }
}
