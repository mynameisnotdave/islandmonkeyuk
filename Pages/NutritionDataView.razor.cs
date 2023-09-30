namespace islandmonkeyuk.Pages;

using Models;

public partial class NutritionDataView {
    private FileUpload fileUpload = new();
    private NutritionModel nutritionModel = new();
    private string selectedTimePeriod { get; set; } = string.Empty;
    private DateOnly selectedSingleDay { get; set; }
    private DateOnly selectedStartDate { get; set; }
    private DateOnly selectedEndDate { get; set; }
    private int selectedMonth { get; set; }
    private string selectedMeal { get; set; } = string.Empty;

    private List<int> PopulateMonthValues()
    {
        var months = from date in FileUpload.records
            group date by date.Date.Month
            into grouped
            select grouped.Key;
        return months.ToList();
    }

    private string GetEarliestDay()
    {
        var records = FileUpload.records;
        string earliestDay;
        return earliestDay = records.Min(record => record.Date).ToString();
    }

    private string GetLatestDay()
    {
        string latestDay;
        return latestDay = FileUpload.records.Max(record => record.Date).ToString();
    }

}
