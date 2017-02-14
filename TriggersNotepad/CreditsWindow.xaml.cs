﻿using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace TriggersNotepad {
	/// <summary>
	/// Interaction logic for CreditsWindow.xaml
	/// </summary>
	public partial class CreditsWindow : Window {
		public CreditsWindow() {
			InitializeComponent();
		}

		private void OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
			System.Diagnostics.Process.Start((sender as Hyperlink).NavigateUri.ToString());
		}

		private void OnOKClicked(object sender, RoutedEventArgs e) {
			DialogResult = true;
		}
	}
}
