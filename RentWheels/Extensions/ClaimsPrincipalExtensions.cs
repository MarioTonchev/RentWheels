using static RentWheels.Core.Constants.AdminRoleConstants;

namespace System.Security.Claims
{
	public static class ClaimsPrincipalExtensions
	{
		public static string Id(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		public static bool IsAdimn(this ClaimsPrincipal user)
		{
			return user.IsInRole(AdminRole);
		}
	}
}