using Metrofier.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Metrofier.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        public readonly IMetrofierService service;
        const string address = "http://localhost:8000/Metrofier/service";
        public uint processId = 0;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            

            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IMetrofierService> factory = new ChannelFactory<IMetrofierService>(binding, new EndpointAddress(address));
            service = factory.CreateChannel();
        }

        public void CoreWindow_VisibilityChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.VisibilityChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("VisibilityChanged");

            if (args.Visible)
            {
                bool result = service.Show(processId);
            }
            else
            {
                bool result = service.Hide(processId);
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            System.Diagnostics.Debug.WriteLine("OnLaunched");
            processId = service.Start("C:\\Windows\\System32\\notepad.exe", "C:\\Windows\\System32");
            // Ensure the current window is active
            Window.Current.Activate();

            Window.Current.CoreWindow.VisibilityChanged += CoreWindow_VisibilityChanged;
            Window.Current.CoreWindow.Closed += CoreWindow_Closed;
            Window.Current.CoreWindow.SizeChanged += CoreWindow_SizeChanged;

            service.Resize(processId,(int)Window.Current.CoreWindow.Bounds.Width, (int)Window.Current.CoreWindow.Bounds.Height);

            EdgeGesture edgeGesture = EdgeGesture.GetForCurrentView();
            edgeGesture.Starting += edgeGesture_Starting;
        }

        void edgeGesture_Starting(EdgeGesture sender, EdgeGestureEventArgs args)
        {
            service.Hide(processId);
        }

        void CoreWindow_SizeChanged(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.WindowSizeChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("SizeChanged");
            service.Resize(processId, (int)Window.Current.CoreWindow.Bounds.Width, (int)Window.Current.CoreWindow.Bounds.Height);
        }

        void CoreWindow_Closed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CoreWindowEventArgs args)
        {
            service.Close(processId);
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("OnActivated");
            base.OnActivated(args);

            //service.Start("C:\\Program Files (x86)\\XBMC\\XBMC.exe", "C:\\Program Files (x86)\\XBMC");
            //service.Show(processId);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnSuspending");
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
