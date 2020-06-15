using ReflectXMLDB.Interface;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// Defines a ReflectXMLDB-compatible save game.
    /// </summary>
    public class GameState : ICollectableObject
    {
        /// <summary>
        /// A unique integer of the game state.
        /// </summary>
        [XmlAttribute]
        public uint EID { get; set; }
        /// <summary>
        /// A unique GUID for the game state.
        /// </summary>
        [XmlAttribute]
        public string GUID { get; set; }

        /// <summary>
        /// A serialized game grid.
        /// </summary>
        public string Grid { get; set; }
        /// <summary>
        /// A serialized game state.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The date of the GameState.
        /// </summary>
        public string Date { get; set; }
       
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameState()
        {
           
        }
    }
}
