using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public class IdentityUser : TableEntity , IIdentityUser , IUser {

		public IdentityUser() {
			Id = Guid.NewGuid().ToString();

			Roles = new List<IdentityUserRole>();
			Claims = new List<IdentityUserClaim>();
			Logins = new List<IdentityUserLogin>();
		}

		[IgnoreProperty]
		public string Id {
			get {
				return PartitionKey;
			}
			private set {
				PartitionKey = value;
			}
		}

		[IgnoreProperty]
		public string UserName {
			get {
				return Email;
			}
			set {
				Email = value;
			}
		}

		[IgnoreProperty]
		public string Email {
			get {
				return RowKey;
			}
			set {
				RowKey = value;
			}
		}

		public bool EmailConfirmed { get; set; }

		public string PasswordHash { get; set; }

		public string SecurityStamp { get; set; }

		public string PhoneNumber { get; set; }

		public bool PhoneNumberConfirmed { get; set; }

		public bool TwoFactorEnabled { get; set; }

		public DateTime? LockoutEndDateUtc { get; set; }

		public bool LockoutEnabled { get; set; }

		public int AccessFailedCount { get; set; }

		public virtual ICollection<IdentityUserRole> Roles { get; private set; }

		public virtual ICollection<IdentityUserClaim> Claims { get; private set; }

		public virtual ICollection<IdentityUserLogin> Logins { get; private set; }

	}

}
