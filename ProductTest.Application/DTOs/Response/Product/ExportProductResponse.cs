namespace ProductTest.Application.DTOs.Response.Product
{
    public class ExportProductResponse
    {
        /// <summary>
        /// The name of the downloaded file (including extension, e.g. "products.xlsx").
        /// </summary>
        public string FilePath { get; set; }
    }
}