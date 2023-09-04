namespace IU.ClimateTrace.Importer.Web.API.Models.Dtos
{
    public class PagedResultDto<T>
    {
        public required IEnumerable<T> Results { get; set; }
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
        public required long PageCount { get; set; }
        public required long TotalRecords { get; set; }
    }
}
