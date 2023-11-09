namespace islandmonkeyuk.Tests;

using FluentAssertions;
using Pages;

public class Tests {
    
    NutritionDataView nutritionDataView;
    
    [SetUp]
    public void Setup()
    {
        nutritionDataView = new();
    }
    

    [Test]
    public void GetCalorieValues_WhenCalled_DoesNotReturnNull()
    {
        nutritionDataView.GetCalorieValues().Should().NotBeNull();
    }
    
    [Test]
    public void GetFatValues_WhenCalled_DoesNotReturnNull()
    {
        nutritionDataView.GetFatValues().Should().NotBeNull();
    }
    
    [Test]
    public void GetSatFatValues_WhenCalled_DoesNotReturnNull()
    {
        nutritionDataView.GetSatFatValues().Should().NotBeNull();
    }
    
    [Test]
    public void GetMonoUnsatFatValues_WhenCalled_DoesNotReturnNull()
    {
        nutritionDataView.GetMonoUnsatFatValues().Should().NotBeNull();
    }
    
    [Test]
    public void GetPolyFatValues_WhenCalled_DoesNotReturnNull()
    {
        nutritionDataView.GetPolyFatValues().Should().NotBeNull();
    }
    
    
}
