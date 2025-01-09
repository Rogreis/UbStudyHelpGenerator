using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq; // Use Newtonsoft.Json for JSON parsing

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{

    internal class Gemini
    {
        private string ProjectNumber = "919631459604";
        private string GeminiApiKey = "AIzaSyDw5Lmx38D4K9RP-G3KmuLWZmbYY4BTBlc";

        public string GenerateJson(string prompt)
        {
            // Create the "parts" array
            var partsArray = new JsonArray();
            var partObject = new JsonObject
            {
                ["text"] = prompt
            };
            partsArray.Add(partObject);

            // Create the "contents" array
            var contentsArray = new JsonArray();
            var contentObject = new JsonObject
            {
                ["parts"] = partsArray
            };
            contentsArray.Add(contentObject);

            // Create the root object
            var rootObject = new JsonObject
            {
                ["contents"] = contentsArray
            };

            // Serialize to JSON string
            return rootObject.ToJsonString(new JsonSerializerOptions { WriteIndented = false });
        }


        public async Task<string> GenerateContent()
        {
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=" + GeminiApiKey;

            var client = new HttpClient();

            var requestContent = new StringContent(
                "{ \"contents\": [{ \"parts\":[{\"text\": \"Explain how AI works\"}] } ] }",
                Encoding.UTF8,
                "application/json");

            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            else
            {
                throw new Exception($"Error generating content: {response.StatusCode}");
            }
        }

        public async Task<string> TranslateEnglishToPortuguese(string textToTranslate)
        {
            if (string.IsNullOrEmpty(textToTranslate))
            {
                return ""; // Or throw an ArgumentException
            }

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={GeminiApiKey}";
            var client = new HttpClient();

            // Construct the prompt for translation. Be very explicit.
            var prompt = $"Translate to Portuguese:\n\n\"{textToTranslate}\"";

            var requestContent = new StringContent(GenerateJson(prompt));
 

            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await client.PostAsync(url, requestContent);
                response.EnsureSuccessStatusCode(); // Throw exception for bad status codes

                var responseString = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to extract the translated text
                JObject jsonResponse = JObject.Parse(responseString);
                string translatedText = jsonResponse["candidates"][0]["content"].ToString();

                // [JSON].candidates.[0].content.parts.[0]
                // Improved extraction (more robust)
                translatedText = jsonResponse["candidates"][0]["content"]["parts"][0]["text"].ToString();

                if (jsonResponse["candidates"] is JArray candidates && candidates.Count > 0)
                {
                    if (candidates[0] is JObject candidate && candidate["content"] is JValue contentValue)
                    {
                        translatedText = contentValue.ToString();
                    }
                }

                //Clean up the response. Remove the prompt from the answer.
                if (translatedText.StartsWith(prompt))
                {
                    translatedText = translatedText.Substring(prompt.Length).Trim();
                }

                return translatedText;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                // Handle the exception appropriately (e.g., log, retry, throw)
                return $"Translation error: {ex.Message}"; // Return an error message
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                return $"Translation error: Invalid JSON response";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Error: {ex.Message}");
                return $"Translation error: {ex.Message}";
            }
        }



    }
}




