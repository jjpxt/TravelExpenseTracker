
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Runtime.CompilerServices;

#if ANDROID
using Android.Content.Res;
#endif

namespace TravelExpenseTracker.Controls;

public class NoUnderlinepicker : Picker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        RemoveUnderline();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName != nameof(BackgroundColor))
        {
            RemoveUnderline();
        }
    }

    private void RemoveUnderline()
    {
#if ANDROID
        if (Handler is PickerHandler pickerHandler)
        {
            if (BackgroundColor == null)
            {
                pickerHandler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
            }
            else
            {
                pickerHandler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(BackgroundColor.ToPlatform());
            }
        }
#endif
    }
}
