namespace MyBeerTap.ApiServices.Security
{
    /// <summary>
    /// Describes client. Used to create SecurityUser instance
    /// </summary>
    public sealed class ApiClient
    {
        //TODO: Add custom ApiClient fields
        public string UserName { get; set; }
        public string Environment { get; set; }
    }
}