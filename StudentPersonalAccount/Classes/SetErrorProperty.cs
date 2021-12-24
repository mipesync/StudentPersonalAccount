using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudentPersonalAccount.Classes
{
    public class SetErrorProperty
    {
        public void SetProperty(TextBox textBox, PackIcon icon, string text)
        {
            HintAssist.SetHelperText(textBox, text);
            icon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
        }

        public void SetProperty(PasswordBox passwordBox, PackIcon icon, string text)
        {
            HintAssist.SetHelperText(passwordBox, text);
            icon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
            passwordBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
        }

        public void ClearProperty(List<TextBox> textBoxes, List<PackIcon> packIcons, List<PasswordBox> passwordBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                HintAssist.SetHelperText(textBox, "");
                textBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            foreach (var icon in packIcons)
            {
                icon.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            foreach (var passwordBox in passwordBoxes)
            {
                HintAssist.SetHelperText(passwordBoxes[0], "At least 8 characters");
                HintAssist.SetHelperText(passwordBoxes[1], "");
                passwordBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }
    }

}
