using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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

    /// <summary>
    /// Focuses the element (keyboard focus)
    /// </summary>
    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If there is no control, return
            if (!(sender is Control control))
                return;

            if ((bool)e.NewValue)
                control.Focus();
        }
    }

    /// <summary>
    /// Focuses the element (keyboard focus) and selects all text within the element if true
    /// </summary>
    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // If there is no control, return
            if (!(sender is TextBoxBase control))
                return;

            if ((bool)e.NewValue)
            {
                control.Focus();
                control.SelectAll();
            }
        }
    }
}
