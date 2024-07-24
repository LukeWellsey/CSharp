using Newtonsoft.Json.Linq;

class DecisionGame {
    static void Main() {
        var jsonData = File.ReadAllText("game_text.json");
        var data = JObject.Parse(jsonData);
        bool hasSword = false;

        Console.WriteLine(data["intro"]);
        string decision = GetUserPrompt(["torch","sword","enter","back"], data);
        if (decision == "torch") {
            ReadOutput(data, "story", "takeTorch");
            decision = GetUserPrompt(["sword","enter","back"], data);
            if (decision == "sword" || decision == "enter") {
                if (decision == "sword") {
                    hasSword = true;
                    ReadOutput(data, "story", "takeSword");
                }
                ReadOutput(data, "story", "seeCorridor");
                decision = GetUserPrompt(["walk","look"], data);
                if (decision == "walk") {
                    ReadOutput (data, "endings", "becomeTrapped");
                } else if (decision == "look") {
                    ReadOutput(data, "story", "avoidTrap");
                    decision = GetUserPrompt(["continue","back"], data);
                    if (decision == "continue") {
                        ReadOutput(data, "story", "reachRoom");
                        decision = GetUserPrompt(["approach","open"], data);
                        if (decision == "approach") {
                            ReadOutput(data, "story", "fallDown");
                        } else if (decision == "open") {
                            ReadOutput(data, "story", "fallDownChest");
                        }
                        if(hasSword) {
                            decision = GetUserPrompt(["light","climb"], data);
                            if (decision == "light") {
                                ReadOutput(data, "story", "holeClosed");
                                ReadOutput(data, "misc", "end");
                            } else if (decision == "climb") {
                                ReadOutput(data, "story", "climbOut");
                                decision = GetUserPrompt(["approach","open"], data);
                                if (decision == "approach") {
                                    ReadOutput(data, "endings", "curseBlind");
                                    ReadOutput(data, "misc", "end");
                                } else if (decision == "open") {
                                    ReadOutput(data, "story", "goldCoins");
                                    ReadOutput(data, "endings", "congratulations");
                                }                              
                            }
                        } else {
                            ReadOutput(data, "misc", "end");
                        }
                    } else if (decision == "back") {
                        ReadOutput(data, "endings", "claustrophobic");
                        ReadOutput(data, "misc", "end");
                    }
                }
            } else if (decision == "back") {
                ReadOutput (data, "endings", "nothingVentured");
            }
        } else if (decision == "sword") {
            ReadOutput(data, "endings", "swordNoLight");
        } else if (decision == "enter") {
            ReadOutput (data, "endings", "noLight");
        } else if (decision == "back") {
            ReadOutput (data, "endings", "nothingVentured");
        } //no need for catch-all 'else' as the getUserPrompt method handles invalid selections and re-prompts user.
    }
    /*************************************************************
    * getUserPrompt accepts a string array of valid options, as well 
    * as the json file. The method loops through each of the valid  
    * options and compares it with the user input it also collects,
    * and the method returns the validated input, or reprompts the
    * user if no valid input was located.
    *************************************************************/
    static string GetUserPrompt(string[] validOptions, JObject data) {
        // Console.WriteLine("getUserPrompt starting");
        while (true) {
            //print out the valid options
            if (data != null) {
                foreach (string option in validOptions) {
                    Console.WriteLine(data["actions"][option]);
                }
            }

            //linebreak
            Console.WriteLine("\n");
            Console.WriteLine(data["misc"]["prompt"]);
            //? denotes a nullable type, even though I check for non-null on the next line
            string? input = Console.ReadLine();
            if (input != null) {
                input = input.ToLower();
                foreach (string option in validOptions) {
                    if (input == option) {
                        return input;
                    }
                }
            }
            Console.WriteLine(data["misc"]["invalid"]);
        }
    }
    /*******************************************************************
    * ReadOutput handles repetitive data validation and console printing
    ******************************************************************/
    static void ReadOutput(JObject data, string path, string message) {
        if (data != null && path != null && message != null) {
            Console.WriteLine("");
            Console.WriteLine(data[path][message]);
            Console.WriteLine("");
        }   
    }
}