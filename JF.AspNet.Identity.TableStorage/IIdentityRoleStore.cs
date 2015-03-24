using System.Collections.Generic;

namespace JF.AspNet.Identity.TableStorage {

	public interface IIdentityRoleStore {

		List<string> Roles { get; set; }

	}

}
