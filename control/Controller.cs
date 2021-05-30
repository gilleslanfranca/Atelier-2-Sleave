using Sleave.dal;
using Sleave.model;
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
        /// Demande de connexion avec les données saisies. Ouvre l'interface "Gestion du Personnel" si correct
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

        /// <summary>
        /// Ferme l'interface et ouvre l'interface "Gestion des absences"
        /// </summary>
        /// <param name="frmPersonnel"></param>
        /// <param name="persAbsence"></param>
        public void OpenFrmAbsence(FrmPersonnel frmPersonnel, Personnel persAbsence)
        {
            frmPersonnel.Hide();
            FrmAbsences formAbsence = new FrmAbsences(this, persAbsence, frmPersonnel);
            formAbsence.ShowDialog();
        }

        /// <summary>
        /// Ouvre l'interface "Gestion du personnel"
        /// </summary>
        /// <param name="frmPersonnel"></param>
        public void OpenFrmPersonnel(FrmPersonnel frmPersonnel)
        {
            frmPersonnel.Show();
        }

        /// <summary>
        /// Demande et retourne la liste des personnels à DataAccess
        /// </summary>
        /// <returns>Liste des personnels</returns>
        public List<Personnel> GetPersonnel()
        {
            return DataAccess.GetPersonnel();
        }

        /// <summary>
        /// Demande et retourne la liste des services à DataAccess
        /// </summary>
        /// <returns>Liste des services</returns>
        public List<Dept> GetDepts()
        {
            return DataAccess.GetDepts();
        }

        /// <summary>
        /// Demande l'ajout d'un personnel à DataAccess
        /// </summary>
        /// <param name="pers"></param>
        public void AddPersonnel(Personnel pers)
        {
            DataAccess.AddPersonnel(pers);
        }

        /// <summary>
        /// Demande l'effacement d'un personnel à DataAccess
        /// </summary>
        /// <param name="pers"></param>
        public void DeletePersonnel(Personnel pers)
        {
            DataAccess.DeletePersonnel(pers);
        }

        /// <summary>
        /// Demande la modification d'un personnel à DataAccess
        /// </summary>
        /// <param name="persUp"></param>
        public void UpdatePersonnel(Personnel persUp)
        {
            DataAccess.UpdatePersonnel(persUp);
        }

        /// <summary>
        /// Demande l'effacement de toutes les absences d'un personnel à DataAccess
        /// </summary>
        /// <param name="index"></param>
        public void DeleteAllAbsences(int index)
        {
            DataAccess.DeleteAllAbsences(index);
        }
    }
}
