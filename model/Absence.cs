using System;

namespace Sleave.model
{
    /// <summary>
    /// Classe métier des absences
    /// </summary>
    public class Absence
    {
        /// <summary>
        /// Identifiant du personnel
        /// </summary>
        private int idPersonnel;

        /// <summary>
        /// Nom du personnel
        /// </summary>
        private string lastName;

        /// <summary>
        /// Prénom du personnel
        /// </summary>
        private string firstName;

        /// <summary>
        /// Début de l'absence (Date)
        /// </summary>
        private DateTime dateStart;

        /// <summary>
        /// Fin de l'absence (Date)
        /// </summary>
        private DateTime dateEnd;

        /// <summary>
        /// Identifiant du motif d'absence
        /// </summary>
        private int idReason;

        /// <summary>
        /// Libellé du motif d'absence
        /// </summary>
        private string reason;

        /// <summary>
        /// Getter sur l'identifiant du personnel
        /// </summary>
        public int GetIdpersonnel { get => idPersonnel; }

        /// <summary>
        /// Getter sur le nom du personnel
        /// </summary>
        public string GetLastname { get => lastName; }

        /// <summary>
        /// Getter sur le prénom du personnel
        /// </summary>
        public string GetFirstName { get => firstName; }

        /// <summary>
        /// Getter sur la date de début de l'absence
        /// </summary>
        public DateTime GetDateStart { get => dateStart.Date; }

        /// <summary>
        /// Getter sur la date de fin de l'absence
        /// </summary>
        public DateTime GetDateEnd { get => dateEnd.Date; }

        /// <summary>
        /// Getter sur l'identifiant du motif de l'absence
        /// </summary>
        public int GetIdReason { get => idReason; }

        /// <summary>
        /// Getter sur le motif de l'absence
        /// </summary>
        public string GetReason { get => reason; }

        /// <summary>
        /// Constructeur de l'absence : Valorise les propriétés
        /// </summary>
        /// <param name="idPersonnel">ID du personnel</param>
        /// <param name="lastName">Nom du personnel</param>
        /// <param name="firstName">Prénom du personnel</param>
        /// <param name="dateStart">Date de début d'absence</param>
        /// <param name="dateEnd">Date de fin d'absence</param>
        /// <param name="idReason">ID du motif</param>
        /// <param name="reason">Libellé du motif</param>
        public Absence(int idPersonnel, string lastName, string firstName, DateTime dateStart, DateTime dateEnd, int idReason, string reason)
        {
            this.idPersonnel = idPersonnel;
            this.lastName = lastName;
            this.firstName = firstName;
            this.dateStart = dateStart.Date;
            this.dateEnd = dateEnd.Date;
            this.idReason = idReason;
            this.reason = reason;
        }
    }
}
