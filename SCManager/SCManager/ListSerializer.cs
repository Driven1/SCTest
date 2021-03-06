﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SCManager
{
    public static class ListSerializer
    {
        //Schreibt Shortcut-Liste in XML
        public static void Serialize(ShortcutList list, string xmlpath)
        {
            // Erstellt eine neue Instanz der XmlSerializer-Klasse für den Typ unserer Listenklasse
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ShortcutList));

            StringWriter ValidationStream = new StringWriter();

            SerializerObj.Serialize(ValidationStream, list);
            ValidationStream.Close();
            string ValidationData = ValidationStream.ToString();

            //validiert das bei der Serialisierung entstandene XML gegen das Schema bevor es gespeichert wird
            string ValidationResult = ValidateXML(ValidationData, list.SchemaLocation);

            if (ValidationResult == "ok")
            {
                // Erstellt eine neue StreamWriter-Instanz um die Daten in eine Datei zu schreiben
                TextWriter WriteFileStream = new StreamWriter(xmlpath);

                // Alle Shortcuts werden als einzelne Child-Elemente("Shortcut") von "Shortcuts" geschrieben
                SerializerObj.Serialize(WriteFileStream, list);

                // Aufräumarbeiten
                WriteFileStream.Close();
            }
            else
            {
                 //Platzhalter
            }
        }

        //Liest Shortcut-Liste aus XML aus
        public static void DeSerialize(ShortcutList list, string xmlpath)
        {
            // Erstellt eine neue Instanz der XmlSerializer-Klasse für den Typ unserer Listenklasse
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ShortcutList));

            // Erstellt einen neuen FileStream um die Daten zu lesen
            FileStream ReadFileStream = new FileStream(xmlpath, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Deserialisiert das XML-File und erzeugt daraus ein XMLShortcutList-Objekt
            ShortcutList LoadedObj = (ShortcutList)SerializerObj.Deserialize(ReadFileStream);

            //Überträgt die Shortcut-Liste von dem deserialisierten Objekt in das übergebene XMLShortcutList-Objekt (aktualisiert also die Liste, die bereits verwendet wird)
            list.Shortcuts = LoadedObj.Shortcuts;

            // Aufräumarbeiten
            ReadFileStream.Close();

        }
        //Prüft, ob das gegebene XML dem gegebenen Schema entspricht
        public static string ValidateXML(string xml, string schema)
        {

            XDocument xdoc = XDocument.Parse(xml);
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(null, schema);

            try
            {
                xdoc.Validate(schemas, null);
                return "ok";
            }
            catch (XmlSchemaValidationException e)
            {
                return e.Message;
            }

        }
    }
}
