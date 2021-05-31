namespace Sleave.model
{
    /// <summary>
    /// /// Classe métier du personnel
    /// </summary>
    public class Personnel
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
        /// N° de téléphone du personnel
        /// </summary>
        private string phone;

        /// <summary>
        /// Adresse mail du personnel
        /// </summary>
        private string mail;

        /// <summary>
        /// Identidiant du service du personnel
        /// </summary>
        private int idDept;

        /// <summary>
        /// Nom du service du personnel
        /// </summary>
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
        /// Getter sur le n° de téléphone du personnel
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
        /// Constructeur du personnel : Valorise les propriétés
        /// </summary>
        /// <param name="idPersonnel">ID du personnel</param>
        /// <param name="lastName">Nom du personnel</param>
        /// <param name="firstName">Prénom du personnel</param>
        /// <param name="phone">Téléphone du personnel</param>
        /// <param name="mail">Adresse Email du personnel</param>
        /// <param name="idDept">ID de service du personnel</param>
        /// <param name="dept">Nom du service du personnel</param>
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
