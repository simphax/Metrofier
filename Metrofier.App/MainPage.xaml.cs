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

        public MainPage()
        {
            this.InitializeComponent();
            this.Unloaded += OnUnloaded;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnUnloaded");
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnNavigatedTo");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnNavigatedFrom");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TestRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PointerPressed");
        }

        private void TestRectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PointerReleased");
        }

        private void TestRectangle_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PointerExited");
        }

        private void TestRectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((App)App.Current).service.Hide(((App)App.Current).processId);
        }

        private void InnerRectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((App)App.Current).service.Show(((App)App.Current).processId);
        }
    }
}
