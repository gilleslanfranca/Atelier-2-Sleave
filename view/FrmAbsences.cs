using Sleave.control;
using Sleave.model;
using System;
using System.Collections.Generic;
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
        /// Constante : Nombre maximal de lignes affichée sans barre déroulante
        /// </summary>
        private const int maxRows = 12;

        /// <summary>
        /// Constante : Chaîne nom de la liste d'actions déroulante
        /// </summary>
        private const string actionText = "Gérer les absences";

        /// <summary>
        /// Instance de contrôle
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
        /// <param name="controller">Contrôleur</param>
        /// <param name="pers">Personnel concerné</param>
        /// <param name="frmPersonnel">Interface de gestion du personnel</param>
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
        /// <param name="sender">L'objet concerné</param>
        /// <param name="e">L'évènement déclancheur</param>
        private void CboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Le code peut être simplifié mais il est beaucoup plus lisible ainsi
            switch (cboAction.SelectedIndex)
            {
                case 0:
                    SetFields();
                    dtpStart.Value = DateTime.Today;
                    dtpEnd.Value = DateTime.Today;
                    cboReason.Text = "Choissisez un motif";
                    break;
                case 1:
                    if (CheckDGVIndex())
                    {
                        ToggleSelection();
                        ToggleButtons();
                        Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        txtDateStart.Text = absence.GetDateStart.ToString("dd.MM.yyyy");
                        txtDateEnd.Text = absence.GetDateEnd.ToString("dd.MM.yyyy");
                        cboReason.Text = absence.GetReason;
                    }
                    break;
                case 2:
                    if (CheckDGVIndex())
                    {
                        SetFields();
                        Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        dtpStart.Value = absence.GetDateStart;
                        dtpEnd.Value = absence.GetDateEnd;
                        cboReason.Text = absence.GetReason;
                    }
                    break;
                default:
                    BeginInvoke(new Action(() => cboAction.Text = actionText));
                    break;
            }
        }
        /// <summary>
        /// Corrige la date de fin si elle est plus petite que la date de début
        /// </summary>
        /// <param name="sender">L'objet concerné</param>
        /// <param name="e">L'évènement déclancheur</param>
        private void DtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value.Date < dtpStart.Value.Date)
            {
                dtpEnd.Value = dtpStart.Value;
            }
        }

        /// <summary>
        /// Corrige la date de début si elle est plus grande que la date de fin
        /// </summary>
        /// <param name="sender">L'objet concerné</param>
        /// <param name="e">L'évènement déclancheur</param>
        private void DtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value.Date < dtpStart.Value.Date)
            {
                dtpStart.Value = dtpEnd.Value;
            }
        }

        /// <summary>
        /// Vérifie et valide l'action demandée 
        /// </summary>
        /// <param name="sender">L'objet déclancheur</param>
        /// <param name="e">L'évènement déclancheur</param>
        private void BtnValid_Click(object sender, EventArgs e)
        {
            // Le code peut être simplifié mais il est beaucoup plus lisible ainsi
            switch (cboAction.SelectedIndex)
            {
                case 0:
                    if (CheckReason() && CheckDatesOfAbsence(null))
                    {
                        Reason reasonAdd = (Reason)bdgReasons.List[bdgReasons.Position];
                        Absence absenceAdd = new Absence(pers.GetIdPersonnel, pers.GetLastName, pers.GetFirstName, dtpStart.Value.Date, dtpEnd.Value.Date, reasonAdd.GetIdReason, reasonAdd.GetName);
                        controller.AddAbsence(absenceAdd);
                        ResetForm();
                        bdgAbsences.MoveLast();
                    }
                    break;
                case 1:
                    if (MessageBox.Show("Supprimer l'absence du " + txtDateStart.Text + " au " + txtDateStart.Text +
                        " ?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    { 
                        Absence absenceDel = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        controller.DelAbsence(absenceDel);
                    }
                    ResetForm();
                    bdgAbsences.MoveFirst();
                    break;
                case 2:
                    if (CheckReason())
                    {
                        Absence absenceDel = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        if (CheckDatesOfAbsence(absenceDel))
                        {
                            if (MessageBox.Show("Modifier l'absence du " + dtpStart.Value.Date.ToString("dd.MM.yyyy") +
                                " au " + dtpEnd.Value.Date.ToString("dd.MM.yyyy") + " ?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                controller.DelAbsence(absenceDel);
                                Reason reasonAdd = (Reason)bdgReasons.List[bdgReasons.Position];
                                Absence absenceAdd = new Absence(pers.GetIdPersonnel, pers.GetLastName, pers.GetFirstName, dtpStart.Value.Date, dtpEnd.Value.Date, reasonAdd.GetIdReason, reasonAdd.GetName);
                                controller.AddAbsence(absenceAdd);
                            }
                            ResetForm();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Annule l'action et réinitialise l'interface
        /// </summary>
        /// <param name="sender">L'Objet concerné</param>
        /// <param name="e">L'évènement déclancheur</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        /// <summary>
        /// Ferme l'interface et ouvre l'interface de gestion du personnel
        /// </summary>
        /// <param name="sender">L'objet concerné</param>
        /// <param name="e">L'évènement déclancheur</param>
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
        /// Defini la taille du champs motif selon le nombre de lignes dans la grille de données
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
            cboAction.Items.Add("Ajouter");
            cboAction.Items.Add("Supprimer");
            cboAction.Items.Add("Modifier");
            cboAction.Text = actionText;
        }

        /// <summary>
        /// Active les champs d'informations/ de saisies
        /// </summary>
        private void SetFields()
        {
            ToggleSelection();
            ToggleButtons();
            HideDtpProtection();
            cboReason.Enabled = true;
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
        /// Vérifie que tous le champ motif est rempli et que le motif choisi existe
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
        /// Verfifie que dates d'absence ne coincident pas avec les dates d'une absence déjà présente
        /// </summary>
        /// <param name="absence">Absence à verifier</param>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckDatesOfAbsence(Absence absence)
        {
            foreach (Absence abs in bdgAbsences)
            {
                if (cboAction.SelectedIndex == 0 || (cboAction.SelectedIndex == 2 && !abs.Equals(absence)))
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
        /// Réinitialise le texte de liste d'action et informe l'utlisateur si aucun élément de la grille de données n'est selectionné
        /// </summary>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckDGVIndex()
        {
            if (dgvAbsences.RowCount < 1)
            {
                MessageBox.Show("Aucune absence selectionnée.", "Absence", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAction.SelectedIndex = -1;

                return false;
            }
            return true;   
        }
    }
}
