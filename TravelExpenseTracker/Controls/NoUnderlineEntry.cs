using Android.Content.Res;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Runtime.CompilerServices;

namespace TravelExpenseTracker.Controls;

public class NoUnderlineEntry : Entry
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        RemoveUnderline();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if(propertyName != nameof(BackgroundColor))
        {
            RemoveUnderline();
        }
    }

    private void RemoveUnderline()
    {
#if ANDROID
        if (Handler is IEntryHandler entryHandler)
        {
            if (BackgroundColor == null)
            {
                entryHandler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
            }
            else
            {
                entryHandler.PlatformView.BackgroundTintList =
                    ColorStateList.ValueOf(BackgroundColor.ToPlatform());
            }
        }
#endif
    }
}
