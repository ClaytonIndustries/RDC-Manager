﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RDCManager.Controls;
using RDCManager.Messages;
using RDCManager.Models;

namespace RDCManager.ViewModels
{
    public class RDCSessionViewModel : Screen, IHandle<RDCSelectedMessage>, IHandle<StopRDCMessage>
    {
        private RDC _selectedRDC;
        public RDC SelectedRDC
        {
            get { return _selectedRDC; }
            set { _selectedRDC = value; NotifyOfPropertyChange(() => SelectedRDC); }
        }

        private ObservableCollection<UserAccount> _userAccounts;
        public ObservableCollection<UserAccount> UserAccounts
        {
            get { return _userAccounts; }
            set { _userAccounts = value; NotifyOfPropertyChange(() => UserAccounts); }
        }

        private UserAccount _selectedUserAccount;
        public UserAccount SelectedUserAccount
        {
            get { return _selectedUserAccount; }
            set
            {
                _selectedUserAccount = value;
                SelectedUserAccountChanged();
                NotifyOfPropertyChange(() => SelectedUserAccount);
                NotifyOfPropertyChange(() => ManualUserAccountEntryEnabled);
            }
        }

        private ObservableCollection<RDCGroup> _rdcGroups;
        public ObservableCollection<RDCGroup> RDCGroups
        {
            get { return _rdcGroups; }
            private set { _rdcGroups = value; NotifyOfPropertyChange(() => RDCGroups); }
        }

        private RDCGroup _selectedRDCGroup;
        public RDCGroup SelectedRDCGroup
        {
            get { return _selectedRDCGroup; }
            set { _selectedRDCGroup = value; SelectedRDCGroupChanged(); NotifyOfPropertyChange(() => SelectedRDCGroup); }
        }

        public bool ManualUserAccountEntryEnabled
        {
            get
            {
                return SelectedUserAccount != null && SelectedUserAccount.Id == Guid.Empty;
            }
        }

        private readonly IEventAggregator _events;
        private readonly ISnackbarMessageQueue _snackbarMessageQueue;
        private readonly IRDCInstanceManager _rdcInstanceManager;
        private readonly IUserAccountManager _userAccountManager;
        private readonly IRDCGroupManager _rdcGroupManager;

        public RDCSessionViewModel(IEventAggregator events, ISnackbarMessageQueue snackbarMessageQueue, IRDCInstanceManager rdcInstanceManager, IUserAccountManager userAccountManager,
            IRDCGroupManager rdcGroupManager)
        {
            _events = events;
            _events.Subscribe(this);

            _snackbarMessageQueue = snackbarMessageQueue;
            _rdcInstanceManager = rdcInstanceManager;
            _userAccountManager = userAccountManager;
            _rdcGroupManager = rdcGroupManager;
        }

        protected override void OnActivate()
        {
            if (SelectedRDC != null)
            {
                UserAccounts = new ObservableCollection<UserAccount>(_userAccountManager.GetUserAccounts());
                UserAccounts.Insert(0, new UserAccount() { Name = "Manual Entry", Id = Guid.Empty });

                RDCGroups = new ObservableCollection<RDCGroup>(_rdcGroupManager.GetGroups());
                RDCGroups.Insert(0, new RDCGroup() { Name = "None", Id = Guid.Empty });

                UserAccount userAccount = UserAccounts.FirstOrDefault(x => x.Id == SelectedRDC.UserAccountId);
                RDCGroup rdcGroup = RDCGroups.FirstOrDefault(x => x.Id == SelectedRDC.GroupId);

                if (userAccount != null)
                {
                    SelectedUserAccount = userAccount;
                }
                else
                {
                    SelectedRDC.UserAccountId = Guid.Empty;

                    SelectedUserAccount = UserAccounts.First();
                }

                if (rdcGroup != null)
                {
                    SelectedRDCGroup = rdcGroup;
                }
                else
                {
                    SelectedRDC.GroupId = Guid.Empty;

                    SelectedRDCGroup = RDCGroups.First();
                }
            }
        }

        public void Start()
        {
            if (SelectedRDC != null && !SelectedRDC.IsRunning)
            {
                SelectedRDC.Connect();
            }
        }

        public void Save()
        {
            string saveMessage = _rdcInstanceManager.Save() ? "Changes saved" : "Failed to save";

            _snackbarMessageQueue.Enqueue(saveMessage);
        }

        public async void Delete()
        {
            if (SelectedRDC != null)
            {
                object result = await DialogHost.Show(new Dialog());

                if ((bool)result)
                {
                    _rdcInstanceManager.Delete(SelectedRDC);
                    _rdcInstanceManager.Save();

                    SelectedRDC = null;

                    _snackbarMessageQueue.Enqueue("RDC deleted and changes saved");

                    _events.PublishOnUIThread(new SwitchViewMessage() { View = Models.View.Collection });
                }
            }
        }

        public void Handle(RDCSelectedMessage message)
        {
            if (message.SelectedRDC != null)
            {
                SelectedRDC = message.SelectedRDC;
            }
        }

        public void Handle(StopRDCMessage message)
        {
            if (SelectedRDC != null && SelectedRDC.IsRunning)
            {
                SelectedRDC.Disconnect();
            }
        }

        private void SelectedUserAccountChanged()
        {
            if (SelectedUserAccount != null)
            {
                SelectedRDC.UserAccountId = SelectedUserAccount.Id;

                if (!ManualUserAccountEntryEnabled)
                {
                    SelectedRDC.Username = SelectedUserAccount.Username;
                    SelectedRDC.Password = SelectedUserAccount.Password;
                    SelectedRDC.Domain = SelectedUserAccount.Domain;
                }
            }
        }

        private void SelectedRDCGroupChanged()
        {
            if (SelectedRDCGroup != null)
            {
                SelectedRDC.GroupId = SelectedRDCGroup.Id;
            }
        }
    }
}
