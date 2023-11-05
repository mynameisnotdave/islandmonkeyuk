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
        IEnumerable<int> months = from date in FileUpload.Records
            group date by date.Date.Month
            into grouped
            select grouped.Key;
        return months.ToList();
    }

    private DateTime GetEarliestDay()
    {
        return FileUpload.Records.Min(record => record.Date);
    }

    private DateTime GetLatestDay()
    {
        return FileUpload.Records.Max(record => record.Date);
    }

    public bool ContainsNotes()
    {
        IEnumerable<string> notes = from c in FileUpload.Records
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
        // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> calsSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Calories;
            return calsSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> calsSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Calories;
            return calsSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> calsDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Calories;
            return calsDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> calsDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Calories;
            return calsDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> calsMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Calories;
            return calsMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> calsMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Calories;
            return calsMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> calsEternityAllMeals = from c in FileUpload.Records
                select c.Calories;
            return calsEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> calsEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Calories;
            return calsEternityAllMeals.Sum();
        }
        return 6006135;
    }
    private decimal? GetFatValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> fatSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Fat;
            return fatSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> fatSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Fat;
            return fatSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> fatDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Fat;
            return fatDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> fatDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Fat;
            return fatDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> fatMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Fat;
            return fatMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> fatMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Fat;
            return fatMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> fatEternityAllMeals = from c in FileUpload.Records
                select c.Fat;
            return fatEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> fatEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Fat;
            return fatEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetSatFatValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> satFatSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.SaturatedFat;
            return satFatSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> satFatSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.SaturatedFat;
            return satFatSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> satFatDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.SaturatedFat;
            return satFatDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> satFatDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.SaturatedFat;
            return satFatDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> satFatMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.SaturatedFat;
            return satFatMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> satFatMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.SaturatedFat;
            return satFatMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> satFatEternityAllMeals = from c in FileUpload.Records
                select c.SaturatedFat;
            return satFatEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> satFatEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.SaturatedFat;
            return satFatEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetMonoUnsatFatValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> monoSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.MonounsaturatedFat;
            return monoSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> monoSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.MonounsaturatedFat;
            return monoSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> monoDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.MonounsaturatedFat;
            return monoDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> monoDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.MonounsaturatedFat;
            return monoDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> monoMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.MonounsaturatedFat;
            return monoMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> monoMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.MonounsaturatedFat;
            return monoMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> monoEternityAllMeals = from c in FileUpload.Records
                select c.MonounsaturatedFat;
            return monoEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> monoEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.MonounsaturatedFat;
            return monoEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetPolyFatValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> polySingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.PolyunsaturatedFat;
            return polySingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> polySingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.PolyunsaturatedFat;
            return polySingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> polyDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.PolyunsaturatedFat;
            return polyDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> polyDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.PolyunsaturatedFat;
            return polyDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> polyMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.PolyunsaturatedFat;
            return polyMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> polyMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.PolyunsaturatedFat;
            return polyMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> polyEternityAllMeals = from c in FileUpload.Records
                select c.PolyunsaturatedFat;
            return polyEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> polyEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.PolyunsaturatedFat;
            return polyEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetTransFatValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> transSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.TransFat;
            return transSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> transSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.TransFat;
            return transSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> transDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.TransFat;
            return transDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> transDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.TransFat;
            return transDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> transMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.TransFat;
            return transMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> transMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.TransFat;
            return transMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> transEternityAllMeals = from c in FileUpload.Records
                select c.TransFat;
            return transEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> transEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.TransFat;
            return transEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetCarbValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> carbsSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Carbohydrates;
            return carbsSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> carbsSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Carbohydrates;
            return carbsSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> carbsDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Carbohydrates;
            return carbsDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> carbsDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Carbohydrates;
            return carbsDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> carbsMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Carbohydrates;
            return carbsMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> carbsMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Carbohydrates;
            return carbsMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> carbsEternityAllMeals = from c in FileUpload.Records
                select c.Carbohydrates;
            return carbsEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> carbsEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Carbohydrates;
            return carbsEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetSugarValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> sugarSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Sugar;
            return sugarSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> sugarSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Sugar;
            return sugarSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> sugarDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Sugar;
            return sugarDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> sugarDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Sugar;
            return sugarDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> sugarMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Sugar;
            return sugarMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> sugarMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Sugar;
            return sugarMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> sugarEternityAllMeals = from c in FileUpload.Records
                select c.Sugar;
            return sugarEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> sugarEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Sugar;
            return sugarEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetFibreValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> fibreSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Fiber;
            return fibreSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> fibreSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Fiber;
            return fibreSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> fibreDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Fiber;
            return fibreDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> fibreDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Fiber;
            return fibreDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> fibreMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Fiber;
            return fibreMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> fibreMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Fiber;
            return fibreMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> fibreEternityAllMeals = from c in FileUpload.Records
                select c.Fiber;
            return fibreEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> fibreEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Fiber;
            return fibreEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetProteinValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> proteinSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Protein;
            return proteinSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> proteinSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Protein;
            return proteinSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> proteinDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Protein;
            return proteinDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> proteinDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Protein;
            return proteinDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> proteinMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Protein;
            return proteinMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> proteinMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Protein;
            return proteinMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> proteinEternityAllMeals = from c in FileUpload.Records
                select c.Protein;
            return proteinEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> proteinEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Protein;
            return proteinEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetCholesterolValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> cholSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Cholesterol;
            return cholSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> cholSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Cholesterol;
            return cholSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> cholDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Cholesterol;
            return cholDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> cholDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Cholesterol;
            return cholDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> cholMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Cholesterol;
            return cholMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> cholMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Cholesterol;
            return cholMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> cholEternityAllMeals = from c in FileUpload.Records
                select c.Cholesterol;
            return cholEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> cholEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Cholesterol;
            return cholEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetSaltValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> saltSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Sodium;
            return saltSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> saltSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Sodium;
            return saltSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> saltDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Sodium;
            return saltDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> saltDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Sodium;
            return saltDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> saltMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Sodium;
            return saltMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> saltMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Sodium;
            return saltMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> saltEternityAllMeals = from c in FileUpload.Records
                select c.Sodium;
            return saltEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> saltEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Sodium;
            return saltEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetPotassiumValues()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> potSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Potassium;
            return potSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> potSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Potassium;
            return potSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> potDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Potassium;
            return potDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> potDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Potassium;
            return potDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> potMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Potassium;
            return potMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> potMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Potassium;
            return potMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> potEternityAllMeals = from c in FileUpload.Records
                select c.Potassium;
            return potEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> potEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Potassium;
            return potEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetVitA_Values()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> vitASingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.VitaminA;
            return vitASingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> vitASingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.VitaminA;
            return vitASingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> vitADateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.VitaminA;
            return vitADateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> vitADateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.VitaminA;
            return vitADateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> vitAMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.VitaminA;
            return vitAMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> vitAMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.VitaminA;
            return vitAMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> vitAEternityAllMeals = from c in FileUpload.Records
                select c.VitaminA;
            return vitAEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> vitAEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.VitaminA;
            return vitAEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetVitC_Values()
    {
    // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> vitCSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.VitaminC;
            return vitCSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> vitCSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.VitaminC;
            return vitCSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> vitCDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.VitaminC;
            return vitCDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> vitCDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.VitaminC;
            return vitCDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> vitCMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.VitaminC;
            return vitCMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> vitCMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.VitaminC;
            return vitCMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> vitCEternityAllMeals = from c in FileUpload.Records
                select c.VitaminC;
            return vitCEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> vitCEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.VitaminC;
            return vitCEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetCalciumValues()
    {
        // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> calciumSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Calcium;
            return calciumSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> calciumSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Calcium;
            return calciumSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> calciumDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Calcium;
            return calciumDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> calciumDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Calcium;
            return calciumDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> calciumMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Calcium;
            return calciumMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> calciumMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Calcium;
            return calciumMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> calciumEternityAllMeals = from c in FileUpload.Records
                select c.Calcium;
            return calciumEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> calciumEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Calcium;
            return calciumEternityAllMeals.Sum();
        }
        return 6006135;
    }

    private decimal? GetIronValues()
    {
            // I know nesting is bad but this could be considered
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> ironSingleDayAllMeals = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Iron;
            return ironSingleDayAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "single-day")
        {
            IEnumerable<decimal?> ironSingleDay = from c in FileUpload.Records
                where c.Date == selectedSingleDay
                select c.Iron;
            return ironSingleDay.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> ironDateRangeAllMeals = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End
                select c.Iron;
            return ironDateRangeAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "date-range")
        {
            IEnumerable<decimal?> ironDateRange = from c in FileUpload.Records
                where c.Date >= dateRange.Start && c.Date <= dateRange.End && c.Meal == SelectedMeal
                select c.Iron;
            return ironDateRange.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> ironMonthAllMeals = from c in FileUpload.Records
                where c.Date.Month == SelectedMonth
                select c.Iron;
            return ironMonthAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "month")
        {
            IEnumerable<decimal?> ironMonth = from c in FileUpload.Records
                where c.Meal == SelectedMeal && c.Date.Month == SelectedMonth
                select c.Iron;
            return ironMonth.Sum();
        }
        if (SelectedMeal == "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> ironEternityAllMeals = from c in FileUpload.Records
                select c.Iron;
            return ironEternityAllMeals.Sum();
        }
        if (SelectedMeal != "All meals" & SelectedTimePeriod == "eternity")
        {
            IEnumerable<decimal?> ironEternityAllMeals = from c in FileUpload.Records
                where c.Meal == SelectedMeal
                select c.Iron;
            return ironEternityAllMeals.Sum();
        }
        return 6006135;
    }


}
