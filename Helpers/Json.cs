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

        public Objeto deserializeObjeto(string file) { 
            string jsonString = File.ReadAllText("../../../Objects/" + file);

            Objeto resultObjeto = JsonSerializer.Deserialize<Objeto>(jsonString)!;
            return resultObjeto;
        }

    }
}
