namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityUserClaim {

		string Id { get; set; }

		string UserId { get; set; }

		string ClaimType { get; set; }

		string ClaimValue { get; set; }

	}

}
