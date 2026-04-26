using System.ComponentModel.DataAnnotations;

namespace TravelExpenseTracker_API.Data.Entities;

public class TripCategory
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Required, MaxLength(280)]
    public string Image { get; set; }

    public static TripCategory Create(int id, string name, string image) =>
        new ()
        {
            Id = id,
            Name = name,
            Image = image
        };

    public static TripCategory[] GetSeedData() =>
        [
             TripCategory.Create(1, "Beach", "category_beach.png"),
             TripCategory.Create(2, "City", "category_city.png"),
             TripCategory.Create(3, "Hills", "category_hills.png"),
             TripCategory.Create(4, "Island", "category_island.png"),
             TripCategory.Create(5, "Mountains", "category_montanha.png"),
             TripCategory.Create(6, "Religious", "category_relig.png"),
             TripCategory.Create(7, "Road Trip", "category_roadtrip.png"),
             TripCategory.Create(8, "Wildlife", "category_safari.png"),
             TripCategory.Create(9, "Other", "other.png")
            ];
}
