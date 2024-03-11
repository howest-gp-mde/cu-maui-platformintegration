using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.PlatformIntegration.Domain.Models;

namespace Mde.PlatformIntegration.Domain.Services
{
    public interface IMusicService
    {
        /// <summary>
        /// Retrieves a collections of classic game themes
        /// </summary>
        /// <returns></returns>
        IEnumerable<GameTrack> GetGameMusic();
    }
}
