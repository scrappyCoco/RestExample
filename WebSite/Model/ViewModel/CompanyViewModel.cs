using System.Collections.Generic;
using WebSite.Api;

namespace WebSite.Model.ViewModel
{
	public class CompanyViewModel
	{
		public ICollection<KeyValuePairOfStringAndString> Statuses { get; set; }
		public ICollection<Company> Companies { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public string CurrentStatusCode { get; set; }
		public string CurrentCompanyName { get; set; }
	}
}