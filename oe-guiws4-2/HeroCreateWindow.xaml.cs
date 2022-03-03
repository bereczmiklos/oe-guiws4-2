using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace oe_guiws4_2
{
    /// <summary>
    /// Interaction logic for HeroCreateWindow.xaml
    /// </summary>
    public partial class HeroCreateWindow : Window
    {
        public HeroCreateWindow(Hero hero)
        {
            InitializeComponent();
        }
    }
}
