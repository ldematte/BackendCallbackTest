using CallbackTest.Hubs;
using Microsoft.AspNet.SignalR;
using Pumpkin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestApplication;

namespace CallbackTest.Controllers {
   public class HomeController : Controller {
      public ActionResult Index() {
         return View();
      }

      private static async Task SubmitSnippetPostFeedback(string snippetId, string connectionId) {
         string result = "";
         try {
            result = await HostConnector.RunSnippetAsync(snippetId);
         }
         catch (Exception ex) {
            result = ex.Message;
         }

         var hubContext = GlobalHost.ConnectionManager.GetHubContext<ResultHub>();
         hubContext.Clients.Client(connectionId).SendResult(connectionId, result);         
      }

      [HttpPost]
      public async Task<ActionResult> SubmitRequest(string connectionId) {

         if (connectionId != null) {
            Task.Run(() => SubmitSnippetPostFeedback("AAA", connectionId)).FireAndForget();
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
         }
         else {
            string result = await HostConnector.RunSnippetAsync("AAA");
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { connectionId = connectionId, message = result });
         }         
      }
   }
}