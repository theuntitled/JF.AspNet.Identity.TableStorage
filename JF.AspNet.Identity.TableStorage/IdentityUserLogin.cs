using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public class IdentityUserLogin : TableEntity , IIdentityUserLogin {

		[IgnoreProperty]
		public string UserId {
			get {
				return PartitionKey;
			}
			set {
				PartitionKey = value;
			}
		}

		[IgnoreProperty]
		public string LoginProvider {
			get {
				return RowKey;
			}
			set {
				RowKey = value;
			}
		}

		public string ProviderKey { get; set; }

	}

}
