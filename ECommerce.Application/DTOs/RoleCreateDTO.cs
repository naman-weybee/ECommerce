namespace ECommerce.Application.DTOs
{
    public class RoleCreateDTO
    {
        public string Name { get; set; }

        public string EntityName { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }
    }
}