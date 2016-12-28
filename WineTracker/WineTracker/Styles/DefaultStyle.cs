using System.Collections.Generic;
using FreshEssentials;
using Xamarin.Forms;

namespace WineTracker.Styles
{
    public static class DefaultStyle
    {
        // Implicit (default) styles go inside Resource Dictionary
        public static ResourceDictionary StyleDictionary { get; } = new ResourceDictionary();

        public static Style ButtonStyle => new Style(typeof(Button))
        {
            Setters =
            {
                new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.Black},
                new Setter {Property = Button.TextColorProperty, Value = BrandColor.White},
                new Setter {Property = Button.FontFamilyProperty, Value = "AvenirNext-Bold"},
                new Setter {Property = Button.FontSizeProperty, Value = 21},
                new Setter {Property = VisualElement.HeightRequestProperty, Value = 48},
                new Setter {Property = Button.BorderRadiusProperty, Value = 0}
            },
            Triggers =
            {
                new Trigger(typeof(Button))
                {
					// Focused entry
					Property = VisualElement.IsEnabledProperty,
                    Value = false,
                    Setters =
                    {
                        new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.PinkishGray}
                    }
                }
            }
        };

        public static Style EntryStyle => new Style(typeof(Entry))
        {
            Setters =
            {
                new Setter {Property = VisualElement.HeightRequestProperty, Value = 40},
                new Setter {Property = InputView.KeyboardProperty, Value = Keyboard.Create(0)}
            }
        };

        public static Style LabelStyle => new Style(typeof(Label))
        {
            Setters =
            {
                new Setter {Property = Label.TextColorProperty, Value = BrandColor.Default},
                new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Regular"}
            }
        };

        public static Style AdvancedFrameStyle => new Style(typeof(AdvancedFrame))
        {
            Setters =
            {
                new Setter {Property = AdvancedFrame.InnerBackgroundProperty, Value = BrandColor.White},
                new Setter {Property = AdvancedFrame.OutlineColorProperty, Value = BrandColor.White},
                new Setter {Property = AdvancedFrame.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand}
            }
        };

        public static Style StackLayoutStyle => new Style(typeof(StackLayout))
        {
            Setters =
            {
                new Setter {Property = StackLayout.SpacingProperty, Value = 0},
                new Setter {Property = StackLayout.MarginProperty, Value = new Thickness(5,5,5,5)},
                new Setter {Property = StackLayout.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand},
                new Setter {Property = StackLayout.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand},
            }
        };

        public static Style LabelBold => new Style(typeof(Label))
        {
            Setters =
            {
                new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Bold"}
            }
        };

        public static Style LabelItalic => new Style(typeof(Label))
        {
            Setters =
            {
                new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Italic"}
            }
        };

        public static Style LabelBoldItalic => new Style(typeof(Label))
        {
            Setters =
            {
                new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-BoldItalic"}
            }
        };

        // Put styles into resource dictionary
        public static void InitStyles()
        {
            // Add implicit (default) styles into this list
            var ImplicitStyleList = new List<Style>
            {
                ButtonStyle,
                EntryStyle,
                LabelStyle,
                AdvancedFrameStyle,
                StackLayoutStyle
            };

            // Loop through and adds styles in above list
            // into resource dictionary
            foreach (var style in ImplicitStyleList)
            {
                StyleDictionary.Add(style);
            }
        }
    }
}
