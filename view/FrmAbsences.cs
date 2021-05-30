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
        /// Constante : Unité de largeur des champs de la grille de données
        /// </summary>
        private const int fieldWidthUnit = 25;

        /// <summary>
        /// Constante : Nombre maximal de ligne affichée sans barre déroulante
        /// </summary>
        private const int maxRows = 12;

        /// <summary>
        /// Constante : Chaine nom de la liste d'actions déroulante
        /// </summary>
        private const string actionText = "Gérer les absences";

        /// <summary>
        /// Instance de controle
        /// </summary>
        private Controller controller;

        /// <summary>
        /// Instance du personnel géré dans l'interface
        /// </summary>
        private Personnel pers;

        /// <summary>
        /// Objet source gérant la liste des absences
        /// </summary>
        BindingSource bdgAbsences = new BindingSource();

        /// <summary>
        /// Objet source gérant la liste des raisons
        /// </summary>
        BindingSource bdgReasons = new BindingSource();

        /// <summary>
        /// Interface appellant cet objet
        /// </summary>
        private FrmPersonnel frmPersonnel;

        /// <summary>
        /// Constructeur : Initialise les éléments de l'interface de gestion des absences
        /// </summary>
        public FrmAbsences(Controller controller, Personnel pers, FrmPersonnel frmPersonnel)
        {
            this.controller = controller;
            this.pers = pers;
            this.frmPersonnel = frmPersonnel;
            InitializeComponent();
            txtLastname.Text = pers.GetLastName;
            txtFirstName.Text = pers.GetFirstName;
            ShowDtpProtection();
            ToggleButtons();
            cboReason.Enabled = false;
            BindDGVAbsences();
            BindDGVReasons();
            BindActions();
            DrawDGVAbsences();
            ResizeDGVAbsences();
        }

        /// <summary>
        /// Recherche l'action demandée et prépare l'interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ajouter
            if (cboAction.SelectedIndex == 0)
            {
                EnableFields();
                dtpStart.Value = DateTime.Today;
                dtpEnd.Value = DateTime.Today;
                cboReason.Text = "Choissisez un motif";
            }
            // Supprimer
            if (cboAction.SelectedIndex == 1 && CheckDGVIndex())
            {
                Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                txtDateStart.Text = absence.GetDateStart.ToString("dd.MM.yyyy");
                txtDateEnd.Text = absence.GetDateEnd.ToString("dd.MM.yyyy");
                cboReason.Text = absence.GetReason;
            }
            if (cboAction.SelectedIndex == 2 && CheckDGVIndex())
            {
                EnableFields();
                cboReason.Enabled = true;
                Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                dtpStart.Value = absence.GetDateStart;
                dtpEnd.Value = absence.GetDateEnd;
                cboReason.Text = absence.GetReason;
            }
            if(cboAction.SelectedIndex == -1)
            {
                cboAction.Text = actionText;
            }
        }

        /// <summary>
        /// Verifie et valide l'action demandée 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValid_Click(object sender, EventArgs e)
        {
            if(cboAction.SelectedIndex == 1)
            {
                if (ConfirmChange("Supprimer l'absence du " + txtDateStart.Text + " au " + txtDateStart.Text + " ?", "Supprimer"))
                {
                    Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                    controller.DelAbsence(absence);
                    ResetForm();
                    bdgAbsences.MoveFirst();
                }
            }
            else
            {
                Reason reason = (Reason)bdgReasons.List[bdgReasons.Position];
                Absence absence = new Absence(pers.GetIdPersonnel, pers.GetLastName, pers.GetFirstName, dtpStart.Value.Date, dtpStart.Value.Date, reason.GetIdReason, reason.GetName);
                if (CheckReason() && CheckDatesOfAbsence(absence))
                {
                    // Ajouter
                    if (cboAction.SelectedIndex == 0)
                    {
                        controller.AddAbsence(absence);
                        ResetForm();
                        bdgAbsences.MoveLast();
                    }
                    // Modifier
                    if (cboAction.SelectedIndex == 0)
                    {
                        if (ConfirmChange("Modifier l'absence du " + absence.GetDateStart.ToString("dd.MM.yyyyy") + " au " + absence.GetDateEnd.ToString("dd.MM.yyyyy") + " ?", "Modifier"))
                        {
                            Absence absenceDel = (Absence)bdgAbsences.List[bdgAbsences.Position];
                            controller.DelAbsence(absenceDel);
                            controller.AddAbsence(absence);
                            ResetForm();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Annule l'action et reinitialise l'interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
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

        /// <summary>
        /// Réinitialise l'interface
        /// </summary>
        private void ResetForm()
        {
            ToggleSelection();
            ToggleButtons();
            BindDGVAbsences();
            ShowDtpProtection();
            txtDateStart.Text = "";
            txtDateEnd.Text = "";
            cboReason.Enabled = false;
            cboReason.SelectedIndex = -1;
            cboReason.Text = "";
            cboAction.Text = actionText;
        }
        /// <summary>
        /// Redessine la grille de données des absences
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
        /// Defini la taille du champs Motif selon le nombre de ligne dans la grille de données
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
        /// Initialise la grille de données des absences
        /// </summary>
        private void BindDGVAbsences()
        {
            List<Absence> absences = controller.GetAbsences(pers);
            bdgAbsences.DataSource = absences;
            dgvAbsences.DataSource = bdgAbsences;
        }

        /// <summary>
        /// Initialise la liste déroulante des motifs
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
            cboAction.Text = actionText;
            cboAction.Items.Add("Ajouter");
            cboAction.Items.Add("Supprimer");
            cboAction.Items.Add("Modifier");
        }

        /// <summary>
        /// Active les champs d'informations/ de saisie
        /// </summary>
        private void EnableFields()
        {
            ToggleSelection();
            ToggleButtons();
            HideDtpProtection();
        }

        /// <summary>
        /// Cache les champs protecteurs des dates de l'absence
        /// </summary>
        private void HideDtpProtection()
        {
            txtDateStart.Visible = false;
            txtDateEnd.Visible = false;
        }

        /// <summary>
        /// Affiche les champs protecteurs des dates de l'absence
        /// </summary>
        private void ShowDtpProtection()
        {
            txtDateStart.Visible = true;
            txtDateEnd.Visible = true;
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

        /// <summary>
        /// Verifie que tous le champ motif est rempli et que le motif choisi existe
        /// </summary>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckReason()
        {
            if (cboReason.Text.Equals(""))
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Saisie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string value = cboReason.Text;
            cboReason.Text = "";
            int index = cboReason.FindString(value);
            cboReason.Text = value;
            if (index < 0 || cboReason.SelectedIndex < 0)
            {
                MessageBox.Show("Choisissez un motif existant.", "Motif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifie les dates de l'absence
        /// </summary>
        /// <returns></returns>
        private bool CheckDatesOfAbsence(Absence absence)
        {
            foreach (Absence abs in bdgAbsences)
            {
                if (abs == absence)
                {
                    //La date de début ou de fin trouve déja dans une absence
                    if (dtpStart.Value.Date >= abs.GetDateStart.Date && dtpStart.Value.Date <= abs.GetDateEnd.Date)
                    {
                        MessageBox.Show("La date de debut se trouve dans une période d'absence.", "Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (dtpEnd.Value.Date >= abs.GetDateStart.Date && dtpEnd.Value.Date <= abs.GetDateEnd.Date)
                    {
                        MessageBox.Show("La date de fin se trouve dans une période d'absence.", "Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    //La date de début se trouve avant et la date de fin après une absence
                    if (dtpStart.Value.Date < abs.GetDateStart.Date && dtpEnd.Value.Date > abs.GetDateEnd.Date)
                    {
                        MessageBox.Show("Une absence se trouve déjà dans cette periode d'absence", "Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Demande la confirmation de pousuivre l'action 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns>Vrai ou Faux</returns>
        private bool ConfirmChange(string message, string title)
        {
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Réinitialise le texte de liste d'action et informe l'utlisateur si aucun élément de la grille de données n'est selectionné
        /// </summary>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckDGVIndex()
        {
            if (dgvAbsences.RowCount < 1)
            {
                MessageBox.Show("Aucune absence n'est selectionnés.", "Absence", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAction.SelectedIndex = -1;
                return false;
            }
            return true;   
        }
    }
}
