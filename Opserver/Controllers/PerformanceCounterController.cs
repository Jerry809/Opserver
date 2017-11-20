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
        public async Task<ActionResult> Get(string node)
        {
            var performanceCounterNode =
                PerformanceCounterSettings.Instance.Nodes.Single(
                    x => x.Name.Equals(node, StringComparison.OrdinalIgnoreCase));

            var client = new HttpClient();
            var response = await client.GetAsync(performanceCounterNode.Url);
            var responseBody = await response.Content.ReadAsStringAsync();

            return this.Content(responseBody, "application/json");
        }
    }
}
