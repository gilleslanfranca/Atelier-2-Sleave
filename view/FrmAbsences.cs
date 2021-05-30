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
        /// Recherche l'action demandée après selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleSelection();
            ToggleButtons();

            switch (cboAction.SelectedIndex)
            {
                // Ajouter 
                case 0:
                    HideDtpProtection();
                    cboReason.Enabled = true;
                    cboReason.Text = "Choissisez un motif";
                    break;
                // Supprimer
                case 1:
                    if (CheckDGVIndex())
                    {
                        Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        txtDateStart.Text = absence.GetDateStart.ToString("dd.MM.yyyy");
                        txtDateEnd.Text = absence.GetDateEnd.ToString("dd.MM.yyyy");
                        cboReason.Text = absence.GetReason;
                    }
                    break;
                // Modifier
                case 2:
                    if (CheckDGVIndex())
                    {
                        HideDtpProtection();
                        Absence absence = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        dtpStart.Value = absence.GetDateStart;
                        dtpEnd.Value = absence.GetDateEnd;
                        cboReason.Enabled = true;
                        cboReason.Text = absence.GetReason;
                    }   
                    break;
                // Afficher texte
                default:
                    cboAction.Text = "Gérer les absences";
                    break;
            }
        }

        

        /// <summary>
        /// Verifie et valide l'action demandée 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValid_Click(object sender, EventArgs e)
        {
            switch (cboAction.SelectedIndex)
            {
                // Ajouter 
                case 0:
                    Reason reasonAdd = (Reason)bdgReasons.List[bdgReasons.Position];
                    Absence absenceAdd = new Absence(pers.GetIdPersonnel, pers.GetLastName, pers.GetFirstName, dtpStart.Value.Date, dtpStart.Value.Date, reasonAdd.GetIdReason, reasonAdd.GetName);

                    if (CheckReason() && CheckDatesOfAbsence(absenceAdd))
                    {
                        controller.AddAbsence(absenceAdd);
                    }
                    break;
                // Supprimer
                case 1:
                    if (ConfirmChange("Supprimer l'absence du " + txtDateStart.Text + " au " + txtDateStart.Text + " ?", "Supprimer"))
                    {
                        Absence absenceDel = (Absence)bdgAbsences.List[bdgAbsences.Position];
                        controller.DelAbsence(absenceDel);
                    }
                    break;
                // Modifier
                case 2:
                    Absence absenceMod = (Absence)bdgAbsences.List[bdgAbsences.Position];
                    if (CheckReason() && CheckDatesOfAbsence(absenceMod))
                    { 
                        if (ConfirmChange("Modifier l'absence du " + absenceMod.GetDateStart.ToString("dd.MM.yyyyy") + " au " + absenceMod.GetDateEnd.ToString("dd.MM.yyyyy") + " ?", "Modifier"))
                        {
                            controller.DelAbsence(absenceMod);
                            Reason reasonUp = (Reason)bdgReasons.List[bdgReasons.Position];
                            Absence absenceUp = new Absence(pers.GetIdPersonnel, pers.GetLastName, pers.GetFirstName, dtpStart.Value.Date, dtpStart.Value.Date, reasonUp.GetIdReason, reasonUp.GetName);
                            controller.AddAbsence(absenceUp);
                        }
                    }

                    break;
                case 3:
                    break;
            }
            BindDGVAbsences();
            ResetForm();
        }

        /// <summary>
        /// Vide les champs de protection des dates de l'absence
        /// </summary>
        private void EmptyDtpProtection()
        {
            txtDateStart.Text = "";
            txtDateEnd.Text = "";
        }

        private void ResetForm()
        {
            ToggleSelection();
            ToggleButtons();
            ShowDtpProtection();
            EmptyDtpProtection();
            cboReason.Enabled = false;
            cboReason.Text = "";
            cboAction.Text = "Gérer les absences";
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
        /// Ferme l'interface et ouvre l'interface "Gestion du personnel"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAbsences_FormClosing(Object sender, FormClosingEventArgs e)
        {
            controller.OpenFrmPersonnel(frmPersonnel);
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
            cboReason.SelectedItem = -1;
            cboReason.Text = "";
        }

        /// <summary>
        /// Initialise les commandes possibles dans l'interface
        /// </summary>
        private void BindActions()
        {
            cboAction.Items.Clear();
            cboAction.Text = "Gérer les absences";
            cboAction.Items.Add("Ajouter");
            cboAction.Items.Add("Supprimer");
            cboAction.Items.Add("Modifier");
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
        /// Active les champs d'information/ de saisie de l'absence
        /// </summary>
        private void EnableAbsFields()
        {
            cboReason.Enabled = true;
        }


        /// <summary>
        /// Désactive les champs d'information/ de saisie de l'absence
        /// </summary>
        private void DisableAbsFields()
        {
            //dtpStart.Enabled = false;
            //dtpEnd.Enabled = false;
            cboReason.Enabled = false;
            cboReason.Text = "";

        }

        /// <summary>
        /// Désactive les dates d'absences
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

        /// <summary>
        /// Verifie que tous le champ motif est rempli et que le motif choisi existe
        /// </summary>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckReason()
        {
            if (cboReason.Text.Equals("") || cboReason.Text.Equals(""))
            {
                MessageBox.Show("Tous les champs sont obligatoires.");
                return false;
            }
            string value = cboReason.Text;
            cboReason.Text = "";
            int index = cboReason.FindString(value);
            cboReason.Text = value;
            if (index < 0 || cboReason.SelectedIndex < 0)
            {
                MessageBox.Show("Choisissez un motif existant.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
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
                        MessageBox.Show("La date de debut se trouve dans une période d'absence.");
                        return false;
                    }
                    if (dtpEnd.Value.Date >= abs.GetDateStart.Date && dtpEnd.Value.Date <= abs.GetDateEnd.Date)
                    {
                        MessageBox.Show("La date de fin se trouve dans une période d'absence.");
                        return false;
                    }
                    //La date de début se trouve avant et la date de fin après une absence
                    if (dtpStart.Value.Date < abs.GetDateStart.Date && dtpEnd.Value.Date > abs.GetDateEnd.Date)
                    {
                        MessageBox.Show("Une absence se trouve déjà dans cette periode d'absence");
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
            if (MessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifie qu'un élément est présent dans la grille de données des absences
        /// </summary>
        /// <returns>Vrai ou Faux</returns>
        private bool CheckDGVIndex()
        {
            MessageBox.Show(dgvAbsences.RowCount.ToString());
            if (dgvAbsences.RowCount < 1)
            {
                MessageBox.Show("Aucun personnel n'est selectionné.");
                ToggleSelection();
                ToggleButtons();
                cboAction.Text = "Gérer les absences";
                return false;
            }
            return true;   
        }
    }
}
