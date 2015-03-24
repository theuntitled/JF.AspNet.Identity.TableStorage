using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityUserLogin : ITableEntity {

		string UserId { get; set; }

		string LoginProvider { get; set; }

		string ProviderKey { get; set; }

	}

}
