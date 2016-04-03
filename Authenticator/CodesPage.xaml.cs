﻿using Authenticator.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Authenticator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodesPage : Page
    {
        private EntryStorage entryStorage;
        private List<EntryBlock> codes;
        private Dictionary<Entry, EntryBlock> mappings;

        public CodesPage()
        {
            InitializeComponent();

            entryStorage = new EntryStorage();
            codes = new List<EntryBlock>();
            mappings = new Dictionary<Entry, EntryBlock>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OTP otp = new OTP("JBSWY3DPEHPK3PXP");

            foreach (Entry entry in entryStorage.Entries)
            {
                EntryBlock code = new EntryBlock(entry);
                code.DeleteRequested += Code_DeleteRequested;

                Codes.Children.Add(code);
                codes.Add(code);
                mappings.Add(entry, code);
            }

            StrechProgress.Begin();
            StrechProgress.Seek(new TimeSpan(0, 0, 30 - otp.RemainingSeconds));
        }

        private void Code_DeleteRequested(object sender, DeleteRequestEventArgs e)
        {
            entryStorage.Remove(e.Entry);
            Codes.Children.Remove(mappings.FirstOrDefault(m => m.Key == e.Entry).Value);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            foreach (EntryBlock code in codes)
            {
                code.InEditMode = !code.InEditMode;
            }
        }
    }
}
