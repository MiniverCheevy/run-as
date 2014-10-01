using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hosting.Operations;
using Hosting.Operations.Apps;
using Voodoo;
namespace Hosting.Controllers
{
	public class AppsController : ApiController
	{
		
		[HttpGet]
		public Voodoo.Messages.PagedResponse<AppMessage> Get
			([FromUri] AppQueryRequest request)
			{
				var op = new AppQuery(request);
				var response = op.Execute();
				return response;
			}

	}
}
