using Microsoft.AspNet.Identity;

namespace JF.AspNet.Identity.TableStorage {

	public interface ICloudTableUserStore<TUser> : IUserStore<TUser>
		where TUser : class , IIdentityUser , IUser<string> , new() {

	}

}
