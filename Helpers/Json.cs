using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace OpentkProyect
{
    public class Json
    {
        public Json() { }
        public void serializeObjeto(Objeto objeto, string fileName) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonResult = JsonSerializer.Serialize(objeto, options);

            string file = "../../../Objects/" + fileName + ".json";
            File.WriteAllText(file, jsonResult);
        }

        public void serializeEscene(Escene escene, string fileName) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonResult = JsonSerializer.Serialize(escene, options);

            string file = "Objects/" + fileName + ".json";
            File.WriteAllText(file, jsonResult);
        }

        public Escene deserializeEscene(string path) {
            string content = File.ReadAllText(path);
            Escene result = JsonSerializer.Deserialize<Escene>(content)!;
            return result;
        
        }
        public Objeto deserializeObjeto(string file) { 
            string jsonString = File.ReadAllText(file);

            Objeto resultObjeto = JsonSerializer.Deserialize<Objeto>(jsonString)!;

            return resultObjeto;
        }

        public object Deserialize(string path) {
            string content = File.ReadAllText(path);
            object result = JsonSerializer.Deserialize<object>(content)!;
            return result;
        }

    }
}
