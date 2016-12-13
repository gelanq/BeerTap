namespace MyBeerTap.ApiServices
{
    /// <summary>
    /// The class is used to pass additional parameters to hypermedia links definitions in resource specifications.
    /// </summary>
    public class LinksParametersSource
    {
        public LinksParametersSource(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; private set; }
    }
}