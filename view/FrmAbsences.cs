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
        BindingSource bdgAbsence = new BindingSource();

        /// <summary>
        /// Objet source gérant la liste des raison
        /// </summary>
        BindingSource bdgReason = new BindingSource();

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
    }
}
