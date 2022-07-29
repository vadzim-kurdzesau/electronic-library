namespace ElectronicLibrary.Web.Parameters
{
    /// <summary>
    /// Contains both book and pagination parameters.
    /// </summary>
    public class BookParameters : PaginationParameters
    {
        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }
    }
}
