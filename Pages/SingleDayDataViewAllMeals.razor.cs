namespace islandmonkeyuk.Pages; 

using System.Linq;

public partial class SingleDayDataViewAllMeals : IGenerateNutritionalValues 
{
    private NutritionDataView dataView = new();
    // So if we did have a single component for all, then we would have to find a way to cleanly switch between different meal types and different date types and display the content based
    // upon that. That will be tough.


    public decimal? GetCalorieValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetFatValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetSatFatValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetMonoUnsatFatValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetPolyFatValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetTransFatValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetCarbValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetSugarValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetFibreValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetProteinValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetCholesterolValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetSaltValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetPotassiumValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetVitA_Values()
    {
        throw new NotImplementedException();
    }
    public decimal? GetVitC_Values()
    {
        throw new NotImplementedException();
    }
    public decimal? GetCalciumValues()
    {
        throw new NotImplementedException();
    }
    public decimal? GetIronValues()
    {
        throw new NotImplementedException();
    }
}
