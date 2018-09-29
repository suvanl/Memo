using System.Threading.Tasks;
using System.Windows;

namespace Memo
{
    /// <summary>
    /// A base class to run any animation when a respective boolean is set to true and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        #region Protected Properties

        protected bool mFirstFire = true;

        #endregion

        #region Public Properties

        public bool FirstLoad { get; set; } = true;

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            if ((bool)sender.GetValue(ValueProperty) == (bool)value && !mFirstFire)
                return;

            mFirstFire = false;

            if (FirstLoad)
            {
                // Starts off hidden before determining how to animate
                // if the initial animation is animating out
                if (!(bool)value)
                    element.Visibility = Visibility.Hidden;

                // Creates a single self-unhookable event for the element's "Loaded" event
                RoutedEventHandler onLoaded = null;
                onLoaded = async (ss, ee) =>
                {
                    // Unhook
                    element.Loaded -= onLoaded;

                    // Slight delay after load is required for some elements to be correctly laid out
                    // and for their widths/heights to be correctly calculated
                    await Task.Delay(5);

                    // Runs desired animation
                    DoAnimation(element, (bool)value);

                    // No longer in FirstLoad stage
                    FirstLoad = false;
                };

                // Hooks into the "Loaded" event of the element
                element.Loaded += onLoaded;
            }
            else
            {
                // Does the desired animation
                DoAnimation(element, (bool)value);
            }
        }

        protected virtual void DoAnimation(FrameworkElement element, bool value)
        {

        }

    }

    /// <summary>
    /// Animates a FrameworkElement, sliding it in from the left on show; sliding out to the left on hide
    /// </summary>
    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Animates a FrameworkElement, sliding it up from the bottom on show; sliding out to the bottom on hide
    /// </summary>
    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    /// <summary>
    /// Animates a FrameworkElement, sliding it up from the bottom on show; sliding out to the bottom on hide
    /// NOTE: Keeps the margin
    /// </summary>
    public class AnimateSlideInFromBottomMarginProperty : AnimateBaseProperty<AnimateSlideInFromBottomMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: true);
            else
                // Animate out
                await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, keepMargin: true);
        }
    }

    /// <summary>
    /// Animates a FrameworkElement, fading in on show; fading out on hide
    /// </summary>
    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.FadeInAsync(FirstLoad ? 0 : 0.3f);
            else
                // Animate out
                await element.FadeOutAsync(FirstLoad ? 0 : 0.3f);
        }
    }
}
