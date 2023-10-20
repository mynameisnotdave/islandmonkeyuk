namespace islandmonkeyuk.Pages;

using Microsoft.AspNetCore.Components;
using Models;

public partial class NutritionDataView {
    private FileUpload fileUpload = new();
    private NutritionModel nutritionModel = new();
    [Parameter]
    public string SelectedTimePeriod { get; set; } = string.Empty;
    [Parameter]
    public DateOnly SelectedSingleDay { get; set; }
    [Parameter]
    public DateOnly SelectedStartDate { get; set; }
    [Parameter]
    public DateOnly SelectedEndDate { get; set; }
    [Parameter]
    public int SelectedMonth { get; set; }
    [Parameter]
    public string SelectedMeal { get; set; } = string.Empty;

    private List<int> PopulateMonthValues()
    {
        IEnumerable<int> months = from date in FileUpload.records
            group date by date.Date.Month
            into grouped
            select grouped.Key;
        return months.ToList();
    }

    private DateOnly GetEarliestDay()
    {
        IEnumerable<NutritionModel> records = FileUpload.records;
        return records.Min(record => record.Date);
    }

    private DateOnly GetLatestDay()
    {
        return FileUpload.records.Max(record => record.Date);
    }

    public bool ContainsNotes()
    {
        IEnumerable<string> notes = from c in FileUpload.records
            select c.Note;
        foreach (string note in notes)
        {
            if (note.Contains(""))
            {
                return false;
            }
            else if (note.Length > 0)
            {
                return true;
            }
        }
        return false;
    }

}
