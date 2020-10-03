using System;
using System.Collections.Generic;
using PesquisaCep.Mobile.ViewModels;
using PesquisaCep.Mobile.Views;
using Xamarin.Forms;

namespace PesquisaCep.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
