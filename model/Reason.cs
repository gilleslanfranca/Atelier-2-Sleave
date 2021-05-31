namespace Sleave.model
{
    /// <summary>
    /// Classe métier des motifs
    /// </summary>
    public class Reason
    {
        /// <summary>
        /// Identifiant du motif
        /// </summary>
        private int idReason;

        /// <summary>
        /// Libellé du motif
        /// </summary>
        private string name;

        /// <summary>
        /// Getter sur l'identifiant du motif 
        /// </summary>
        public int GetIdReason { get => idReason; }

        /// <summary>
        /// Getter sur le libellé du motif
        /// </summary>
        public string GetName { get => name; }

        /// <summary>
        /// Constructeur du motif : Valorise les propriétés
        /// </summary>
        /// <param name="idReason">ID du motif</param>
        /// <param name="name">Libellé du motif</param>
        public Reason(int idReason, string name)
        {
            this.idReason = idReason;
            this.name = name;
        }
        /// <summary>
        /// Retourne la chaîne libellé du motif 
        /// </summary>
        /// <returns>Libellé du motif</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
