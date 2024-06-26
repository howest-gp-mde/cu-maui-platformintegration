﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.PlatformIntegration.Domain.Models;
using Mde.PlatformIntegration.Domain.Services;

namespace Mde.PlatformIntegration.ViewModels
{
    public class ProfileViewModel : ObservableObject
    {
        private readonly INativeAuthentication localAuthentication;
        private readonly IProfileService profileService;
        private readonly IDialogService dialogService;

        public const string RetrievalAuthenticationPrompt = "Retrieving your profile data requires authentication";
        public const string SaveSuccesfulMessage = "Your profile data was saved securely";
        public const string RemovalAuthenticationPrompt = "Confirm your identity to remove your profile data";
        public const string DeletionSuccessful = "Your profile data has been removed";
        public const string ConfirmDeletionTitle = "Confirm deletion";
        public const string ConfirmDeletionDescription = "Are you sure you want to delete your profile data?";
        public const string AuthenticationFailedMessage = "Authentication failed, please try again";

        public ProfileViewModel(INativeAuthentication localAuthentication, IProfileService profileService, IDialogService dialogService)
        {
            this.localAuthentication = localAuthentication;
            this.profileService = profileService;
            this.dialogService = dialogService;
            SaveCommand = new Command(OnSaveCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            AppearingCommand = new Command(OnAppearing);

            currentProfile = new Profile();
        }

        private Profile currentProfile;

        public string FirstName
        {
            get { return currentProfile.FirstName; }
            set
            {
                currentProfile.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return currentProfile.LastName; }
            set
            {
                currentProfile.LastName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return currentProfile.Email; }
            set
            {
                currentProfile.Email = value;
                OnPropertyChanged();
            }
        }
        public bool ConsentAnalytics
        {
            get { return currentProfile.ConsentAnalytics; }
            set
            {
                currentProfile.ConsentAnalytics = value;
                OnPropertyChanged();
            }
        }
        public bool ConsentPersonalized
        {
            get { return currentProfile.ConsentPersonalized; }
            set
            {
                currentProfile.ConsentPersonalized = value;
                OnPropertyChanged();
            }
        }

        private bool isAuthenticated;

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set {
                SetProperty(ref isAuthenticated, value);
            }
        }


        public Command SaveCommand { get; }

        private async void OnSaveCommand()
        {
            await profileService.SaveProfileAsync(currentProfile);
            await dialogService.ShowToast(SaveSuccesfulMessage);
            await Refresh();
        }

        public Command DeleteCommand { get; }

        private async void OnDeleteCommand()
        {
            bool confirmDeletion;
            if (localAuthentication.IsSupported())
            {
                var result = await localAuthentication.PromptLoginAsync(RemovalAuthenticationPrompt);
                confirmDeletion = result.Authenticated;
            }
            else
            {
                //fallback for unsupported devices 
                confirmDeletion = await dialogService.ShowConfirmationAsync(ConfirmDeletionTitle, ConfirmDeletionDescription);
            }
            
            if (confirmDeletion)
            {
                await profileService.DeleteProfileAsync(currentProfile);
                await dialogService.ShowToast(DeletionSuccessful);
                await Refresh();
            }
            else
            {
                await dialogService.ShowToast(AuthenticationFailedMessage);
            }
        }

        public Command AppearingCommand { get; }

        private async void OnAppearing()
        {
            if(localAuthentication.IsSupported())
            {
                var result = await localAuthentication.PromptLoginAsync(RetrievalAuthenticationPrompt);
                IsAuthenticated = result.Authenticated;
            }
            else
            {
                //auth not supported, assume authenticated
                IsAuthenticated = true;
            }

            if (IsAuthenticated)
            {
                await Refresh();
            }
            else
            {
                //go back
                await dialogService.ShowToast(AuthenticationFailedMessage);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async Task Refresh()
        {
            currentProfile = await profileService.GetProfileAsync() ?? new Profile();
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(ConsentAnalytics));
            OnPropertyChanged(nameof(ConsentPersonalized));
        }

    }
}
