using System;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityUser : ITableEntity , IUser<string> {

		string Email { get; set; }

		bool EmailConfirmed { get; set; }

		string PasswordHash { get; set; }

		string SecurityStamp { get; set; }

		string PhoneNumber { get; set; }

		bool PhoneNumberConfirmed { get; set; }

		bool TwoFactorEnabled { get; set; }

		DateTime? LockoutEndDateUtc { get; set; }

		bool LockoutEnabled { get; set; }

		int AccessFailedCount { get; set; }

	}

}
