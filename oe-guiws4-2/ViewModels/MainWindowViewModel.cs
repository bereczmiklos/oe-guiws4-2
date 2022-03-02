using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oe_guiws4_2.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        ObservableCollection<string> Heroes { get; set; }
        ObservableCollection<string> SelectedHeroes { get; set; }

        public MainWindowViewModel()
        {

        }
    }
}
