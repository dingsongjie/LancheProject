using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Lanche.DynamicWebApi.Controller.Filters.Extensions
{
   internal static class HttpRequestMessageExtensions
    {
       public async static Task<HttpResponseMessage> GetRedirectResponse(this HttpRequestMessage requestMessage ,string redirectUrl)
       {
           
           if (string.IsNullOrEmpty(redirectUrl))
           {
               throw new ArgumentNullException("redirectUrl");
           }

           return await new RedirectResult(new Uri(redirectUrl), requestMessage).ExecuteAsync(CancellationToken.None);
       }
    }
}
