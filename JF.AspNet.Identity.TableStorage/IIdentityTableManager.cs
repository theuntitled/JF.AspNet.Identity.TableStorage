using JF.Azure.TableStorage;

namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityTableManager<TUser> : ITableManager
		where TUser : class , IIdentityUser , new() {

		Table<TUser> Users { get; set; }

		Table<IdentityUserRole> UserRoles { get; set; }

		Table<IdentityUserClaim> UserClaims { get; set; }

		Table<IdentityUserLogin> UserLogins { get; set; }

	}

}
