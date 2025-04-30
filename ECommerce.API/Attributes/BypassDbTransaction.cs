namespace ECommerce.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BypassDbTransaction : Attribute
    {
    }
}