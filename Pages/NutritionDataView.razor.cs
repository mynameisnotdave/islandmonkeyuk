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
    public string SelectedMeal { get; set; }
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
    
        public decimal? GetCalorieValues()
    {
        var calories = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Calories;

        return calories.Sum();
    }
    public decimal? GetFatValues()
    {
        var fats = FileUpload.records.Where(c => c.Meal == SelectedMeal).Select(c => c.Fat);
        return fats.Sum();
    }

    public decimal? GetSatFatValues()
    {
        IEnumerable<decimal?> satFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.SaturatedFat;
        return satFats.Sum();
    }

    public decimal? GetMonoUnsatFatValues()
    {
        IEnumerable<decimal?> monoUnsatFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.MonounsaturatedFat;
        return monoUnsatFats.Sum();
    }

    public decimal? GetPolyFatValues()
    {
        IEnumerable<decimal?> polyFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.PolyunsaturatedFat;
        return polyFats.Sum();
    }

    public decimal? GetTransFatValues()
    {
        IEnumerable<decimal?> transFats = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.TransFat;
        return transFats.Sum();
    }

    public decimal? GetCarbValues()
    {
        IEnumerable<decimal?> carbs = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Carbohydrates;
        return carbs.Sum();
    }

    public decimal? GetSugarValues()
    {
        IEnumerable<decimal?> sugars = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Sugar;
        return sugars.Sum();
    }

    public decimal? GetFibreValues()
    {
        IEnumerable<decimal?> fibre = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Fiber;
        return fibre.Sum();
    }

    public decimal? GetProteinValues()
    {
        IEnumerable<decimal?> protein = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Protein;
        return protein.Sum();
    }

    public decimal? GetCholesterolValues()
    {
        IEnumerable<decimal?> cholesterol = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Cholesterol;
        return cholesterol.Sum();
    }

    public decimal? GetSaltValues()
    {
        IEnumerable<decimal?> salts = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Sodium;
        return salts.Sum();
    }

    public decimal? GetPotassiumValues()
    {
        IEnumerable<decimal?> potassium = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Potassium;
        return potassium.Sum();
    }

    public decimal? GetVitA_Values()
    {
        IEnumerable<decimal?> vitAs = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.VitaminA;
        return vitAs.Sum();
    }

    public decimal? GetVitC_Values()
    {
        IEnumerable<decimal?> vitC = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.VitaminC;
        return vitC.Sum();
    }

    public decimal? GetCalciumValues()
    {
        IEnumerable<decimal?> calcium = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Calcium;
        return calcium.Sum();
    }

    public decimal? GetIronValues()
    {
        IEnumerable<decimal?> iron = from c in FileUpload.records
            where c.Meal == SelectedMeal
            select c.Iron;
        return iron.Sum();
    }


}
