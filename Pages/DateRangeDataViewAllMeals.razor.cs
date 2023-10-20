namespace islandmonkeyuk.Pages; 

public partial class DateRangeDataViewAllMeals : IGenerateNutritionalValues {
    private NutritionDataView dataView = new();


    public decimal? GetCalorieValues()
    {
        IEnumerable<decimal?> calories = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Calories;

        return calories.Sum();
    }
    public decimal? GetFatValues()
    {
        IEnumerable<decimal?> fats = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Fat;
        return fats.Sum();
    }

    public decimal? GetSatFatValues()
    {
        IEnumerable<decimal?> satFats = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.SaturatedFat;
        return satFats.Sum();
    }

    public decimal? GetMonoUnsatFatValues()
    {
        IEnumerable<decimal?> monoUnsatFats = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.MonounsaturatedFat;
        return monoUnsatFats.Sum();
    }

    public decimal? GetPolyFatValues()
    {
        IEnumerable<decimal?> polyFats = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.PolyunsaturatedFat;
        return polyFats.Sum();
    }

    public decimal? GetTransFatValues()
    {
        IEnumerable<decimal?> transFats = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.TransFat;
        return transFats.Sum();
    }

    public decimal? GetCarbValues()
    {
        IEnumerable<decimal?> carbs = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Carbohydrates;
        return carbs.Sum();
    }

    public decimal? GetSugarValues()
    {
        IEnumerable<decimal?> sugars = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Sugar;
        return sugars.Sum();
    }

    public decimal? GetFibreValues()
    {
        IEnumerable<decimal?> fibre = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Fiber;
        return fibre.Sum();
    }

    public decimal? GetProteinValues()
    {
        IEnumerable<decimal?> protein = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Protein;
        return protein.Sum();
    }

    public decimal? GetCholesterolValues()
    {
        IEnumerable<decimal?> cholesterol = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Cholesterol;
        return cholesterol.Sum();
    }

    public decimal? GetSaltValues()
    {
        IEnumerable<decimal?> salts = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Sodium;
        return salts.Sum();
    }

    public decimal? GetPotassiumValues()
    {
        IEnumerable<decimal?> potassium = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Potassium;
        return potassium.Sum();
    }

    public decimal? GetVitA_Values()
    {
        IEnumerable<decimal?> vitAs = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.VitaminA;
        return vitAs.Sum();
    }

    public decimal? GetVitC_Values()
    {
        IEnumerable<decimal?> vitC = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.VitaminC;
        return vitC.Sum();
    }

    public decimal? GetCalciumValues()
    {
        IEnumerable<decimal?> calcium = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Calcium;
        return calcium.Sum();
    }

    public decimal? GetIronValues()
    {
        IEnumerable<decimal?> iron = from c in FileUpload.records
            where c.Date >= dataView.SelectedStartDate & c.Date <= dataView.SelectedEndDate & c.Meal == dataView.SelectedMeal
            select c.Iron;
        return iron.Sum();
    }
}
