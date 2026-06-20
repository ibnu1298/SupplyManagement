using System.ComponentModel.DataAnnotations;

namespace SupplyManagement.Web.Helper
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DisplayAttribute), false)
                            as DisplayAttribute[];

            return attr?.FirstOrDefault()?.Name ?? value.ToString();
        }
    }
}
