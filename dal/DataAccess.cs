using Sleave.connection;
using Sleave.model;
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
        /// <returns>Vrai ou Faux</returns>
        public static Boolean ControlAccess(string login, string pwd)
        {
            string req = "SELECT * FROM responsable res ";
            req += "WHERE res.login=@login AND pwd=SHA2(@pwd, 256);";
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

        /// <summary>
        /// Récupère et retourne les personnels de la base de données
        /// </summary>
        /// <returns>Liste des personnels</returns>
        public static List<Personnel> GetPersonnel()
        {
            List<Personnel> personnel = new List<Personnel>();
            string req = "SELECT p.idpersonnel AS idPersonnel, s.idservice AS idDept, s.nom AS dept, p.nom AS lastName, p.prenom AS firstName, p.tel AS phone, p.mail AS mail ";
            req += "FROM personnel p ";
            req += "JOIN service AS s ON (p.idservice = s.idservice) ";
            req += "ORDER BY p.idpersonnel ";
            ConnectionDataBase curs = ConnectionDataBase.GetInstance(connectionString);
            curs.ReqSelect(req, null);
            while (curs.Read())
            {
                Personnel pers = new Personnel((int)curs.Field("idPersonnel"), (string)curs.Field("lastName"), (string)curs.Field("firstName"), (string)curs.Field("phone"), (string)curs.Field("mail"), (int)curs.Field("idDept"), (string)curs.Field("dept"));
                personnel.Add(pers);
            }
            curs.Close();
            return personnel;
        }

        /// <summary>
        /// Récupère et retourne les services de la base de données
        /// </summary>
        /// <returns>Liste des services</returns>
        public static List<Dept> GetDepts()
        {
            List<Dept> depts = new List<Dept>();
            string req = "SELECT idservice AS idDept, nom AS dept ";
            req += "FROM `service` ";
            req += "WHERE 1 ";
            req += "ORDER BY dept ";
            ConnectionDataBase curs = ConnectionDataBase.GetInstance(connectionString);
            curs.ReqSelect(req, null);
            while (curs.Read())
            {
                Dept dept = new Dept((int)curs.Field("idDept"), (string)curs.Field("dept"));
                depts.Add(dept);
            }
            curs.Close();
            return depts;
        }

        /// <summary>
        /// Récupère et retourne les absences de la base de données
        /// </summary>
        /// <param name="pers"></param>
        /// <returns>Liste des absences</returns>
        public static List<Absence> GetAbsences(Personnel pers)
        {
            List<Absence> absences = new List<Absence>();
            string req = "SELECT a.idpersonnel AS idPersonnel, p.nom AS lastName, p.prenom AS firstName, a.DateDebut AS dateStart, a.dateFin AS dateEnd, m.idmotif AS idReason, m.libelle AS reason ";
            req += "FROM absence AS a ";
            req += "JOIN personnel AS p ON p.idpersonnel = a.idpersonnel ";
            req += "JOIN motif AS m ON a.idmotif = m.idmotif ";
            req += "WHERE a.idpersonnel = @idpersonnel ";
            req += "ORDER BY dateStart desc";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@idpersonnel", pers.GetIdPersonnel);
            ConnectionDataBase curs = ConnectionDataBase.GetInstance(connectionString);
            curs.ReqSelect(req, parameters);
            while (curs.Read())
            {
                Absence absence = new Absence((int)curs.Field("idpersonnel"), (string)curs.Field("lastName"), (string)curs.Field("firstName"), (DateTime)curs.Field("dateStart"), (DateTime)curs.Field("dateEnd"), (int)curs.Field("idReason"), (string)curs.Field("reason"));
                absences.Add(absence);
            }
            curs.Close();
            return absences;
        }

        /// <summary>
        /// Récupère et retourne les motifs de la base de données
        /// </summary>
        /// <returns>Liste de motifs</returns>
        public static List<Reason> GetReasons()
        {
            List<Reason> reasons = new List<Reason>();
            string req = "SELECT idmotif AS idReason, libelle AS reason";
            req += "FROM motif ";
            req += "WHERE 1 ";
            req += "ORDER BY reason ";
            ConnectionDataBase curs = ConnectionDataBase.GetInstance(connectionString);
            curs.ReqSelect(req, null);
            while (curs.Read())
            {
                Reason reason = new Reason((int)curs.Field("idReason"), (string)curs.Field("reason"));
                reasons.Add(reason);
            }
            curs.Close();
            return reasons;
        }

        /// <summary>
        /// Ajoute un personnel à base de données
        /// </summary>
        /// <param name="pers"></param>
        public static void AddPersonnel(Personnel pers)
        {
            string req = "INSERT INTO personnel(nom, prenom, tel, mail, idservice) ";
            req += "VALUES (@nom, @prenom, @tel, @mail, @idservice);";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@nom", pers.GetLastName);
            parameters.Add("@prenom", pers.GetFirstName);
            parameters.Add("@tel", pers.GetPhone);
            parameters.Add("@mail", pers.GetMail);
            parameters.Add("@idservice", pers.GetIdDept);
            ConnectionDataBase conn = ConnectionDataBase.GetInstance(connectionString);
            conn.ReqNoQuery(req, parameters);
        }

        /// <summary>
        /// Efface le personnel de la base de données
        /// </summary>
        /// <param name="pers"></param>
        public static void DeletePersonnel(Personnel pers)
        {
            string req = "DELETE FROM personnel ";
            req += "WHERE idpersonnel = @idpersonnel;";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@idpersonnel", pers.GetIdPersonnel);
            ConnectionDataBase conn = ConnectionDataBase.GetInstance(connectionString);
            conn.ReqNoQuery(req, parameters);
        }

        /// <summary>
        /// Modifie le personnel dans la base de données
        /// </summary>
        /// <param name="persUp"></param>
        public static void UpdatePersonnel(Personnel persUp)
        {
            string req = "UPDATE personnel SET idservice = @idservice, nom = @nom, prenom = @prenom, tel = @tel, mail = @mail ";
            req += "where idpersonnel = @idpersonnel;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@idpersonnel", persUp.GetIdPersonnel);
            parameters.Add("@nom", persUp.GetLastName);
            parameters.Add("@prenom", persUp.GetFirstName);
            parameters.Add("@tel", persUp.GetPhone);
            parameters.Add("@mail", persUp.GetMail);
            parameters.Add("@idservice", persUp.GetIdDept);
            ConnectionDataBase conn = ConnectionDataBase.GetInstance(connectionString);
            conn.ReqNoQuery(req, parameters);
        }

        /// <summary>
        /// Efface toutes les absences d'un personnel de la base de données
        /// </summary>
        /// <param name="index"></param>
        public static void DeleteAllAbsences(int index)
        {
            string req = "DELETE FROM absence ";
            req += "WHERE idpersonnel = @idpersonnel;";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@idpersonnel", index);
            ConnectionDataBase conn = ConnectionDataBase.GetInstance(connectionString);
            conn.ReqNoQuery(req, parameters);
        }
    }
}
