using JF.Azure.TableStorage;

namespace JF.AspNet.Identity.TableStorage {

	public class IdentityTableManager<TUser> : TableManager , IIdentityTableManager<TUser>
		where TUser : IdentityUser , new() {

		public IdentityTableManager( string connectionStringName ) : base( connectionStringName ) {
		}

		public Table<TUser> Users { get; set; }

		public Table<IdentityUserRole> UserRoles { get; set; }

		public Table<IdentityUserClaim> UserClaims { get; set; }

		public Table<IdentityUserLogin> UserLogins { get; set; }

	}

}
