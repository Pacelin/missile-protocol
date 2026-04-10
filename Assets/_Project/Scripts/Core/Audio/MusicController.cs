using System.Collections.Generic;
using Plugins.Audio;

namespace Project.Core.Audio
{
    public static class MusicController
    {
        private static readonly Dictionary<EMusicTrack, ISoundEventInstance> _activeMusic = new();
        private static readonly Dictionary<EMusicTrack, ISoundEvent> _selectedMusic = new();
        
        public static void Set(EMusicTrack id, ISoundEvent soundEvent)
        {
            if (_selectedMusic.TryGetValue(id, out var currentSelected) &&
                currentSelected == soundEvent)
                return;

            Stop(id);
            _selectedMusic[id] = soundEvent;
            _activeMusic[id] = soundEvent.CreateInstance();
            _activeMusic[id].Start();
        }
        
        public static void Stop(EMusicTrack id)
        {
            if (!_activeMusic.ContainsKey(id))
                return;
            
            _activeMusic[id].Stop(true);
            _activeMusic[id].Release();
            _activeMusic.Remove(id);
            _selectedMusic.Remove(id);
        }

        public static void StopAll()
        {
            foreach (var music in _activeMusic.Values)
            {
                music.Stop(true);
                music.Release();
            }
            
            _activeMusic.Clear();
            _selectedMusic.Clear();
        }
    }
}
