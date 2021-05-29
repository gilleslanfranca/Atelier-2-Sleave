using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleave.model
{
    /// <summary>
    /// Classe métier des motifs
    /// </summary>
    public class Reason
    {
        private int idReason;
        private string name;

        /// <summary>
        /// Getter sur l'identifiant du motif 
        /// </summary>
        public int GetIdmotif { get => idReason; }

        /// <summary>
        /// Getter sur le libellé du motif
        /// </summary>
        public string GetLibelle { get => name; }

        /// <summary>
        /// Constructeur du motif: valorise les propriétés
        /// </summary>
        /// <param name="idReason"></param>
        /// <param name="name"></param>
        public Reason(int idReason, string name)
        {
            this.idReason = idReason;
            this.name = name;
        }
        /// <summary>
        /// Retourne la chaine nom du motif 
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
