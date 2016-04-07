﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SCManager
{
    [XmlRoot(ElementName = "ShortcutList")]
    public class ShortcutList
    {
        [XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = XmlSchema.InstanceNamespace)]
        public string SchemaLocation = "SCM_Schema.xsd";

        public List<Shortcut> Shortcuts = new List<Shortcut>();
        // Settings könnten hier noch rein und in das gleiche XML geschrieben werden
    }

    [Serializable()]
    public class Shortcut
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string shorthand;
        public string Shorthand
        {
            get { return shorthand; }
            set { shorthand = value; }
        }

        private string keycombo;
        public string Keycombo
        {
            get { return keycombo; }
            set { keycombo = value; }
        }

        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        // IsNullable wird benutzt um festzulegen, ob bei der Serialisierung Felder die null sind mit einem entsprechenden Eintrag serialisiert oder komplett ignoriert werden.
        [XmlElementAttribute(IsNullable = false)]
        private string iconLocation;
        public string IconLocation
        {
            get { return iconLocation; }
            set { iconLocation = value; }
        }

        [XmlElementAttribute(IsNullable = false)]
        private string parameters;
        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
    }
}
