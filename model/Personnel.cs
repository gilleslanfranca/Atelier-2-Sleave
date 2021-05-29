using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleave.model
{
    public class Personnel
    {
        private int idPersonnel;
        private string lastName;
        private string firstName;
        private string phone;
        private string mail;
        private int idDept;
        private string dept;

        /// <summary>
        /// Getter sur l'identifiant du personnel
        /// </summary>
        public int GetIdPersonnel { get => idPersonnel; }

        /// <summary>
        /// Getter sur le nom du personnel
        /// </summary>
        public string GetLastName { get => lastName; }

        /// <summary>
        /// Getter sur le prénom du personnel
        /// </summary>
        public string GetFirstName { get => firstName; }

        /// <summary>
        /// Getter sur le numéro de telephone du personnel
        /// </summary>
        public string GetPhone { get => phone; }

        /// <summary>
        /// Getter sur l'adresse mail du personnel
        /// </summary>
        public string GetMail { get => mail; }

        /// <summary>
        /// Getter sur l'identifiant de service du personnel
        /// </summary>
        public int GetIdDept { get => idDept; }

        /// <summary>
        /// Getter sur le nom de service du personnel
        /// </summary>
        public string GetDept { get => dept; }

        /// <summary>
        /// Constructeur du personnel: valorise les propriétés
        /// </summary>
        /// <param name="idPersonnel"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="phone"></param>
        /// <param name="mail"></param>
        /// <param name="idDept"></param>
        /// <param name="dept"></param>
        public Personnel(int idPersonnel, string lastName, string firstName, string phone, string mail, int idDept, string dept)
        {
            this.idPersonnel = idPersonnel;
            this.lastName = lastName;
            this.firstName = firstName;
            this.phone = phone;
            this.mail = mail;
            this.idDept = idDept;
            this.dept = dept;
        }
    }
}
