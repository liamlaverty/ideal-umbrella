namespace IU.ClimateTrace.Common.DataFilters
{
    /// <summary>
    /// Enum for db sorting query filters
    ///  
    /// shared between controllers and repositories
    /// </summary>
    public enum CountryEmissionsOrderByCol
    {
        /// <summary>
        /// Orders a result set by the startTime column
        /// </summary>
        startTime,
        
        /// <summary>
        /// Orders a result set by the endTime
        /// </summary>
        endTime, 
        
        /// <summary>
        /// Orders a result set by isoCountry (alphabetical)
        /// </summary>
        isoCountry,
        
        /// <summary>
        /// Orders a result set by emissions quantity
        /// 
        /// (note that emisisons quantities may be in different magnitudes)
        /// </summary>
        emissionsQuantity
    }
}
