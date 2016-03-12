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

namespace TeamProjectChess.View
{
    /// <summary>
    /// Логика взаимодействия для LogInWin.xaml
    /// </summary>
    public partial class LogInWin : Window
    {
        public LogInWin()
        {
            InitializeComponent();
            webbrowser.Navigate("https://oauth.vk.com/authorize?client_id=5330828&display=page&redirect_uri=https://oauth.vk.com/blank.html&display=page&scope=friends,wall&response_type=token");
        }
    }
}
