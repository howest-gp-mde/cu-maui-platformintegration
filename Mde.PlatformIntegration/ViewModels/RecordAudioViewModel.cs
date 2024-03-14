using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;
using System.Diagnostics;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Mde.PlatformIntegration.ViewModels
{
    public class RecordAudioViewModel : ObservableObject
    {
        private readonly IAudioManager audioManager;
        private readonly IDispatcherTimer recordingUpdateTimer;
        private readonly Stopwatch recordingStopwatch = new Stopwatch();
        private IAudioRecorder audioRecorder;
        private AsyncAudioPlayer audioPlayer;
        private IAudioSource audioSource = null;

        public RecordAudioViewModel(IAudioManager audioManager, IDispatcherTimer timer)
        {
            this.audioManager = audioManager;
            this.recordingUpdateTimer = timer;
            this.recordingUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000/20); // 0.02s per update (50 fps)
            this.recordingUpdateTimer.Tick += UpdateRecordingTime;

            RecordCommand = new Command(StartRecording, () => !IsRecording);
            StopRecordingCommand = new Command(StopRecording, () => IsRecording);
            PlayCommand = new Command(StartPlaying, () => !IsPlaying && HasAudioSource);
            StopPlayingCommand = new Command(StopPlaying, () => IsPlaying && HasAudioSource);
        }

        private void UpdateRecordingTime(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(RecordTime));
        }

        public double RecordTime
        {
            get => recordingStopwatch.ElapsedMilliseconds;
        }

        public bool HasAudioSource
        {
            get => audioSource != null;
        }

        public bool IsPlaying
        {
            get => audioPlayer?.IsPlaying ?? false;
        }

        public bool IsRecording
        {
            get => audioRecorder?.IsRecording ?? false;
        }

        public Command RecordCommand { get; }
        public Command StopRecordingCommand { get; }
        public Command PlayCommand { get; }
        public Command StopPlayingCommand { get; }

        private async void StartRecording()
        {
            var microphonePermission = await Permissions.CheckStatusAsync<Microphone>();
            if (microphonePermission != PermissionStatus.Granted)
            {
                microphonePermission = await Permissions.RequestAsync<Microphone>();
            }

            if (microphonePermission == PermissionStatus.Granted)
            {
                audioRecorder = audioManager.CreateRecorder();
                await audioRecorder.StartAsync();
            }

            recordingStopwatch.Restart();
            recordingUpdateTimer.Start();

            OnPropertyChanged(nameof(IsRecording));

            RecordCommand.ChangeCanExecute();
            StopRecordingCommand.ChangeCanExecute();
        }

        private async void StopRecording()
        {
            audioSource = await audioRecorder.StopAsync();

            recordingStopwatch.Stop();
            recordingUpdateTimer.Stop();

            OnPropertyChanged(nameof(IsRecording));

            RecordCommand.ChangeCanExecute();
            StopRecordingCommand.ChangeCanExecute();
            PlayCommand.ChangeCanExecute();
        }

        private void StartPlaying()
        {
            if (HasAudioSource)
            {
                audioPlayer = this.audioManager.CreateAsyncPlayer(((FileAudioSource)audioSource).GetAudioStream());

                _ = audioPlayer
                    .PlayAsync(CancellationToken.None)
                    .ContinueWith((task) => 
                    {
                        //notify UI that audio has finished playing
                        _ = MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            OnPropertyChanged(nameof(IsPlaying));
                            PlayCommand.ChangeCanExecute();
                            StopPlayingCommand.ChangeCanExecute();
                        });
                    });

                //notify UI that audio has started playing
                OnPropertyChanged(nameof(IsPlaying));
            }

            PlayCommand.ChangeCanExecute();
            StopPlayingCommand.ChangeCanExecute();
        }

        public void StopPlaying()
        {
            audioPlayer.Stop();

            OnPropertyChanged(nameof(IsPlaying));

            PlayCommand.ChangeCanExecute();
            StopPlayingCommand.ChangeCanExecute();
        }
        
    }
}
