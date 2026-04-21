using TravelExpenseTracker.Models;

namespace TravelExpenseTracker.Services;

public class CategoryService
{
    public static CategoryModel[] Categories { get; private set; } = [];

    static CategoryService()
    {
        Categories = [
             new CategoryModel(1, "Beach", "category_beach.png"),
             new CategoryModel(2, "City", "category_city.png"),
             new CategoryModel(3, "Hills", "category_hills.png"),
             new CategoryModel(4, "Island", "category_island.png"),
             new CategoryModel(5, "Mountains", "category_montanha.png"),
             new CategoryModel(6, "Religious", "category_relig.png"),
             new CategoryModel(7, "Road Trip", "category_roadtrip.png"),
             new CategoryModel(8, "Wildlife", "category_safari.png"),
             new CategoryModel(9, "Other", "other.png")
            ];
    }

}
