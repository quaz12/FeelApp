﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeelApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GetFloorPlanPage : ContentPage
	{
		public GetFloorPlanPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            //BindingContext = new FloorPageViewModel();
        }
	}
}