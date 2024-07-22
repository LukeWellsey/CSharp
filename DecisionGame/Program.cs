using System;
using System.IO;
using Newtonsoft.Json.Linq;

class DecisionGame {
    static void Main() {
        var jsonData = File.ReadAllText("game_text.json");
        var data = JObject.Parse(jsonData);

        Console.WriteLine(data["intro"]);
    }
}