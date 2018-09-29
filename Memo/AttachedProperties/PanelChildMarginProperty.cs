using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Memo
{
    /// <summary>
    /// The NoFrameHistory attached property for creating a <see cref="Frame"/> that never shows navigation UI properties
    /// and keeps the navigation history empty
    /// </summary>
    public class PanelChildMarginProperty : BaseAttachedProperty<PanelChildMarginProperty, string>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get panel (typically a grid)
            var panel = (sender as Panel);

            // Wait for panel to load
            panel.Loaded += (s, ee) =>
            {
                // Loop through each child
                foreach (var child in panel.Children)
                {
                    // Set its margin to the given value
                    (child as FrameworkElement).Margin = (Thickness)(new ThicknessConverter().ConvertFromString(e.NewValue as string));
                }
            };
        }
    }
}
