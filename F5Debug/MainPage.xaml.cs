using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Phone.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace F5Debug
{
    public partial class MainPage : PhoneApplicationPage
    {
        string[] post;
        string[] link;
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        //articles start
        private void articlesLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "articles");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //news start
        private void newsLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(news_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/News/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void news_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "news");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //review start
        private void reviewLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(review_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Review/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void review_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "review");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxReview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //tips start
        private void tipsLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tips_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Tips/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void tips_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "tips");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxTips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //video start
        private void videoLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(video_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Video/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void video_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "video");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxVideo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //giveaway start
        private void giveawayLoaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(giveaway_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Gadget/syndication.axd"));
            progressbar.Visibility = Visibility.Visible;
        }

        private void giveaway_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            downloadComplete(e, "giveaway");
            progressbar.Visibility = Visibility.Collapsed;
        }

        private void feedListBoxGiveaway_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged(sender);
        }

        //Uri Download Complete
        public void downloadComplete(DownloadStringCompletedEventArgs e, string cat)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                this.State["feed"] = e.Result;
                UpdateFeedList(e.Result, cat);
            }
        }

        //Updating Feed List
        private void UpdateFeedList(string feedXML, string cat)
        {
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            if(cat == "articles")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBox.ItemsSource = feed.Items;
                });
            if(cat == "news")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBoxNews.ItemsSource = feed.Items;
                });
            if (cat == "review")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBoxReview.ItemsSource = feed.Items;
                });
            if (cat == "tips")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBoxTips.ItemsSource = feed.Items;
                });
            if (cat == "video")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBoxVideo.ItemsSource = feed.Items;
                });
            if (cat == "giveaway")
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    feedListBoxGiveaway.ItemsSource = feed.Items;
                });
        }

        //Open tapped post in browser
        public void selectionChanged(object sender)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                if (sItem.Links.Count > 0)
                {
                    Uri uri = sItem.Links.FirstOrDefault().Uri;

                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = uri;
                    webBrowserTask.Show();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_LoadStoriesCompleted);
            webClient.OpenReadAsync(new Uri("http://www.f5debug.net/search.aspx?q=" + searchtext.Text));
            progressbar.Visibility = Visibility.Visible;
            SecondListBox.Items.Clear();
        }
        private void webClient_LoadStoriesCompleted(object sender, OpenReadCompletedEventArgs args)
        {
            progressbar.Visibility = Visibility.Collapsed;
            if (args.Error != null) return; // an error occurred - you can handle to show an alert about this

            HtmlDocument doc = new HtmlDocument();
            doc.Load(args.Result); // load Html source from IO Stream
            post = new string[20];
            link = new string[20];
            int i = 0;
            foreach (HtmlNode postNode in doc.DocumentNode.SelectNodes("//div[contains(@class,'searchresult')]"))
            {
                HtmlNodeCollection nc = postNode.ChildNodes;
                post[i] = nc[1].InnerText;
                post[i] = HTMLtoPlain(post[i]);
                link[i] = postNode.SelectSingleNode(".//a").GetAttributeValue("href", "unknown").ToString();
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text = post[i];
                tb.FontSize = 24;
                tb.TextDecorations = TextDecorations.Underline;
                Color currentAccentColorHex = (Color)Application.Current.Resources["PhoneAccentColor"];
                SolidColorBrush backColor = new SolidColorBrush(currentAccentColorHex);
                tb.Foreground = backColor;
                TextBlock tbdesc = new TextBlock();
                tbdesc.TextWrapping = TextWrapping.Wrap;
                tbdesc.Text = HTMLtoPlain(nc[3].InnerText);
                //tbdesc.TextTrimming = TextTrimming.WordEllipsis;
                StackPanel sp = new StackPanel();
                sp.Children.Add(tb);
                sp.Children.Add(tbdesc);
                SecondListBox.Items.Add(sp);
                i++;
            }
        }

        void sf_Tap(object sender, GestureEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.URL = link[SecondListBox.SelectedIndex];
            task.Show();
        }
        public string HTMLtoPlain(string html)
        {
            string text = Regex.Replace(html.ToString(), "<[^>]+>", string.Empty);

            // Remove newline characters.
            text = text.Replace("\r", "").Replace("\n", "");

            // Remove encoded HTML characters.
            text = HttpUtility.HtmlDecode(text);
            return text;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if (pivotScreen.SelectedIndex == 0)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }
            if (pivotScreen.SelectedIndex == 1)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(news_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/News/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }
            if (pivotScreen.SelectedIndex == 2)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(review_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Review/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }
            if (pivotScreen.SelectedIndex == 3)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(tips_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Tips/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }
            if (pivotScreen.SelectedIndex == 4)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(video_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Video/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }
            if (pivotScreen.SelectedIndex == 5)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(giveaway_DownloadStringCompleted);
                webClient.DownloadStringAsync(new System.Uri("http://www.f5debug.net/Gadget/syndication.axd"));
                progressbar.Visibility = Visibility.Visible;
            }

        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            pivotScreen.SelectedIndex = 6;
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/about.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            string id = this.NavigationContext.QueryString["id"];
            if (id != null)
            {
                pivotScreen.SelectedIndex = Convert.ToInt16(id);
                this.NavigationContext.QueryString["id"] = null;
            }
        }
    }
}