﻿using PetFinderMAUI.Pages;

namespace PetFinderMAUI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new WelcomePage());
		}
	}
}