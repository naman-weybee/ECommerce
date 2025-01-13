namespace ECommerce.Application.Interfaces
{
    public interface IMD5Service
    {
        string ComputeMD5Hash(string input);
    }
}