using WineTracker.Styles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WineTracker.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            Padding = new Thickness(0, 20, 0, 0);
            BackgroundImage = "bg_bw";
          
            //ToolbarItems.Add(new ToolbarItem("Wine Capture", "Plus-30.png", () =>
            //{
            //    Application.Current.MainPage = new WineCapturePage();
            //}));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var basePageModel = this.BindingContext as FreshMvvm.FreshBasePageModel;
            if (basePageModel != null)
            {
                if (basePageModel.IsModalAndHasPreviousNavigationStack())
                {
                    if (ToolbarItems.Count < 2)
                    {
                        var closeModal = new ToolbarItem("Close Modal", "", () =>
                        {
                            basePageModel.CoreMethods.PopModalNavigationService();
                        });

                        ToolbarItems.Add(closeModal);
                    }
                }
            }
        }
    }
}

