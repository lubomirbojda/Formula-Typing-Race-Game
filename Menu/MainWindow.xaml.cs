using Microsoft.Xna.Framework;
using Aplikacia;
using Aplikacia.PozadieHry;
using Aplikacia.Zavodnici;
using System.ComponentModel;
using System.Windows;


namespace Menu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Obtiaznost? PoslednyHranaObtiaznost;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZmenaStavuHry(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Hra hra)
            {
                PoslednyHranaObtiaznost = hra.Level_.Obtiaznost_;
                Show();
                ObrazovkaKoniecHry(hra);
            }
        }

        private void ObrazovkaKoniecHry(Hra hra)
        {
            HratButton.Visibility = Visibility.Collapsed;
            VypnutButton.Visibility = Visibility.Collapsed;
            LahkaButton.Visibility = Visibility.Collapsed;
            StrednaButton.Visibility = Visibility.Collapsed;
            TazkaButton.Visibility = Visibility.Collapsed;
            SpatButton.Visibility = Visibility.Visible;
            OpakovatButton.Visibility = Visibility.Visible;
            if (hra.Level_.Vyhra)
            {
                TextVyhraPrehra.Text = "VYHRAL SI";
                FinalnyCas.Text = hra.Level_.StopkyHry.Elapsed.ToString(@"m\:ss\.fff");
                FinalnyCas.Visibility = Visibility.Visible;
            }
            else
            {
                TextVyhraPrehra.Text = "PREHRAL SI";
                FinalnyCas.Visibility = Visibility.Collapsed;
            }
            TextVyhraPrehra.Visibility = Visibility.Visible;
        }

        private void HratButton_Click(object sender, RoutedEventArgs e)
        {
            HratButton.Visibility = Visibility.Collapsed;
            VypnutButton.Visibility = Visibility.Collapsed;
            OpakovatButton.Visibility = Visibility.Collapsed;
            LahkaButton.Visibility = Visibility.Visible;
            StrednaButton.Visibility = Visibility.Visible;
            TazkaButton.Visibility = Visibility.Visible;
            SpatButton.Visibility = Visibility.Visible;
            FinalnyCas.Visibility = Visibility.Collapsed;
            TextVyhraPrehra.Visibility = Visibility.Collapsed;
        }

        private void VypnutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LahkaButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var hra = new Hra();
            hra.PropertyChanged += ZmenaStavuHry;
            var textury = new Textury(hra.Content);
            var hracY = Hrac.DajYHraca();
            var hrac = new Hrac(textury.TexturaHrac, Hra.VypocitajRychlostHraca(), new Vector2(0, hracY), 7);
            hra.NacitajLevel(new Level(hrac, new Pozadie(textury.TexturaPozadie), textury, Obtiaznost.Lahka));
            hra.Run();
            hra.Dispose();

        }

        private void StrednaButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var hra = new Hra();
            hra.PropertyChanged += ZmenaStavuHry;
            var textury = new Textury(hra.Content);
            var hracY = Hrac.DajYHraca();
            var hrac = new Hrac(textury.TexturaHrac, Hra.VypocitajRychlostHraca(), new Vector2(0, hracY), 5);
            hra.NacitajLevel(new Level(hrac, new Pozadie(textury.TexturaPozadie), textury, Obtiaznost.Stredna));
            hra.Run();
            hra.Dispose();
        }

        private void TazkaButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var hra = new Hra();
            hra.PropertyChanged += ZmenaStavuHry;
            var textury = new Textury(hra.Content);
            var hracY = Hrac.DajYHraca();
            var hrac = new Hrac(textury.TexturaHrac, Hra.VypocitajRychlostHraca(), new Vector2(0, hracY), 3);
            hra.NacitajLevel(new Level(hrac, new Pozadie(textury.TexturaPozadie), textury, Obtiaznost.Tazka));
            hra.Run();
            hra.Dispose();
        }

        private void SpatButton_Click(object sender, RoutedEventArgs e)
        {
            HratButton.Visibility = Visibility.Visible;
            VypnutButton.Visibility = Visibility.Visible;
            LahkaButton.Visibility = Visibility.Collapsed;
            StrednaButton.Visibility = Visibility.Collapsed;
            TazkaButton.Visibility = Visibility.Collapsed;
            SpatButton.Visibility = Visibility.Collapsed;
            OpakovatButton.Visibility = Visibility.Collapsed;
            FinalnyCas.Visibility = Visibility.Collapsed;
            TextVyhraPrehra.Visibility = Visibility.Collapsed;
        }

        private void OpakovatButton_Click(object sender, RoutedEventArgs e)
        {
            if (PoslednyHranaObtiaznost != null)
            {
                Hide();
                switch (PoslednyHranaObtiaznost)
                {
                    case Obtiaznost.Lahka:
                        LahkaButton_Click(sender, e);
                        break;
                    case Obtiaznost.Stredna:
                        StrednaButton_Click(sender, e);
                        break;
                    case Obtiaznost.Tazka:
                        TazkaButton_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}