using Metrofier.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Metrofier.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private readonly IMetrofierService service;
        const string address = "http://localhost:8000/Metrofier/service";

        public MainPage()
        {
            this.InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IMetrofierService> factory = new ChannelFactory<IMetrofierService>(binding, new EndpointAddress(address));
            service = factory.CreateChannel();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //service.Start("C:\\Program Files (x86)\\XBMC\\XBMC.exe", "C:\\Program Files (x86)\\XBMC");
            service.Start("C:\\Windows\\System32\\notepad.exe", "C:\\Windows\\System32");
        }
    }
}
