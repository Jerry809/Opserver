using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StackExchange.Opserver.Controllers
{
    public class PerformanceCounterController : Controller
    {
        [Route("performance-counter")]
        public ActionResult Get(string node)
        {
            var performanceCounterNode =
                PerformanceCounterSettings.Instance.Nodes.Single(
                    x => x.Name.Equals(node, StringComparison.OrdinalIgnoreCase));

            var client = new HttpClient();
            var response = client.GetAsync(performanceCounterNode.Url).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            return this.Content(responseBody, "application/json");
        }
    }
}
