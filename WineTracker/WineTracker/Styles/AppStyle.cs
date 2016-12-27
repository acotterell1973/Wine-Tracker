using Xamarin.Forms;

namespace WineTracker.Styles
{
	public static class AppStyle
	{
		// Define arbitrary style variables here (e.g. layout width, common font size, etc.)
		public static int SidebarClosedWidth = 80;
		public static int SidebarOpenWidth = 257;
		public static double SidebarClosedModalOverlayOpacity = 0;
		public static double SidebarOpenModalOverlayOpacity = 0.7;
		public static int TitleFontSize = 21;
		public static int LargeButtonHeight = 60;
		public static double OverlayMaskOpacity = 0.7;



		// Define styles.
		// These are globally-accessible and explicit (named).

		//== Sitewide Buttons

		public static class ButtonStyle
		{
			public static int WidthShort => 130;
			public static int WidthMedium => 206;
			public static int WidthLong => 283;

			public static Style Accent => new Style(typeof(Button))
			{
				Setters =
				{
					new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.TurquoiseBlue}
				},
				BasedOn = DefaultStyle.ButtonStyle
			};
		}



		//== Sitewide Labels / Fonts

		public static class LabelStyle
		{
			public static Style LargeLabel => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.FontSizeProperty, Value = TitleFontSize}
				}
			};

			public static Style LargeLabelError => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = BrandColor.Error}
				},
				BasedOn = LargeLabel
			};

			public static Style LargeLabelBold => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.FontSizeProperty, Value = TitleFontSize}
				},
				BasedOn = DefaultStyle.LabelBold
			};

			public static Style LargeLabelBoldError => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = BrandColor.Error}
				},
				BasedOn = LargeLabelBold
			};

			public static Style SidebarUserInfo => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.FontSizeProperty, Value = 12},
					new Setter {Property = Label.TextColorProperty, Value = Color.White}
				}
			};
		}


		//== Miscellaneous
		public static Style OverlayMask => new Style(typeof(BoxView))
		{
			Setters =
			{
				new Setter {Property = BoxView.ColorProperty, Value = Color.Black},
				new Setter {Property = VisualElement.OpacityProperty, Value = OverlayMaskOpacity }
			}
		};

		public static Style OverlayMaskWhite => new Style(typeof(BoxView))
		{
			Setters =
			{
				new Setter {Property = BoxView.ColorProperty, Value = Color.White},
				new Setter {Property = VisualElement.OpacityProperty, Value = OverlayMaskOpacity }
			}
		};





		#region Page-Specific Styles
		/****************************
		 *** Page-Specific Styles ***
		 ***************************/



		//== Dashboard Page

		public static class DashboardPageStyle
		{
			public static Style Message => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = StackLayout.PaddingProperty, Value = 20}
				}
			};

			public static Style MessageTitle => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.FontSizeProperty, Value = 24},
				}
			};

			public static Style MessageBody => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.FontSizeProperty, Value = 14},
				}
			};
		}


		//== Pick Ticket List Page

		public static class PickTicketListPageStyle
		{
			public static Style TicketDefaultBackground => new Style(typeof(StackLayout))
			{
				Setters =
				{
					new Setter {Property = VisualElement.BackgroundColorProperty, Value = Color.White}
				}
			};

			public static Style TicketLockedBackground => new Style(typeof(StackLayout))
			{
				Setters =
				{
					new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.PinkishGray}
				}
			};

			public static Style TicketSeparatorDefaultStyle => new Style(typeof(BoxView))
			{
				Setters =
				{
					new Setter {Property = VisualElement.BackgroundColorProperty, Value = BrandColor.PinkishGray},
					new Setter {Property = VisualElement.HeightRequestProperty, Value = 4}
				}
			};

			public static Style TicketSeparatorLastBatch => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = Color.White},
					new Setter {Property = Label.FontFamilyProperty, Value = "AvenirNext-Regular"},
					new Setter {Property = Label.FontSizeProperty, Value = 27},
					new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
				}
			};

			public static Style TicketTitle => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = Color.White},
					new Setter {Property = Label.FontSizeProperty, Value = 44},
					new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
				}
			};

			public static Style TicketCategoryTitle => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = Color.White},
					new Setter {Property = Label.FontSizeProperty, Value = 72},
					new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
				},
				BasedOn = DefaultStyle.LabelBold
			};

			public static Style TicketBaseLabel => new Style(typeof(Label))
			{
				Setters =
				{
					new Setter {Property = Label.TextColorProperty, Value = Color.White},
					new Setter {Property = Label.FontSizeProperty, Value = 27},
					new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start}
				}
			};
		}



		//== Pick Ticket Detail Page

		public static class PickTicketDetailPageStyle
		{
			public static double LineItemHeightDefault => 251;
			public static double LineItemHeightShort => 114;
			public static double LineItemImageHeightDefault => 142;
			public static double LineItemImageHeightShort => 82;

			public static Style LineItemDefaultSeparator => new Style(typeof(BoxView))
			{
				Setters =
				{
					new Setter {Property = VisualElement.HeightRequestProperty, Value = 20},
					new Setter {Property = BoxView.ColorProperty, Value = BrandColor.PinkishGray}
				}
			};

			public static Style LineItemFullyPickedSeparator => new Style(typeof(BoxView))
			{
				Setters =
				{
					new Setter {Property = VisualElement.HeightRequestProperty, Value = 1},
					new Setter {Property = BoxView.ColorProperty, Value = BrandColor.White}
				}
			};
		}


		#endregion

	}
}
