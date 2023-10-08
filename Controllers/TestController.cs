using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RH.Models; 
namespace RH.Controllers{
    public class TestController : Controller{
        
        public IActionResult createform()
        {
            string idParam = Request.Query["idbesoin"];
            Response.Cookies.Append("idbesoin-test", idParam, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            });
            return View( "Views/Home/CreateForm.cshtml" );
        }

        [HttpPost]
        public async Task<string> AddQuestion()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string jsonBody = await reader.ReadToEndAsync();
                    QuestionData newQuestionData = JsonConvert.DeserializeObject<QuestionData>(jsonBody);
                    string question = newQuestionData.Question;
                    List<string> options = newQuestionData.Options;
                    var existingCookie = Request.Cookies["questionDataList"];
                    List<QuestionData> questionDataList;
                    if (string.IsNullOrEmpty(existingCookie))
                    {
                        questionDataList = new List<QuestionData>();
                    }
                    else
                    {
                        // Désérialisez la liste JSON depuis le cookie
                        questionDataList = JsonConvert.DeserializeObject<List<QuestionData>>(existingCookie);
                    }
                    questionDataList.Add(newQuestionData);
                    var updatedCookieValue = JsonConvert.SerializeObject(questionDataList);
                    Response.Cookies.Append("questionDataList", updatedCookieValue, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                    return JsonConvert.SerializeObject(questionDataList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        
        [HttpGet]
        public async Task<string> GetAll(){
            try
            {
                string existingCookie = Request.Cookies["questionDataList"];
                string questionDataList = "";
                if (string.IsNullOrEmpty(existingCookie) ==  false)
                {
                    questionDataList = existingCookie;
                }
                return questionDataList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}