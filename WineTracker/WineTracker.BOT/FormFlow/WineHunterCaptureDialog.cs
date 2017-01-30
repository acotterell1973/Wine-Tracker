using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;

namespace winetracker.bot.connector.FormFlow
{
    public enum CaptureOptions
    {
        [Terms("picture")]
        TakeAPicture =1,
        [Terms("barcode")]
        ScanTheBarcode,
        [Terms("manual")]
        ManualEntry
    }

    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.EnumSelectOne, "So how do you want to capture the wine today? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
    [Serializable]
    public class WineHunterCaptureDialog
    {
        [Prompt("So how do you want to capture the wine today? {||}")]
        public CaptureOptions? CaptureOption;

        public static IForm<WineHunterCaptureDialog> BuildForm()
        {
            return new FormBuilder<WineHunterCaptureDialog>()
                    .Message("Good Even Andrew, Welcome back!")
                    .Field(nameof(CaptureOption),validate:  (state, value) => (Task<ValidateResult>) value)
                    .Message("You have selected {CaptureOption}")
                    
                    .Build();
        }
    }
}
