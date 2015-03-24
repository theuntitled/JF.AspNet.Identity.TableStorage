using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityUserRole : ITableEntity {

		string Name { get; set; }

		string UserId { get; set; }

	}

}
