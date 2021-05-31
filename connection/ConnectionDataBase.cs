using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sleave.connection
{   
    /// <summary>
    /// Classe de connection à la base de données
    /// </summary>
     class ConnectionDataBase
    {
        /// <summary>
        /// Instance Unique de la classe
        /// </summary>
        private static ConnectionDataBase instance = null;

        /// <summary>
        /// Objet de connexion à la BDD à partir d'une chaîne de connexion
        /// </summary>
        private MySqlConnection connection;

        /// <summary>
        /// Objet pour exécuter une requête SQL
        /// </summary>
        private MySqlCommand command;

        /// <summary>
        /// Objet contenant le résultat d'une requête "select" (curseur)
        /// </summary>
        private MySqlDataReader reader;

        /// <summary>
        /// Constructeur privé pour créer la connexion à la BDD et l'ouvrir
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion</param>
        private ConnectionDataBase(string stringConnect)
        {
            try
            {
                connection = new MySqlConnection(stringConnect);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// Crée une instance unique de la classe
        /// </summary>
        /// <param name="stringConnect">Chaine de connexion</param>
        /// <returns>Instance unique de la classe</returns>
        public static ConnectionDataBase GetInstance(string stringConnect)
        {
            if (instance is null)
            {
                instance = new ConnectionDataBase(stringConnect);
            }
            return instance;
        }

        /// <summary>
        /// Exécute une requête type "select" et valorise le curseur
        /// </summary>
        /// <param name="stringQuery">Requête select</param>
        /// <param name="parameters">Dictionnaire contenant les paramètres</param>
        public void ReqSelect(string stringQuery, Dictionary<string, object> parameters)
        {
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Exécution d'une requête autre "select"
        /// </summary>
        /// <param name="stringQuery">Requête noQuery</param>
        /// <param name="parameters">Dictionnaire contenant les paramètres</param>
        public void ReqNoQuery(string stringQuery, Dictionary<string, object> parameters)
        {
            try
            {
                command = new MySqlCommand(stringQuery, connection);
                if (!(parameters is null))
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Tente de lire la ligne suivante du curseur
        /// </summary>
        /// <returns>False si fin de curseur atteinte</returns>
        public bool Read()
        {
            if (reader is null)
            {
                return false;
            }
            try
            {
                return reader.Read();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne le contenu d'un champ dont le nom est passé en paramètre
        /// </summary>
        /// <param name="nameField">Nom du champ</param>
        /// <returns>Valeur du champ</returns>
        public object Field(string nameField)
        {
            if (reader is null)
            {
                return null;
            }
            try
            {
                return reader[nameField];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Fermeture du curseur
        /// </summary>
        public void Close()
        {
            if (!(reader is null))
            {
                reader.Close();
            }
        }
    }
}
