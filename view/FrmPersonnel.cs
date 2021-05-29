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
    /// /// Interface graphique pour la gestion du personnel
    /// </summary>
    public partial class FrmPersonnel : Form
    {
        /// <summary>
        /// Instance de controle
        /// </summary>
        Controller controller;

        

        /// <summary>
        /// Initialise les éléments de l'interface du personnel
        /// </summary>
        public FrmPersonnel(Controller controller)
        {
            this.controller = controller;
            InitializeComponent();
        }
    }
}
