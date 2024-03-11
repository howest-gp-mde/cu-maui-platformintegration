using Mde.PlatformIntegration.Domain.Models;

namespace Mde.PlatformIntegration.Domain.Services
{
    public class BundledMusicService : IMusicService
    {
        public IEnumerable<GameTrack> GetGameMusic()
        {
            return
            [
                new GameTrack
                {
                    Title = "Lemmings",
                    AudioPath = "lemmings.mp3",
                    ImagePath = "lemmings.png",
                },
                new GameTrack
                {
                    Title = "A Link to the Past",
                    AudioPath = "alinktothepast.mp3",
                    ImagePath = "alinktothepast.png",
                },
                new GameTrack
                {
                    Title = "Cannon Fodder",
                    AudioPath = "cannonfodder.mp3",
                    ImagePath = "cannonfodder.png",
                },
                new GameTrack
                {
                    Title = "Shadow of the Beast",
                    AudioPath = "shadowofthebeast.mp3",
                    ImagePath = "shadowofthebeast.png",
                },
                new GameTrack
                {
                    Title = "Secret of Monkey Island",
                    AudioPath = "thesecretofmonkeyisland.mp3",
                    ImagePath = "thesecretofmonkeyisland.png",
                },
            ];
        }
    }
}
