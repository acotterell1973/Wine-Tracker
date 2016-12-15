using Xamarin.Forms;

namespace WineTracker.Styles
{
    public static class Styles
    {
        public static ResourceDictionary StyleDictionary { get; } = new ResourceDictionary();

        public static void InitStyles()
        {
            ////// Buttons

            // button - base styles
            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.ButtonPrimary},
                    new Setter {Property = Button.TextColorProperty, Value = Color.White},
                    new Setter {Property = Button.FontFamilyProperty, Value = "AvenirNext-Bold"},
                    new Setter {Property = Button.FontSizeProperty, Value = 21},
                    new Setter {Property = VisualElement.HeightRequestProperty, Value = 60},
                    new Setter {Property = Button.BorderRadiusProperty, Value = 0}
                }
            };
            StyleDictionary.Add(buttonStyle);

            // secondary button
            var secondaryButtonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.ButtonSecondary}
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
                            new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.ButtonDisabled}
                        }
                    }
                },
                BasedOn = buttonStyle
            };
            StyleDictionary.Add("secondaryButtonStyle", secondaryButtonStyle);



            ////// Entries

            // entry - base styles
            var entryStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.HeightRequestProperty, Value = 40},
                    new Setter {Property = InputView.KeyboardProperty, Value = Keyboard.Create(0)}
                },
                Triggers =
                {
                    new Trigger(typeof (Entry))
                    {
						// Focused entry
						Property = VisualElement.IsFocusedProperty,
                        Value = false,
                        Setters =
                        {
                            new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.Green}
                        }
                    }
                }
            };
            StyleDictionary.Add(entryStyle);



            ////// Labels

            // labels - base styles
            var labelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = BrandColor.Default},
                    new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Regular"}
                }
            };
            StyleDictionary.Add(labelStyle);

            // Sidebar - user labels
            var sidebarUserInfoStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 12},
                    new Setter {Property = Label.TextColorProperty, Value = Color.White}
                }
            };
            StyleDictionary.Add("sidebarUserInfoStyle", sidebarUserInfoStyle);




            #region Page-Specific Styles
            //////////////////////////////////
            ////// Page-Specific Styles //////
            //////////////////////////////////



            ////// Dashboard Page

            // message - title
            var dashboardPageMessageTitleStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Bold}
                }
            };
            StyleDictionary.Add("dashboardPageMessageTitleStyle", dashboardPageMessageTitleStyle);

            // message - spacing
            var dashboardPageMessagePaddingStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = Layout.PaddingProperty, Value = new Thickness(0, 0, 0, 20)},
                    new Setter {Property = StackLayout.SpacingProperty, Value = 0}
                }
            };
            StyleDictionary.Add("dashboardPageMessagePaddingStyle", dashboardPageMessagePaddingStyle);

            // bucket - container - base
            var dashboardPageBucketStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = StackLayout.OrientationProperty, Value = StackOrientation.Horizontal},
                    new Setter {Property = StackLayout.SpacingProperty, Value = 0},
                    new Setter {Property = VisualElement.HeightRequestProperty, Value = 130},
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.Default},
                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand}
                }
            };
            StyleDictionary.Add("dashboardPageBucketStyle", dashboardPageBucketStyle);

            // bucket - container - success
            var dashboardPageBucketSuccessStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.Success}
                },
                BasedOn = dashboardPageBucketStyle
            };
            StyleDictionary.Add("dashboardPageBucketSuccessStyle", dashboardPageBucketSuccessStyle);

            // bucket - container - error
            var dashboardPageBucketErrorStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.Error}
                },
                BasedOn = dashboardPageBucketStyle
            };
            StyleDictionary.Add("dashboardPageBucketErrorStyle", dashboardPageBucketErrorStyle);

            // bucket - container - neutral
            var dashboardPageBucketNeutralStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.Gray}
                },
                BasedOn = dashboardPageBucketStyle
            };
            StyleDictionary.Add("dashboardPageBucketNeutralStyle", dashboardPageBucketNeutralStyle);

            // bucket - base text
            var dashboardPageBucketTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.White},
                    new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center}
                }
            };
            StyleDictionary.Add("dashboardPageBucketTextStyle", dashboardPageBucketTextStyle);

            // bucket - action label
            var dashboardPageBucketActionStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 20},
                    new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Bold}
                },
                BasedOn = dashboardPageBucketTextStyle
            };
            StyleDictionary.Add("dashboardPageBucketActionStyle", dashboardPageBucketActionStyle);

            // bucket - number label
            var dashboardPageBucketNumberStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 35},
                    new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Bold},
                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Fill}
                },
                BasedOn = dashboardPageBucketTextStyle
            };
            StyleDictionary.Add("dashboardPageBucketNumberStyle", dashboardPageBucketNumberStyle);

            // bucket - item type label
            var dashboardPageBucketItemTypeStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 17},
                    new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Bold}
                },
                BasedOn = dashboardPageBucketTextStyle
            };
            StyleDictionary.Add("dashboardPageBucketItemTypeStyle", dashboardPageBucketItemTypeStyle);

            // bucket - SLA message
            var dashboardPageBucketSlaMessageStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 10}
                },
                BasedOn = dashboardPageBucketTextStyle
            };
            StyleDictionary.Add("dashboardPageBucketSlaMessageStyle", dashboardPageBucketSlaMessageStyle);

            // pickTicket - background - default
            var dashboardPageTicketDefaultBackgroundStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.White}
                }
            };
            StyleDictionary.Add("dashboardPageTicketDefaultBackgroundStyle", dashboardPageTicketDefaultBackgroundStyle);

            // pickTicket - background - locked
            var dashboardPageTicketLockedBackgroundStyle = new Style(typeof(StackLayout))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.FromHex("dddddd")}
                }
            };
            StyleDictionary.Add("dashboardPageTicketLockedBackgroundStyle", dashboardPageTicketLockedBackgroundStyle);

            // pickTicket - ticket separator - default
            var dashboardPageTicketSeparatorDefaultStyle = new Style(typeof(BoxView))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.Gray},
                    new Setter {Property = VisualElement.HeightRequestProperty, Value = 1}
                }
            };
            StyleDictionary.Add("ticketSeparatorDefaultStyle", dashboardPageTicketSeparatorDefaultStyle);

            // pickTicket - ticket separator - last batch
            var dashboardPageTicketSeparatorLastBatchStyle = new Style(typeof(BoxView))
            {
                Setters =
                {
                    new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.Blue},
                    new Setter {Property = VisualElement.HeightRequestProperty, Value = 2}
                }
            };
            StyleDictionary.Add("ticketSeparatorLastBatchStyle", dashboardPageTicketSeparatorLastBatchStyle);

            #endregion
            #region PickList Card Styles
            var baseTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.White},
                    new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Regular"},
                    new Setter {Property = Label.FontSizeProperty, Value = 27},
                    new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
                }
            };
            StyleDictionary.Add("baseTextStyle", baseTextStyle);

            var baseTextBoldStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.White},
                    new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Bold"},
                    new Setter {Property = Label.FontSizeProperty, Value = 21}
                }
            };
            StyleDictionary.Add("baseTextBoldStyle", baseTextBoldStyle);

            var baseTextBoldItalicStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("333333")},
                    new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-BoldItalic"},
                    new Setter {Property = Label.FontSizeProperty, Value = 12}
                }
            };
            StyleDictionary.Add("baseTextBoldItalicStyle", baseTextBoldItalicStyle);

            var baseTextItalicStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("333333")},
                    new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Italic"},
                    new Setter {Property = Label.FontSizeProperty, Value = 12}
                }
            };
            StyleDictionary.Add("baseTextItalicStyle", baseTextItalicStyle);

            var baseButtonTextBoldStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter {Property = Button.TextColorProperty, Value = Color.White},
                    new Setter {Property = Button.FontFamilyProperty, Value = "AvenirNext-Bold"},
                    new Setter {Property = Button.FontSizeProperty, Value = 21}
                }
            };
            StyleDictionary.Add("baseButtonTextBoldStyle", baseButtonTextBoldStyle);

            // label - base text
            var labelTitleTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 44},
                },
                BasedOn = baseTextStyle
            };
            StyleDictionary.Add("labelTitleTextStyle", labelTitleTextStyle);

            // label - base text
            var cardCategoryLabelTextStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.FontSizeProperty, Value = 72},
                    new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
                },
                BasedOn = baseTextBoldStyle
            };
            StyleDictionary.Add("cardCategoryLabelTextStyle", cardCategoryLabelTextStyle);

            #region Row Style
            var rowLabelTextBoldStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("333333")},
                    new Setter {Property = Label.FontSizeProperty, Value = 16},
                    new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
                },
                BasedOn = baseTextBoldStyle
            };
            StyleDictionary.Add("rowLabelTextBoldStyle", rowLabelTextBoldStyle);

            var rowLabelTextNormalStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("333333")},
                    new Setter {Property = Label.FontSizeProperty, Value = 16},
                    new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
                },
                BasedOn = baseTextStyle
            };
            StyleDictionary.Add("rowLabelTextNormalStyle", rowLabelTextNormalStyle);
            #endregion
            #endregion
        }
    }
}
