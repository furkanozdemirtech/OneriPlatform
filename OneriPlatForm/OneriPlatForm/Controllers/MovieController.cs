using Newtonsoft.Json;
using OneriPlatform.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OneriPlatForm.Controllers
{

    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetDataTable()
        {
            var data = new List<string>();
            return Json(data, JsonRequestBehavior.AllowGet);
            try
            {
                return Json(new
                {
                    success = true,
                    message = "İşlem Başarılı",
                    data = data

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "İşlem Başarısız",
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);

                throw;
            }
        }
        public class RecommendationResponse
        {
            public List<List<string>> Recommendations { get; set; }
            public List<double> Scores { get; set; }
        }
        [HttpPost]
        public async Task<JsonResult> CreateList(string title, string year, string genre, string imdb, string director)
        {
            var data = new List<Movie>();
            string apiUrl = $"http://localhost:5000/get_recommendations?title={title}&year={year}&genre={genre}&imdb={imdb}&director={director}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var recommendations = JsonConvert.DeserializeObject<RecommendationResponse>(responseData);
                        int i = 0;
                        foreach (var recommendation in recommendations.Recommendations)
                        {

                            string[] lines = recommendation.ToArray();
                            var node_movie = new Movie { Title = lines[0].ToString(), Director = lines[2].ToString(), Genre = lines[1].ToString(), Year = lines[3].ToString(), Img_Url = lines[4].ToString(), Certificate = lines[5].ToString(), Duration = lines[6].ToString(), Imdb = lines[7].ToString(), Metascore = lines[8], Overview = lines[9], Actors = lines[10], Scores = recommendations.Scores[i] };
                            data.Add(node_movie);
                            i++;
                        }
                        return Json(new
                        {
                            success = true,
                            message = "İşlem başarılı!",
                            data = data
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = $"API'den başarısız yanıt alındı. StatusCode: {response.StatusCode}",
                            error = "API yanıtı başarısız."
                        });
                    }
                }
                catch (HttpRequestException ex)
                {

                    return Json(new
                    {
                        success = false,
                        message = "İşlem başarısız. API ile iletişim sırasında hata oluştu.",
                        error = ex.Message
                    });
                }
                catch (JsonException ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "İşlem başarısız. JSON dönüşümü sırasında hata oluştu.",
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "İşlem başarısız.",
                        error = ex.Message
                    });
                }
            }


        }
    }
}