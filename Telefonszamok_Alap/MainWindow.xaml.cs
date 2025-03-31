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



}