using ReflectXMLDB.Interface;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// Defines a ReflectXMLDB-compatible database for the game states.
    /// </summary>
    public class GameStateDatabase : IDatabase
    {
        /// <summary>
        /// The unique identifier for the database.
        /// </summary>
        [XmlAttribute]
        public string GUID { get; set; }
        /// <summary>
        /// A collection of save games in the current database.
        /// </summary>
        public List<GameState> GameStates { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameStateDatabase()
        {
            GameStates = new List<GameState>();
        }
    }
}
