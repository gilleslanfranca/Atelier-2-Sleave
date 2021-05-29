using Sleave.connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sleave.dal
{
    class DataAccess
    {
        /// <summary>
        /// Chaine de connexion à la base de données
        /// </summary>
        private static string connectionString = "server=localhost;user id=responsable;password=pwd;database=mediatek86;SslMode=none";

        /// <summary>
        /// Controle si l'utilisateur a le droit de se connecter
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static Boolean ControlAccess(string login, string pwd)
        {
            string req = "select * from responsable res ";
            req += "where res.login=@login and pwd=SHA2(@pwd, 256);";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@login", login);
            parameters.Add("@pwd", pwd);
            ConnectionDataBase curs = ConnectionDataBase.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);
            if (curs.Read())
            {
                curs.Close();
                return true;
            }
            else
            {
                curs.Close();
                return false;
            }
        }

    }
}
