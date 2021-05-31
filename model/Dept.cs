namespace Sleave.model
{
    /// <summary>
    /// Classe métier des services
    /// </summary>
    public class Dept
    {
        /// <summary>
        /// Identifiant du service
        /// </summary>
        private int idDept;

        /// <summary>
        /// Nom du service
        /// </summary>
        private string name;

        /// <summary>
        /// Getter sur l'identifiant du service
        /// </summary>
        public int GetIdDept { get => idDept; }

        /// <summary>
        /// Getter sur le nom du service
        /// </summary>
        public string GetName { get => name; }

        /// <summary>
        /// Constructeur du motif : Valorise les propriétés
        /// </summary>
        /// <param name="idDept">ID du service</param>
        /// <param name="name">Nom du service</param>
        public Dept(int idDept, string name)
        {
            this.idDept = idDept;
            this.name = name;
        }

        /// <summary>
        /// Retourne la chaîne nom du service
        /// </summary>
        /// <returns>Nom du service</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
