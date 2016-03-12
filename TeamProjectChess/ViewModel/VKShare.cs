using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TeamProjectChess.View;
using System.Windows.Navigation;

namespace TeamProjectChess.ViewModel
{
    public class VKShare: ViewModelBase
    {
        public string access_token;
        public string user_id;
        public string AppID = "5330828";

        WebBrowser wb = new WebBrowser();
        LogInWin win;
        public string Title { get { return "Share"; } }
        private ICommand _share;
        WebBrowser webbrowser = new WebBrowser();
        public ICommand Share
        {
            get
            {
                return _share ?? (_share = new RelayCommand(() =>
                {
                    Autorisation();
                }));
            }
        }

        public void Autorisation()
        {
            win = new LogInWin();
            access_token = null;
            user_id = null;
            win.webbrowser.Navigated += new NavigatedEventHandler(Authorize_proceed);
            win.ShowDialog();
            //if (access_token == null || user_id == null)
            //{
            //    return false;
            //}
            //else return true;
        }
        public void Authorize_proceed(object sender, NavigationEventArgs e)
        {
            string[] parts = e.Uri.AbsoluteUri.Split('#');
            if (parts[0] == "https://oauth.vk.com/blank.html")
            {
                if (parts[1].Substring(0, 5) == "error") win.Close();
                else if (parts[1].Substring(0, 12) == "access_token")
                {
                    access_token = parts[1].Split('=')[1].Split('&')[0];
                    user_id = parts[1].Split('&')[2].Split('=')[1];
                    string message = "Join me in a fantastic new Chess Puzzle App";
                    var str = string.Format("https://api.vk.com/method/wall.post?message={0}&access_token={1}", message, access_token);
                    Uri uri = new Uri(str);
                    HttpClient client = new HttpClient();
                    var response = client.GetAsync(uri).Result;
                    this.win.Close();
                    MessageBox.Show("You have talked about our awesome application^^");
                }
            }
            else
            {
                parts = e.Uri.AbsoluteUri.Split('?');
                if (parts[0] == "https://oauth.vk.com/oauth/authorize")
                    webbrowser.Navigate("https://oauth.vk.com/authorize?client_id=5330828&display=page&redirect_uri=https://oauth.vk.com/blank.html&display=page&scope=friends,wall&response_type=token");
                else
                    MessageBox.Show("You should update your IE browser!");
            }
        }


    }
}
