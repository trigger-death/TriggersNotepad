﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace TriggersNotepad {
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : Window {
		public AboutWindow() {
			InitializeComponent();

			DateTime buildDate = GetLinkerTime(Assembly.GetExecutingAssembly());
			this.labelVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " Release";
			this.labelBuildDate.Content = buildDate.ToShortDateString() + " (" + buildDate.ToShortTimeString() + ")";
		}

		private DateTime GetLinkerTime(Assembly assembly, TimeZoneInfo target = null) {
			var filePath = assembly.Location;
			const int c_PeHeaderOffset = 60;
			const int c_LinkerTimestampOffset = 8;

			var buffer = new byte[2048];

			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				stream.Read(buffer, 0, 2048);

			var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
			var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

			var tz = target ?? TimeZoneInfo.Local;
			var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

			return localTime;
		}

		private void OnOKClicked(object sender, RoutedEventArgs e) {
			DialogResult = true;
		}
	}
}
