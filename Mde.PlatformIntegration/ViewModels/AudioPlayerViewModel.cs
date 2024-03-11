using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Mde.PlatformIntegration.Domain.Models;
using Mde.PlatformIntegration.Domain.Services;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.PlatformIntegration.ViewModels
{
    public class AudioPlayerViewModel : ObservableObject
    {
        private readonly IAudioManager audioManager;
        private readonly IDispatcherTimer timer;
        private readonly IMusicService musicService;
        private readonly IMessenger messenger;
        private AsyncAudioPlayer audioPlayer;
        private CancellationTokenSource cancellationTokenSource = new();

        public AudioPlayerViewModel(IAudioManager audioManager, IDispatcherTimer timer, IMusicService musicService, IMessenger messenger)
        {
            this.audioManager = audioManager;
            this.musicService = musicService;
            this.messenger = messenger;
            this.timer = timer;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += (s, e) =>
            {
                isPositionChangeSystemDriven = true;
                OnPropertyChanged(nameof(SongProgress));
                isPositionChangeSystemDriven = false;
            };
            timer.Start();
        }

        public double Volume
        {
            get => audioPlayer?.Volume ?? 1.0;
            set
            {
                if(audioPlayer != null)
                    audioPlayer.Volume = value;
            }
        }

        public double Speed
        {
            get => audioPlayer?.Speed ?? 1.0;
            set
            {
                if (audioPlayer != null)
                    audioPlayer.Speed = value;
            }
        }

        bool isPositionChangeSystemDriven;

        public double SongProgress
        {
            get {
                if (audioPlayer != null && audioPlayer.Duration > 0)
                {
                    return audioPlayer.CurrentPosition / audioPlayer.Duration;
                }
                return 0;
            }
            set
            {
                if (audioPlayer != null && 
                    audioPlayer.CanSeek &&
                    !isPositionChangeSystemDriven)
                {
                    audioPlayer.Seek(audioPlayer.Duration * value);
                }
            }
        }


        private ObservableCollection<GameTrack> music;
        public ObservableCollection<GameTrack> Music
        {
            get { return music; }
            set {
                SetProperty(ref music, value);
            }
        }

        private GameTrack currentSong;

        public GameTrack CurrentSong
        {
            get { return currentSong; }
            set { 
                SetProperty(ref currentSong, value);
            }
        }

        public bool IsPlaying => audioPlayer?.IsPlaying == true;

        public ICommand AppearingCommand => new Command(() =>
        {
            var gameTracks = musicService.GetGameMusic();
            Music = new ObservableCollection<GameTrack>(gameTracks);
        });

        public ICommand DisappearingCommand => new Command(() =>
        {
            CancelPlaybackCommand.Execute(null);
        });

        public ICommand CancelPlaybackCommand => new Command(() =>
        {
            if (audioPlayer?.IsPlaying == true)
                audioPlayer.Stop();

            OnPropertyChanged(nameof(IsPlaying));
        });

        public ICommand RepeatPlaybackCommand => new Command(async () =>
        {
            if (audioPlayer?.Duration > 0)
                await audioPlayer.PlayAsync(cancellationTokenSource.Token);

            OnPropertyChanged(nameof(IsPlaying));
        });

        public ICommand PlayAudioAssetCommand => new Command<GameTrack>(async (song) =>
        {
            CancelPlaybackCommand.Execute(null);

            CurrentSong = song;

            using (var audioStream = await FileSystem.OpenAppPackageFileAsync(song.AudioPath))
            {
                //create audio player with song
                audioPlayer = audioManager.CreateAsyncPlayer(audioStream);

                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(Speed));

                messenger.Send(new AudioIsPlayingMessage());

                _ = audioPlayer.PlayAsync(cancellationTokenSource.Token);

                OnPropertyChanged(nameof(IsPlaying));
            }

        });
    }
}
