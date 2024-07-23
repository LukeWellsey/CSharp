using System;
using System.IO;
using Newtonsoft.Json.Linq;

class DecisionGame {
    static void Main() {
        var jsonData = File.ReadAllText("game_text.json");
        var data = JObject.Parse(jsonData);

        Console.WriteLine(data["intro"]);
    }
    static string getUserPrompt(string[] validOptions, var data) {
        while (true) {
            //using nice formatting, print out the valid options
            
            foreach (string option in validOptions) {
                Console.Write(data[option])
            }
            //blank line?
            Console.WriteLine("");

            Console.WriteLine(data["prompt"]);
            string input = Console.ReadLine().ToLower();
            foreach (string option in validOptions) {
                if (input == option) {
                    return input;
                }
            }
            Console.WriteLine(data["invalid"]);
        }
    }
}