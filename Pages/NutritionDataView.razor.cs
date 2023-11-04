namespace islandmonkeyuk.Pages;

using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;

public partial class NutritionDataView {
    private FileUpload fileUpload = new();
    private NutritionModel nutritionModel = new();

    [Parameter]
    public string SelectedTimePeriod { get; set; } = string.Empty;

    [Parameter]
    public DateOnly SelectedSingleDay { get; set; }

    [Parameter]
    public static DateOnly SelectedStartDate { get; set; }

    [Parameter]
    public static DateOnly SelectedEndDate { get; set; }

    [Parameter]
    public int SelectedMonth { get; set; }

    [Parameter]
    public string SelectedMeal { get; set; }
    
    [Parameter]
    [EditorRequired]
    public DateOnly Date { get; set; }
    
    [Parameter]
    public static EventCallback<DateOnly> DateChanged { get; set; }

    [Parameter]
    [EditorRequired]
    public string? Label { get; set; }

    private DateRange dateRange = new DateRange(selectedStartDate, selectedEndDate);

    private DateTime? selectedSingleDay
    {
        get => SelectedSingleDay.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value is not null)
            {
                SelectedSingleDay = DateOnly.FromDateTime((DateTime)value);
                DateChanged.InvokeAsync(SelectedSingleDay);
            }
        }
    }
    
    private static DateTime? selectedStartDate
    {
        get => SelectedStartDate.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value is not null)
            {
                SelectedStartDate = DateOnly.FromDateTime((DateTime)value);
                DateChanged.InvokeAsync(SelectedStartDate);
            }
        }
    }
    
    private static DateTime? selectedEndDate
    {
        get => SelectedEndDate.ToDateTime(TimeOnly.MinValue);
        set
        {
            if (value is not null)
            {
                SelectedEndDate = DateOnly.FromDateTime((DateTime)value);
                DateChanged.InvokeAsync(SelectedEndDate);
            }
        }
    }
    private void SelectedMealChanged(ChangeEventArgs e)
    {
        if (e.Value is not null)
        {
            SelectedMeal = (string)e.Value;
        }
    }
    
    private void SelectedDateChanged(ChangeEventArgs e)
    {
        if (e.Value is not null)
        {
          SelectedTimePeriod = (string)e.Value;  
        }
        
    }
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

    private decimal? GetCalorieValues()
    {
        if (SelectedMeal == "All meals" && SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> calsSingleDayAllMeals = from c in FileUpload.records
                where c.Date == SelectedSingleDay
                select c.Calories;
            return calsSingleDayAllMeals.Sum();
        }
        else if (SelectedMeal != "All meals" && SelectedTimePeriod == "date-range")
        {
            var calsDateRangeAllMeals = from c in FileUpload.records
                where c.Meal == SelectedMeal && c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Calories;
            return calsDateRangeAllMeals.Sum();
        }

    }
    private decimal? GetFatValues()
    {
        var fats = FileUpload.records.Where(c => c.Meal == SelectedMeal).Select(c => c.Fat);
        return fats.Sum();
    }

    private decimal? GetSatFatValues()
    {
        IEnumerable<decimal?> satFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.SaturatedFat;
        return satFats.Sum();
    }

    private decimal? GetMonoUnsatFatValues()
    {
        IEnumerable<decimal?> monoUnsatFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.MonounsaturatedFat;
        return monoUnsatFats.Sum();
    }

    private decimal? GetPolyFatValues()
    {
        IEnumerable<decimal?> polyFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.PolyunsaturatedFat;
        return polyFats.Sum();
    }

    private decimal? GetTransFatValues()
    {
        IEnumerable<decimal?> transFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.TransFat;
        return transFats.Sum();
    }

    private decimal? GetCarbValues()
    {
        IEnumerable<decimal?> carbs = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Carbohydrates;
        return carbs.Sum();
    }

    private decimal? GetSugarValues()
    {
        IEnumerable<decimal?> sugars = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Sugar;
        return sugars.Sum();
    }

    private decimal? GetFibreValues()
    {
        IEnumerable<decimal?> fibre = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Fiber;
        return fibre.Sum();
    }

    private decimal? GetProteinValues()
    {
        IEnumerable<decimal?> protein = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Protein;
        return protein.Sum();
    }

    private decimal? GetCholesterolValues()
    {
        IEnumerable<decimal?> cholesterol = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Cholesterol;
        return cholesterol.Sum();
    }

    private decimal? GetSaltValues()
    {
        IEnumerable<decimal?> salts = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Sodium;
        return salts.Sum();
    }

    private decimal? GetPotassiumValues()
    {
        IEnumerable<decimal?> potassium = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Potassium;
        return potassium.Sum();
    }

    private decimal? GetVitA_Values()
    {
        IEnumerable<decimal?> vitAs = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.VitaminA;
        return vitAs.Sum();
    }

    private decimal? GetVitC_Values()
    {
        IEnumerable<decimal?> vitC = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.VitaminC;
        return vitC.Sum();
    }

    private decimal? GetCalciumValues()
    {
        IEnumerable<decimal?> calcium = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Calcium;
        return calcium.Sum();
    }

    private decimal? GetIronValues()
    {
        IEnumerable<decimal?> iron = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Iron;
        return iron.Sum();
    }


}
