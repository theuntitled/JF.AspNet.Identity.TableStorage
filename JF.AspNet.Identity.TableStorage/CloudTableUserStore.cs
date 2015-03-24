using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace JF.AspNet.Identity.TableStorage {

	public class CloudTableUserStore<TUser> : ICloudTableUserStore<TUser> ,
											  // ReSharper disable once RedundantExtendsListEntry
											  IUserStore<TUser> ,
											  IUserRoleStore<TUser , string> ,
											  IUserClaimStore<TUser , string> ,
											  IUserPasswordStore<TUser , string> ,
											  IUserSecurityStampStore<TUser , string> ,
											  IUserEmailStore<TUser , string> ,
											  IUserPhoneNumberStore<TUser , string> ,
											  IUserLoginStore<TUser , string> ,
											  IUserTwoFactorStore<TUser , string> ,
											  IUserLockoutStore<TUser , string>
		where TUser : class , IIdentityUser , IUser<string> , new() {

		private readonly IIdentityRoleStore _roleStore;
		private readonly IIdentityTableManager<TUser> _tableManager;

		public CloudTableUserStore( IIdentityTableManager<TUser> tableManager , IIdentityRoleStore roleStore ) {
			_roleStore = roleStore;
			_tableManager = tableManager;
		}

		public void Dispose() {
		}

		#region ICloudTableUserStore

		public async Task CreateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			await _tableManager.Users.AddOrUpdateAsync( user );
		}

		public async Task UpdateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			await _tableManager.Users.AddOrUpdateAsync( user );
		}

		public async Task DeleteAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			await _tableManager.Users.RemoveAsync( user );
		}

		public Task<TUser> FindByIdAsync( string userId ) {
			var result = _tableManager.Users.FindByPartitionKey( userId );

			if ( result != null ) {
				return Task.FromResult( result.FirstOrDefault() );
			}

			return Task.FromResult<TUser>( null );
		}

		public Task<TUser> FindByNameAsync( string userName ) {
			var result = _tableManager.Users.FindByRowKey( userName );

			if ( result != null ) {
				return Task.FromResult( result.FirstOrDefault() );
			}

			return Task.FromResult<TUser>( null );
		}

		#endregion

		#region IUserRoleStore

		public Task AddToRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			if ( !_roleStore.Roles.Contains( roleName ) ) {
				throw new InvalidOperationException( string.Format( "'{0}' is not a valid role name." , roleName ) );
			}

			_tableManager.UserRoles.AddOrUpdate(
				new IdentityUserRole {
					Name = roleName ,
					UserId = user.Id ,
				} );

			return Task.FromResult( 0 );
		}

		public Task RemoveFromRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			var role = _tableManager.UserRoles.FindByRowKey( roleName ).FirstOrDefault();

			if ( role != null ) {
				_tableManager.UserRoles.Remove( role );
			}

			return Task.FromResult( 0 );
		}

		public Task<IList<string>> GetRolesAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			IList<string> result = _tableManager.UserRoles.FindByPartitionKey( user.Id ).Select( item => item.Name ).ToList();

			return Task.FromResult( result );
		}

		public Task<bool> IsInRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			var role = _tableManager.UserRoles.FindByRowKey( roleName ).FirstOrDefault();

			return Task.FromResult( role != null );
		}

		#endregion

		#region IUserClaimStore

		public Task<IList<Claim>> GetClaimsAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return
				Task.FromResult(
					_tableManager.UserClaims.FindByPartitionKey( user.Id )
						.Select( item => new Claim( item.ClaimType , item.ClaimValue ) )
						.ToList() as IList<Claim> );
		}

		public Task AddClaimAsync( TUser user , Claim claim ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			_tableManager.UserClaims.AddOrUpdate(
				new IdentityUserClaim {
					UserId = user.Id ,
					ClaimType = claim.Type ,
					ClaimValue = claim.Value ,
				} );

			return Task.FromResult( 0 );
		}

		public Task RemoveClaimAsync( TUser user , Claim claim ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			if ( claim == null ) {
				throw new ArgumentNullException( "claim" );
			}

			var userClaim =
				_tableManager.UserClaims.FindBy(
					TableQuery.CombineFilters(
						TableQuery.GenerateFilterCondition( "ClaimType" , QueryComparisons.Equal , claim.Type ) ,
						TableOperators.And ,
						TableQuery.GenerateFilterCondition( "PartitionKey" , QueryComparisons.Equal , user.Id ) ) ).FirstOrDefault();

			if ( userClaim != null ) {
				_tableManager.UserClaims.Remove( userClaim );
			}

			return Task.FromResult( 0 );
		}

		#endregion

		#region IUserPasswordStore

		public Task SetPasswordHashAsync( TUser user , string passwordHash ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.PasswordHash = passwordHash;

			// TODO wird das gebraucht? await _tableManager.Users.AddOrUpdateAsync( user );

			return Task.FromResult( 0 );
		}

		public Task<string> GetPasswordHashAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.PasswordHash );
		}

		public Task<bool> HasPasswordAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( !string.IsNullOrEmpty( user.PasswordHash ) );
		}

		#endregion

		#region IUserSecurityStampStore

		public Task SetSecurityStampAsync( TUser user , string stamp ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.SecurityStamp = stamp;

			return Task.FromResult( 0 );
		}

		public Task<string> GetSecurityStampAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.SecurityStamp );
		}

		#endregion

		#region IUserEmailStore

		public Task SetEmailAsync( TUser user , string email ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.Email = email;

			return Task.FromResult( 0 );
		}

		public Task<string> GetEmailAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.Email );
		}

		public Task<bool> GetEmailConfirmedAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.EmailConfirmed );
		}

		public Task SetEmailConfirmedAsync( TUser user , bool confirmed ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.EmailConfirmed = confirmed;

			return Task.FromResult( 0 );
		}

		public Task<TUser> FindByEmailAsync( string email ) {
			return Task.FromResult( _tableManager.Users.FindByRowKey( email ).FirstOrDefault() );
		}

		#endregion

		#region IUserPhoneNumberStore

		public Task SetPhoneNumberAsync( TUser user , string phoneNumber ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.PhoneNumber = phoneNumber;

			return Task.FromResult( 0 );
		}

		public Task<string> GetPhoneNumberAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.PhoneNumber );
		}

		public Task<bool> GetPhoneNumberConfirmedAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.PhoneNumberConfirmed );
		}

		public Task SetPhoneNumberConfirmedAsync( TUser user , bool confirmed ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.PhoneNumberConfirmed = confirmed;

			return Task.FromResult( 0 );
		}

		#endregion

		#region IUserLoginStore

		public Task AddLoginAsync( TUser user , UserLoginInfo login ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			if ( login == null ) {
				throw new ArgumentNullException( "login" );
			}

			_tableManager.UserLogins.AddOrUpdate(
				new IdentityUserLogin {
					UserId = user.Id ,
					ProviderKey = login.ProviderKey ,
					LoginProvider = login.LoginProvider ,
				} );

			return Task.FromResult( 0 );
		}

		public Task RemoveLoginAsync( TUser user , UserLoginInfo login ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			if ( login == null ) {
				throw new ArgumentNullException( "login" );
			}

			_tableManager.UserLogins.Remove(
				new IdentityUserLogin {
					UserId = user.Id ,
					ProviderKey = login.ProviderKey ,
					LoginProvider = login.LoginProvider ,
				} );

			return Task.FromResult( 0 );
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return
				Task.FromResult(
					_tableManager.UserLogins.FindByPartitionKey( user.Id )
						.Select( item => new UserLoginInfo( item.LoginProvider , item.ProviderKey ) )
						.ToList() as IList<UserLoginInfo> );
		}

		public Task<TUser> FindAsync( UserLoginInfo login ) {
			if ( login == null ) {
				throw new ArgumentNullException( "login" );
			}

			var userLogin =
				_tableManager.UserLogins.FindBy(
					TableQuery.CombineFilters(
						TableQuery.GenerateFilterCondition( "LoginProvider" , QueryComparisons.Equal , login.LoginProvider ) ,
						TableOperators.And ,
						TableQuery.GenerateFilterCondition( "ProviderKey" , QueryComparisons.Equal , login.ProviderKey ) ) )
					.FirstOrDefault();

			if ( userLogin == null ) {
				return Task.FromResult<TUser>( null );
			}

			return Task.FromResult( _tableManager.Users.FindByPartitionKey( userLogin.UserId ).FirstOrDefault() );
		}

		#endregion

		#region IUserTwoFactorStore

		public Task SetTwoFactorEnabledAsync( TUser user , bool enabled ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.TwoFactorEnabled = enabled;

			return Task.FromResult( 0 );
		}

		public Task<bool> GetTwoFactorEnabledAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.TwoFactorEnabled );
		}

		#endregion

		#region IUserLockoutStore

		public Task<DateTimeOffset> GetLockoutEndDateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return
				Task.FromResult(
					user.LockoutEndDateUtc.HasValue
						? new DateTimeOffset( DateTime.SpecifyKind( user.LockoutEndDateUtc.Value , DateTimeKind.Utc ) )
						: new DateTimeOffset() );
		}

		public Task SetLockoutEndDateAsync( TUser user , DateTimeOffset lockoutEnd ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;

			return Task.FromResult( 0 );
		}

		public Task<int> IncrementAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			++user.AccessFailedCount;

			return Task.FromResult( user.AccessFailedCount );
		}

		public Task ResetAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.AccessFailedCount = 0;

			return Task.FromResult( 0 );
		}

		public Task<int> GetAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.AccessFailedCount );
		}

		public Task<bool> GetLockoutEnabledAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			return Task.FromResult( user.LockoutEnabled );
		}

		public Task SetLockoutEnabledAsync( TUser user , bool enabled ) {
			if ( user == null ) {
				throw new ArgumentNullException( "user" );
			}

			user.LockoutEnabled = enabled;

			return Task.FromResult( 0 );
		}

		#endregion
	}

}
