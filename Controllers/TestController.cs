using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RH.Models;
namespace RH.Controllers{
    public class TestController : Controller{
        
        public IActionResult createform()
        {
            // string idParam = "1"; 
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

        public IActionResult Save(){
            int idbesoin = int.Parse(Request.Cookies["idbesoin-test"]);
            var existingCookie = Request.Cookies["questionDataList"];
            List<QuestionData> questionDataList;
            if (string.IsNullOrEmpty(existingCookie))
            {
                return RedirectToAction("createform");
            }
            questionDataList = JsonConvert.DeserializeObject<List<QuestionData>>(existingCookie);
            Questionnaire q = new Questionnaire(); 
            q.questions = questionDataList; 
            Besoin besoin = new Besoin(); 
            besoin.idBesoin = idbesoin;
            q.Besoin = besoin; 
            try{
                q.Insert(null);
                Response.Cookies.Delete("idbesoin-test");
                Response.Cookies.Delete("questionDataList");

                return RedirectToAction("detailsOffre","welcome",new{besoin=idbesoin});
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
                throw e;
                // return RedirectToAction("createform");
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