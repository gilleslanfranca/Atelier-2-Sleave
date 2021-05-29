using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleave.model
{
    public class Dept
    {
        private int idDept;
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
        /// Constructeur du motif: valorise les propriétés
        /// </summary>
        /// <param name="idDept"></param>
        /// <param name="name"></param>
        public Dept(int idDept, string name)
        {
            this.idDept = idDept;
            this.name = name;
        }

        /// <summary>
        /// Retourne la chaine nom du service
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
