using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Notino.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //fileName zadavat jako argument
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            //mozna kontrola:
            //      existuje sourceFileName soubor?
            //      je sourceFileName jiny nez targetFileName?
            //      existuje cesta k targetFileName?
            //      existuje targetFileName?

            try
            {
                //chybi using pripadne manualni Dispose()
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                //chybi using pripadne manualni Dispose()
                var reader = new StreamReader(sourceStream);
                //posledni prikaz v bloku - "zbytecne"
                //presunout deklaraci input pred try/catch blok
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                //vytvoreni nove Exception se zpravou puvodni Exception (ztracim info o puvodni Exception)
                //zbytecny try/catch blok
                //pokud chci napr. logovat, vyhodit puvodni Exception pouze pres 'throw;'
                throw new Exception(ex.Message);
            }

            //promenna input neni deklarovana
            //presunout deklaraci input pred try/catch blok
            var xdoc = XDocument.Parse(input);
            
            var doc = new Document
            {
                //'title' prip. 'text' nemusi byt v souboru definovane
                //osetrit null 'Element("title")?.Value'
                //case sensitive - bud ignore case nebo nacitat 'Title' a 'Text'
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            //existuje cesta/soubor targetFileName? pokud existuje chceme jej prepsat?
            //chybi using pripadne manualni Dispose()
            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            //chybi using pripadne manualni Dispose()
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);
        }
    }
}