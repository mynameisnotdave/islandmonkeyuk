using CsvHelper.Configuration.Attributes;

namespace islandmonkeyuk.Models;

public class NutritionModel {
    [Name("Date")] public DateTime Date { get; set; } = new DateTime().Date;

    [Name("Meal")] public string Meal { get; set; } = string.Empty;

    [Name("Calories")] public decimal? Calories { get; set; }

    [Name("Fat (g)")] public decimal? Fat { get; set; }

    [Name("Saturated Fat")] public decimal? SaturatedFat { get; set; }

    [Name("Polyunsaturated Fat")] public decimal? PolyunsaturatedFat { get; set; }

    [Name("Monounsaturated Fat")] public decimal? MonounsaturatedFat { get; set; }

    [Name("Trans Fat")] public decimal? TransFat { get; set; }

    [Name("Cholesterol")] public decimal? Cholesterol { get; set; }

    [Name("Sodium (mg)")] public decimal? Sodium { get; set; }

    [Name("Potassium")] public decimal? Potassium { get; set; }

    [Name("Carbohydrates (g)")] public decimal? Carbohydrates { get; set; }

    [Name("Fiber")] public decimal? Fiber { get; set; }

    [Name("Sugar")] public decimal? Sugar { get; set; }

    [Name("Protein (g)")] public decimal? Protein { get; set; }

    [Name("Vitamin A")] public decimal? VitaminA { get; set; }

    [Name("Vitamin C")] public decimal? VitaminC { get; set; }

    [Name("Calcium")] public decimal? Calcium { get; set; }

    [Name("Iron")] public decimal? Iron { get; set; }

    [Name("Note")] public string Note { get; set; } = string.Empty;
}