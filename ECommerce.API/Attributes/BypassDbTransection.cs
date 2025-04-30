namespace ECommerce.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BypassDbTransection : Attribute
    {
    }
}