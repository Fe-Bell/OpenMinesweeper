using ReflectXMLDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    public class GameState : ICollectableObject
    {
        [XmlAttribute]
        public uint EID { get; set; }
        [XmlAttribute]
        public string GUID { get; set; }

        public string Grid { get; set; }
        public string State { get; set; }
        public string Date { get; set; }
       
        public GameState()
        {
           
        }
    }
}
