using System.Collections.Generic;
using FreshMvvm;
using Xamarin.Forms;
using WineTracker.Styles;

namespace WineTracker.NavigationService
{
    public class ThemedMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        readonly List<MenuItem> _pageIcons = new List<MenuItem>();

        public ThemedMasterDetailNavigationContainer(string navigationServiceName) : base(navigationServiceName)
        {
        }

        public void AddPageWithIcon<T>(string title, string icon = "", object data = null) where T : FreshBasePageModel
        {
            AddPage<T>(title, data);
            _pageIcons.Add(new MenuItem
            {
                Title = title,
                IconSource = icon
            });
        }

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            var listview = new ListView();
            var menuPage = new ContentPage
            {
                Title = menuPageTitle//,
               // BackgroundColor = Color.FromHex("#c8c8c8")
            };

            listview.ItemsSource = _pageIcons;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetValue(TextCell.TextColorProperty, Color.Black);
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");


            listview.ItemTemplate = cell;
            listview.ItemSelected += (sender, args) =>
            {
                if (Pages.ContainsKey(((MenuItem)args.SelectedItem).Title))
                {
                    Detail = Pages[((MenuItem)args.SelectedItem).Title];
                }
                IsPresented = false;
            };

            menuPage.Content = listview;

            var navPage = new NavigationPage(menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;

            Master = navPage;

        }

        protected override Page CreateContainerPage(Page page)
        {
            var navigation = new NavigationPage(page)
            {
                BarBackgroundColor = BrandColor.DashboardBackground,
                BarTextColor = BrandColor.WarmGrey
            };

            return navigation;
        }
    }
}
