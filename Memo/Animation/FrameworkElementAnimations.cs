﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Memo
{
    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameworkElementAnimations
    {
        #region Left

        /// <summary>
        /// Slides an element in from the left
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="direction">The direction of the slide animation</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same width or not, during animation</param>
        /// <param name="size">The animation width/height to animate to. If unspecified, the element's size is used</param>
        /// <param name="firstLoad">Indicates if this is the first load</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInAsync(this FrameworkElement element, AnimationSlideInDirection direction, bool firstLoad, float seconds = 0.3f, bool keepMargin = true, int size = 0)
        {
            var sb = new Storyboard();

            //switch ()

            sb.AddSlideFromLeft(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);

            sb.AddFadeIn(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the left
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same width or not, during animation</param>
        /// <param name="width">The animation width to animate to. If unspecified, the element's width is used</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();

            sb.AddSlideToLeft(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);

            sb.AddFadeOut(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));

            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Right

        /// <summary>
        /// Slides an element in from the right
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same width or not, during animation</param>
        /// <param name="width">The animation width to animate to. If unspecified, the element's width is used</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();

            sb.AddSlideFromRight(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);

            sb.AddFadeIn(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the right
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same width or not, during animation</param>
        /// <param name="width">The animation width to animate to. If unspecified, the element's width is used</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int width = 0)
        {
            var sb = new Storyboard();

            sb.AddSlideToRight(seconds, width == 0 ? element.ActualWidth : width, keepMargin: keepMargin);

            sb.AddFadeOut(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));

            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Bottom

        /// <summary>
        /// Slides an element in from the bottom
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same height or not, during animation</param>
        /// <param name="height">The animation height to animate to. If unspecified, the element's height is used</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();

            sb.AddSlideFromBottom(seconds, height == 0 ? element.ActualHeight : height, keepMargin: keepMargin);

            sb.AddFadeIn(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides an element out to the bottom
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <param name="keepMargin">Whether to keep the element at the same height or not, during animation</param>
        /// <param name="height">The animation height to animate to. If unspecified, the element's height is used</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToBottomAsync(this FrameworkElement element, float seconds = 0.3f, bool keepMargin = true, int height = 0)
        {
            var sb = new Storyboard();

            sb.AddSlideToBottom(seconds, height == 0 ? element.ActualHeight : height, keepMargin: keepMargin);

            sb.AddFadeOut(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));

            element.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Fade In/Out

        /// <summary>
        /// Fades an element in
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            var sb = new Storyboard();

            sb.AddFadeIn(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Fades an element out
        /// </summary>
        /// <param name="element">The element to animate</param>
        /// <param name="seconds">Duration of the animation</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float seconds = 0.3f)
        {
            var sb = new Storyboard();

            sb.AddFadeOut(seconds);

            sb.Begin(element);

            if (seconds != 0)
                element.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));

            element.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
