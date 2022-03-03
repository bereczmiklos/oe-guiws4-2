using Newtonsoft.Json;
using oe_guiws4_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace oe_guiws4_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("heroes.json"))
            {
                var heroes = JsonConvert.DeserializeObject<Hero[]>(File.ReadAllText("heroes.json"));
                heroes.ToList().ForEach(x => lb_barrack.Items.Add(x));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Hero> hero = new List<Hero>();
            foreach (var item in lb_barrack.Items)
            {
                hero.Add(item as Hero);
            }
            string jsonData = JsonConvert.SerializeObject(hero);
            File.WriteAllText("heroes.txt", jsonData);
        }
    }
}
