namespace islandmonkeyuk.Pages;

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;

public partial class FileUpload {
    public FileUpload()
    {
    }

    private readonly long maxFileSize = 50001;

    public static IEnumerable<NutritionModel> records { get; private set; }
    private bool loadingSuccess;
    private bool loadingFailure;

    public async Task LoadFile(InputFileChangeEventArgs e)
    {
        var randomFileNumber = 1;

        if (e.File.Size < maxFileSize && e.File.ContentType.Equals("text/csv")) // BUG: Will accept anything renamed as a CSV. Oh dear.
        {
            await using FileStream fs = new($"csv{randomFileNumber:D6}.csv", FileMode.Create);
            await e.File.OpenReadStream().CopyToAsync(fs);
            fs.Position = 0;

            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };

            using var reader = new StreamReader(fs);
            using var csv = new CsvReader(reader, config);
            const string expectedHeaderValues = "Date,Meal,Calories,Fat (g),Saturated Fat,Polyunsaturated Fat,Monounsaturated Fat,Trans Fat,Cholesterol,Sodium (mg),Potassium,Carbohydrates (g),Fiber,Sugar,Protein (g),Vitamin A,Vitamin C,Calcium,Iron,Note";
            await csv.ReadAsync();
            csv.ReadHeader();
            if (!csv.HeaderRecord.SequenceEqual(expectedHeaderValues.Split(",")))
            {
                var fileName = fs.Name;
                File.Delete(fileName);
                loadingFailure = true;
                await JSRuntime.InvokeVoidAsync("alert", $"{e.File.Name} does not have the expected headers, aborting.");
                Logger.LogError("File: {FileName} Error: File does not have the expected headers, aborting.", e.File.Name);
            }

            else
            {
                // Don't like having this as a list, but seems like we have to make Linq happy
                records = await csv.GetRecordsAsync<NutritionModel>().ToListAsync();

                loadingSuccess = true;
            }
        }
        else
        {
            loadingFailure = true;
            await JSRuntime.InvokeVoidAsync("alert", $"File: {e.File.Name} Error: File uploaded exceeds maximum limit, or is not a CSV file. Aborting.");
            Logger.LogError("File: {FileName} Error: File uploaded exceeds maximum limit, or is not a CSV file. Aborting.", e.File.Name);
            File.Delete(e.File.Name);
        }
    }
}
