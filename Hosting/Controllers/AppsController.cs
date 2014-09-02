using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Voodoo;
namespace Hosting.Controllers
{
	public class AppsController : ApiController
	{
		
		[HttpGet]
		public Voodoo.Messages.PagedResponse<Hosting.Messages.AppMessage> Get
			([FromUri] Hosting.Messages.AppQueryRequest request)
			{
				var op = new Hosting.Operations.ApplicationQuery(request);
				var response = op.Execute();
				return response;
			}

	}
}
