using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public class IdentityUserRole : TableEntity , IIdentityUserRole {

		[IgnoreProperty]
		public string Name {
			get {
				return RowKey;
			}
			set {
				RowKey = value;
			}
		}

		[IgnoreProperty]
		public string UserId {
			get {
				return PartitionKey;
			}
			set {
				PartitionKey = value;
			}
		}

	}

}
