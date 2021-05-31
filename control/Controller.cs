using Sleave.dal;
using Sleave.model;
using Sleave.view;
using System;
using System.Collections.Generic;

namespace Sleave.control
{
    /// <summary>
    /// Classe de contrôle de l'application
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
        /// Demande de connexion avec les données saisies. Ouvre l'interface de gestion du personnel si correct
        /// </summary>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="pwd">Mot de passe de l'utilisateur</param>
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
        /// Ouvre l'interface de gestion du personnel
        /// </summary>
        /// <param name="frmPersonnel">L'interface gestion du personnel à ouvrir</param>
        public void OpenFrmPersonnel(FrmPersonnel frmPersonnel)
        {
            frmPersonnel.Show();
        }

        /// <summary>
        /// Ferme l'interface de gestion du personnel et ouvre l'interface "de gestion des absences
        /// </summary>
        /// <param name="frmPersonnel">L'interface gestion à ferme</param>
        /// <param name="persAbsence">Le personnel concerné</param>
        public void OpenFrmAbsence(FrmPersonnel frmPersonnel, Personnel persAbsence)
        {
            frmPersonnel.Hide();
            FrmAbsences formAbsence = new FrmAbsences(this, persAbsence, frmPersonnel);
            formAbsence.ShowDialog();
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
        /// Demande et retourne la liste d'absences d'un personnel à DataAccess
        /// </summary>
        /// <param name="pers">Le personnel concerné</param>
        /// <returns>LIste des absences</returns>
        public List<Absence> GetAbsences(Personnel pers)
        {
            return DataAccess.GetAbsences(pers);
        }

        /// <summary>
        /// Demande et retourne la liste des motifs à DataAccess
        /// </summary>
        /// <returns>Liste des motifs</returns>
        public List<Reason> GetReasons()
        {
            return DataAccess.GetReasons();
        }

        /// <summary>
        /// Demande l'ajout d'un personnel à DataAccess
        /// </summary>
        /// <param name="pers">Le personnel à ajouter</param>
        public void AddPersonnel(Personnel pers)
        {
            DataAccess.AddPersonnel(pers);
        }

        /// <summary>
        /// Demande l'effacement d'un personnel à DataAccess
        /// </summary>
        /// <param name="pers">Le personnel à effacer</param>
        public void DeletePersonnel(Personnel pers)
        {
            DataAccess.DeletePersonnel(pers);
        }

        /// <summary>
        /// Demande la modification d'un personnel à DataAccess
        /// </summary>
        /// <param name="persUp">Le personnel à modifier</param>
        public void UpdatePersonnel(Personnel persUp)
        {
            DataAccess.UpdatePersonnel(persUp);
        }

        /// <summary>
        /// Demande l'ajout d'une absence à DataAccess
        /// </summary>
        /// <param name="absenceAdd">L'absence à ajouter</param>
        public void AddAbsence(Absence absenceAdd)
        {
            DataAccess.AddAbsence(absenceAdd);
        }

        /// <summary>
        /// Demande l'effacement d'une absence à DataAccess
        /// </summary>
        /// <param name="absenceDel">L'absence à effacer</param>
        internal void DelAbsence(Absence absenceDel)
        {
            DataAccess.DelAbsence(absenceDel);
        }

        /// <summary>
        /// Demande l'effacement de toutes les absences d'un personnel à DataAccess
        /// </summary>
        /// <param name="index">ID du personnel</param>
        public void DeleteAllAbsences(int index)
        {
            DataAccess.DeleteAllAbsences(index);
        }
    }
}
