using CommunityToolkit.Mvvm.ComponentModel;
using TravelExpenseTracker.Shared.Dtos;

namespace TravelExpenseTracker.Models;

public partial class SaveExpenseModel : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private DateTime _spentOn = DateTime.Now;

    [ObservableProperty]
    private int _categoryId;

    [ObservableProperty]
    private string _selectedCategoryName;

    public (bool IsValid, string? Error) Validate()
    {
        string? error = null;

        if (CategoryId == 0)
            error = "Category selection is required";

        else if(Amount == 0)
            error = "Amount is required";

        else if (string.IsNullOrWhiteSpace(Title)
            || SpentOn == default)
            error = "All fields required";

        return (error == null, error);
    }

    public ExpenseDto ToDto() =>
        new ExpenseDto
        {
            CategoryId = CategoryId,
            Id = Id,
            Title = Title,
            Amount = Amount,
            SpentOn = SpentOn
        };
}
