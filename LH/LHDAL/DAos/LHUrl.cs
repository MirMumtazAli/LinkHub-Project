namespace LHDAL.DAos
{
    public class LHUrl
    {
        public int UrlId { get; set; }
        public string? UrlTitle { get; set; }
        public string? LHUrlName { get; set; }
        public string? Description { get;set; }

        public bool? IsApproved { get; set; }

        public int? CategoryId { get; set; }
        public string? Id { get; set; }

        public Category? Category { get; set; }

        public LHUser? User { get; set; }

    }
}