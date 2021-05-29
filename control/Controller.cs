using Sleave.dal;
using Sleave.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleave.control
{
    /// <summary>
    /// Classe de controle de l'application
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Interface graphique de connexion
        /// </summary>
        private FrmConnection frmConnection;
        
        /// <summary>
        /// Constructeur du controle : Initialise l'interface de connexion
        /// </summary>
        public Controller()
        {
            frmConnection = new FrmConnection(this);
            frmConnection.ShowDialog();

        }

        /// <summary>
        /// Demande une connextion avec les données saisies
        /// Ouverture de l'interface Gestion du Personnel si correct
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns>Vrai ou Faux</returns>
        public Boolean ControlAccess(string login, string pwd)
        {
            if (DataAccess.ControlAccess(login, pwd))
            {
                frmConnection.Hide();
                FrmPersonnel frmPersonnel = new FrmPersonnel(this);
                frmPersonnel.ShowDialog();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
