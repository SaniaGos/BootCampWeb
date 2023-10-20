using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Core.Helper
{
	public static class EnumHelper
	{
		public static string? GetDisplayName(this System.Enum enumValue)
		{
			var strValue = enumValue.ToString();
			var attr = enumValue.GetType()
								.GetMember(strValue)
								.First()
								.GetCustomAttribute<DisplayAttribute>(true);
			return attr == null ? strValue : attr.Name;
		}
	}
}
