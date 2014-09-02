using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Voodoo;
namespace Hosting.Controllers
{
	public class UsersController : ApiController
	{
		
		[HttpGet]
		public Voodoo.Messages.ListResponse<ra.Models.UserAccount> Get
			([FromUri] Voodoo.Messages.EmptyRequest request)
			{
				var op = new Hosting.Operations.UserAccountQuery(request);
				var response = op.Execute();
				return response;
			}

	}
}
