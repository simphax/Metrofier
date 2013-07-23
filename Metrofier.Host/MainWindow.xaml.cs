using Metrofier.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
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

namespace Metrofier.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost host = null;
        private string urlMeta, urlService = "http://localhost:8000/Metrofier/service";

        public MainWindow()
        {
            InitializeComponent();

            host = new ServiceHost(typeof(MetrofierService));
            host.Opening += new EventHandler(host_Opening);
            host.Opened += new EventHandler(host_Opened);
            host.Closing += new EventHandler(host_Closing);
            host.Closed += new EventHandler(host_Closed);

            BasicHttpBinding httpBinding = new BasicHttpBinding();
            host.AddServiceEndpoint(typeof(IMetrofierService), httpBinding, urlService);

            host.Open();
        }

        private void host_Closed(object sender, EventArgs e)
        {
            
        }

        private void host_Closing(object sender, EventArgs e)
        {
            
        }

        private void host_Opened(object sender, EventArgs e)
        {
            
        }

        private void host_Opening(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MetrofierService service = new MetrofierService();
            service.Start("C:\\Program Files (x86)\\XBMC\\XBMC.exe", "C:\\Program Files (x86)\\XBMC");
        }
    }
}
