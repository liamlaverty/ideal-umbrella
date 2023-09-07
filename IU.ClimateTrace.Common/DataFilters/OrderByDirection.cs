namespace IU.ClimateTrace.Common.DataFilters
{

    /// <summary>
    /// Enum for db query filters sorting 
    ///  
    /// shared between controllers and repositories
    /// </summary>
    public enum OrderByDirection
    {
        /// <summary>
        /// Sorts a query ascending
        /// </summary>
        ASC,
        /// <summary>
        /// Sorts a query descending
        /// </summary>
        DESC
    }
}
