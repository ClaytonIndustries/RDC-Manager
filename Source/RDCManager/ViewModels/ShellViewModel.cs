﻿using Caliburn.Micro;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class ShellViewModel : Screen
    {
        public IScreen RDCListVM
        {
            get;
            private set;
        }

        public ShellViewModel()
        {
            DisplayName = "RDC Manager";

            RDCListVM = new RDCListViewModel(new EventAggregator(), new RDCStarter());
        }
    }
}