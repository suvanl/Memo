using System;
using System.Windows;
using System.Windows.Controls;

namespace Memo
{
    /// <summary>
    /// Matches the label width of all text entry controls within this panel
    /// </summary>
    public class TextEntryWidthMatcherProperty : BaseAttachedProperty<TextEntryWidthMatcherProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get panel (typically a grid)
            var panel = (sender as Panel);

            // Call SetWidths (also allows the designer to work properly with this)
            SetWidths(panel);

            // Wait for panel to load
            RoutedEventHandler onLoaded = null;
            onLoaded = (s, ee) =>
            {
                // Unhook
                panel.Loaded -= onLoaded;

                // Set widths
                SetWidths(panel);

                // Loop through each child
                foreach (var child in panel.Children)
                {
                    // Ignore anything that isn't a TextEntryControl
                    if (!(child is TextEntryControl control))
                        continue;

                    // Set its width to the given value
                    control.Label.SizeChanged += (ss, eee) =>
                    {
                        // Update widths
                        SetWidths(panel);
                    };
                }
            };

            // Hook into the Loaded event
            panel.Loaded += onLoaded;
        }

        /// <summary>
        /// Updates all child TextEntryControl instances so their widths match the greatest width in the group
        /// </summary>
        /// <param name="panel">The panel containing the <see cref="TextEntryControl"/>s</param>
        private void SetWidths(Panel panel)
        {
            var maxSize = 0d;

            foreach (var child in panel.Children)
            {
                // Ignore anything that isn't a TextEntryControl
                if (!(child is TextEntryControl control))
                    continue;

                // Find out whether this value is larger than the others
                maxSize = Math.Max(maxSize, control.Label.RenderSize.Width + control.Label.Margin.Left + control.Label.Margin.Right);
            }

            var gridLength = (GridLength)new GridLengthConverter().ConvertFromString(maxSize.ToString());

            foreach (var child in panel.Children)
            {
                // Ignore anything that isn't a TextEntryControl
                if (!(child is TextEntryControl control))
                    continue;

                // Set each control's LabelWidth value to the max size
                control.LabelWidth = gridLength;
            }
        }
    }
}
