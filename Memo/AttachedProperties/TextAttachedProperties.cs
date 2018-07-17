using System.Windows;
using System.Windows.Controls;

namespace Memo
{
    /// <summary>
    /// Focuses the element on load (keyboard focus)
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If there is no control, return
            if (!(sender is Control control))
                return;

            // Focuses the control once loaded
            control.Loaded += (s, se) => control.Focus();
        }
    }
}
