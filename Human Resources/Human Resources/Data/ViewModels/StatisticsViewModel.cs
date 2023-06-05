namespace Human_Resources.Data.ViewModels
{
    public  class StatisticsViewModel
    {
        public  Dictionary<string, int> BarGraphData { get; set; }
        public List<ChartData> chartDatas { get; set; }
        public List<int> PieChart { get; set; }
        public StatisticsViewModel()
        {
            BarGraphData = new Dictionary<string, int>();
            chartDatas = new List<ChartData>();
            PieChart = new List<int>() { 0,0,0};
        }

    }
}
