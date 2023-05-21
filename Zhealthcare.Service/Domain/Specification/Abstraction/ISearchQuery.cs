namespace Exxat.SSOConfiguration.Domain.Specification.Abstraction
{
    public interface ISearchQuery
    {
        int Start { get; set; }
        int PageSize { get; set; }
    }
}