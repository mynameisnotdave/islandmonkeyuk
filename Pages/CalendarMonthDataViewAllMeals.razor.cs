namespace islandmonkeyuk.Pages; 

public partial class CalendarMonthDataViewAllMeals : IGenerateNutritionalValues {
        private NutritionDataView dataView = new();


    public decimal? GetCalorieValues()
    {
        var calories = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Calories;

        return calories.Sum();
    }
    public decimal? GetFatValues()
    {
        var fats = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Fat;
        return fats.Sum();
    }

    public decimal? GetSatFatValues()
    {
        var satFats = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.SaturatedFat;
        return satFats.Sum();
    }

    public decimal? GetMonoUnsatFatValues()
    {
        var monoUnsatFats = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.MonounsaturatedFat;
        return monoUnsatFats.Sum();
    }

    public decimal? GetPolyFatValues()
    {
        var polyFats = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.PolyunsaturatedFat;
        return polyFats.Sum();
    }

    public decimal? GetTransFatValues()
    {
        var transFats = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.TransFat;
        return transFats.Sum();
    }

    public decimal? GetCarbValues()
    {
        var carbs = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Carbohydrates;
        return carbs.Sum();
    }

    public decimal? GetSugarValues()
    {
        var sugars = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Sugar;
        return sugars.Sum();
    }

    public decimal? GetFibreValues()
    {
        var fibre = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Fiber;
        return fibre.Sum();
    }

    public decimal? GetProteinValues()
    {
        var protein = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Protein;
        return protein.Sum();
    }

    public decimal? GetCholesterolValues()
    {
        var cholesterol = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Cholesterol;
        return cholesterol.Sum();
    }

    public decimal? GetSaltValues()
    {
        var salts = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Sodium;
        return salts.Sum();
    }

    public decimal? GetPotassiumValues()
    {
        var potassium = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Potassium;
        return potassium.Sum();
    }

    public decimal? GetVitA_Values()
    {
        var vitAs = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.VitaminA;
        return vitAs.Sum();
    }

    public decimal? GetVitC_Values()
    {
        var vitC = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.VitaminC;
        return vitC.Sum();
    }

    public decimal? GetCalciumValues()
    {
        var calcium = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Calcium;
        return calcium.Sum();
    }

    public decimal? GetIronValues()
    {
        var iron = from c in FileUpload.records
            where c.Date.Month == dataView.SelectedMonth
            select c.Iron;
        return iron.Sum();
    }
}
