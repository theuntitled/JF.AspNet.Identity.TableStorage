using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public class IdentityUserClaim : TableEntity , IIdentityUserClaim {

		public IdentityUserClaim() {
			Id = Guid.NewGuid().ToString();
		}

		[IgnoreProperty]
		public string Id {
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

		public string ClaimType { get; set; }

		public string ClaimValue { get; set; }

	}

}
