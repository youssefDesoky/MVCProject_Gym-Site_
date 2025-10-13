using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Data.Contexts;

public static class GymDataSeeding
{
    public static bool SeedData(GymContext context)
    {
        try
        {
            if (!context.Categories.Any())
            {
                var categories = LoadDataFromJsonFile<Category>("categories.json");

                context.Categories.AddRange(categories);
            }

            if (!context.Plans.Any())
            {
                var plans = LoadDataFromJsonFile<Plan>("plans.json");

                context.Plans.AddRange(plans);
            }

            return context.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static List<T> LoadDataFromJsonFile<T>(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException();

        var jsonData = File.ReadAllText(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<List<T>>(jsonData, options) ?? new List<T>();
    }
}
