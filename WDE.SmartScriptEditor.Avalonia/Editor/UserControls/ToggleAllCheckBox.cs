using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;

namespace WDE.SmartScriptEditor.Avalonia.Editor.UserControls;

public class ToggleAllCheckBox : CheckBox, IStyleable
{
    private ICommand? _altCommand;
    public static readonly DirectProperty<ToggleAllCheckBox, ICommand?> AltCommandProperty =
        AvaloniaProperty.RegisterDirect<ToggleAllCheckBox, ICommand?>(nameof(AltCommand),
            button => button.AltCommand, (checkBox, command) => checkBox.AltCommand = command);

    
    /// <summary>
    /// Gets or sets an <see cref="ICommand"/> to be invoked when the checkbox is clicked with any modifier key.
    /// </summary>
    public ICommand? AltCommand
    {
        get => _altCommand;
        set => SetAndRaise(CommandProperty, ref _altCommand, value);
    }
    
    public Type StyleKey => typeof(CheckBox);

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        if (IsPressed && e.InitialPressMouseButton == MouseButton.Left &&
            e.KeyModifiers != KeyModifiers.None)
        {
            SetValue(IsPressedProperty, false);
            e.Handled = true;
            OnClickAlternative();
        }
        else
            base.OnPointerReleased(e);
    }

    private void OnClickAlternative()
    {
        AltCommand?.Execute(IsChecked);
    }
}