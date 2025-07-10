using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GStore.Models.ViewModels
{
    public class StatusChangeTextSetter
    {
        public string TitleText { get; set; }

        public string WarningText { get; set; }

        public string ButtonText { get; set; }

        public StatusChangeTextSetter SetOnActive(string stringEndFirstSentense, string stringEndSecondSentence)
        {
            TitleText = "Деактивиране на " + stringEndFirstSentense;
            WarningText = "Сигурен ли сте, че искате да деактивирате " + stringEndSecondSentence;
            ButtonText = "Деактивирай";

            return this;
        }

        public StatusChangeTextSetter SetOnDeactivated(string stringEndFirstSentense)
        {
            TitleText = "Активиране на " + stringEndFirstSentense;
            WarningText = "";
            ButtonText = "Активирай";

            return this;
        }
    }
}
