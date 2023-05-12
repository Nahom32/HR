namespace Human_Resources.Data.ViewModels
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string SearchValue { get; set; }
        public int SortColumn { get; set; }
        public string SortDirection { get; set; }

       
    }
}
