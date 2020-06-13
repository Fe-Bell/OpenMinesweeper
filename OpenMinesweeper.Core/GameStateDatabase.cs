using ReflectXMLDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    public class GameStateDatabase : IDatabase
    {
        [XmlAttribute]
        public string GUID { get; set; }
        public List<GameState> GameStates { get; set; }

        public GameStateDatabase()
        {
            GameStates = new List<GameState>();
        }
    }
}
