using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using cnTelefonkonyv;
using Models;
using Telefonszamok_Alap.Models;

namespace Telefonszamok_Alap;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    TelefonkonyvContext _context;
    public MainWindow()
    {
        InitializeComponent();
        _context = new TelefonkonyvContext();
        
    }

    private void miMentes_Click(object sender, RoutedEventArgs e)
    {
        _context.SaveChanges();
    }

    private void miKilepes_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void miMindenAdat_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Visible;
        grAdatok.Visibility = Visibility.Hidden;

        var adatok = (from s in _context.enSzemelyek
                      join h in _context.enHelysegek on s.enHelysegid equals h.id
                      join t in _context.enTelefonszamok on h.id equals t.id
                      select new Lekerdezes
                      {
                          Vezeteknev = s.Vezeteknev,
                          Utonev = s.Utonev,
                          Iranyitoszam = h.IRSZ,
                          Lakcim = s.Lakcim,
                          Varos = h.Nev,
                          Telefonszam = t.Szam
                      }).ToList();
        dgAdatracs.ItemsSource = adatok;
    }

    private void miHelyisegek_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Visible;
        grAdatok.Visibility = Visibility.Hidden;

        var lambda = _context.enHelysegek.ToList();
        var linq = (from h in _context.enHelysegek
                    select new { h.IRSZ, h.Nev }).ToList();

        dgAdatracs.ItemsSource = linq;
    }

    private void miUjhelyiseg_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Collapsed;
        grAdatok.Visibility = Visibility.Visible;

        var helyisegek = _context.enHelysegek.ToList();

        //Combobox feltöltése
        cbIrsz.ItemsSource = helyisegek;
        cbHelyisegnev.ItemsSource = helyisegek;

        grAdatok.DataContext = _context.enHelysegek.ToList();
        cbIrsz.SelectedItem = 0;
    }

    private void cbIrsz_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var enAktualis = ((ComboBox)sender).SelectedItem as enHelyseg;
                            cbHelyisegnev.SelectedItem = enAktualis;
                            tbIrsz.Text = enAktualis.IRSZ.ToString();
                            tbVaros.Text = enAktualis.Nev.ToString();
    }

    private void cbHelyisegnev_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void btRogzit_Click(object sender, RoutedEventArgs e)
    {
        var enAktualis = cbIrsz.SelectedItem as enHelyseg;
        enAktualis.IRSZ = tbIrsz.Text;
        enAktualis.Nev = tbVaros.Text;
    }

    private void btMent_Click(object sender, RoutedEventArgs e)
    {
        var ujadat = new enHelyseg
        {
            IRSZ = tbIrsz.Text,
            Nev = tbVaros.Text,
        };
        try
        {
            bool exist = _context.enHelysegek.Any(h => h.IRSZ == ujadat.IRSZ && h.Nev == ujadat.Nev);

            if (!exist)
            {
                _context.enHelysegek.Add(ujadat);
                _context.SaveChanges();
                MessageBox.Show(ujadat.Nev + " " + ujadat.IRSZ);
                MessageBox.Show("A mentés sikerült!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("A rekord már létezik az adatbázisban:" + ujadat.Nev + " " + ujadat.IRSZ);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btVissza_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Hidden;
        grAdatok.Visibility = Visibility.Hidden;
    }
}