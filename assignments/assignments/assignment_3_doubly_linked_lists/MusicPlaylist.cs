using System;
using System.Linq;
using Week4DoublyLinkedLists.Core;

namespace Week4DoublyLinkedLists.Applications
{
    // STEP 8: Song class (already provided)
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public Song(string title, string artist, TimeSpan duration, string album = "", int year = 0, string genre = "")
        {
            Title = title;
            Artist = artist;
            Duration = duration;
            Album = album;
            Year = year;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist} ({Duration:mm\\:ss})";
        }

        public string ToDetailedString()
        {
            return $"{Title} - {Artist} [{Album}, {Year}] ({Duration:mm\\:ss}) [{Genre}]";
        }
    }

    // STEP 9–11: Playlist class
    public class MusicPlaylist
    {
        private DoublyLinkedList<Song> playlist;
        private Node<Song>? currentSong;

        public string Name { get; set; }
        public int TotalSongs => playlist.Count;
        public bool HasSongs => playlist.Count > 0;
        public Song? CurrentSong => currentSong?.Data;

        public MusicPlaylist(string name = "My Playlist")
        {
            Name = name;
            playlist = new DoublyLinkedList<Song>();
            currentSong = null;
        }

        // Step 10a: Add songs
        public void AddSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            playlist.AddLast(song);
            if (currentSong == null)
                currentSong = playlist.First;
        }

        public void AddSongAt(int position, Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));
            if (position < 0 || position > TotalSongs)
                throw new ArgumentOutOfRangeException(nameof(position));

            playlist.Insert(position, song);
            if (currentSong == null)
                currentSong = playlist.First;
        }

        // Step 10b: Remove songs
        public bool RemoveSong(Song song)
        {
            if (song == null) throw new ArgumentNullException(nameof(song));

            var node = playlist.Find(song);
            if (node == null) return false;

            if (node == currentSong)
                currentSong = node.Next ?? node.Previous;

            return playlist.Remove(song);
        }

        public bool RemoveSongAt(int position)
        {
            if (position < 0 || position >= TotalSongs)
                return false;

            if (position == 0)
            {
                if (currentSong == playlist.First)
                    currentSong = playlist.First?.Next;
                playlist.RemoveFirst();
                return true;
            }

            if (position == TotalSongs - 1)
            {
                if (currentSong == playlist.Last)
                    currentSong = playlist.Last?.Previous;
                playlist.RemoveLast();
                return true;
            }

            // Middle removal
            playlist.RemoveAt(position);
            return true;
        }

        // Step 10c: Navigation
        public bool Next()
        {
            if (currentSong?.Next == null) return false;
            currentSong = currentSong.Next;
            return true;
        }

        public bool Previous()
        {
            if (currentSong?.Previous == null) return false;
            currentSong = currentSong.Previous;
            return true;
        }

        public bool JumpToSong(int position)
        {
            if (position < 0 || position >= TotalSongs)
                return false;

            var node = playlist.First;
            for (int i = 0; i < position; i++)
                node = node?.Next;

            currentSong = node;
            return node != null;
        }

        // Step 11: Display methods
        public void DisplayPlaylist()
        {
            Console.WriteLine($"=== {Name} ===");
            if (!HasSongs)
            {
                Console.WriteLine("[Empty playlist]");
                return;
            }

            int index = 1;
            foreach (var song in playlist)
            {
                string marker = (song == CurrentSong) ? "►" : " ";
                Console.WriteLine($"{marker} {index}. {song}");
                index++;
            }

            Console.WriteLine($"Total songs: {TotalSongs}");
        }

        public void DisplayCurrentSong()
        {
            if (CurrentSong == null)
            {
                Console.WriteLine("No song is currently selected.");
                return;
            }

            Console.WriteLine("=== CURRENT SONG ===");
            Console.WriteLine(CurrentSong.ToDetailedString());
            Console.WriteLine($"Position: {GetCurrentPosition()} of {TotalSongs}");
        }

        public Song? GetCurrentSong() => currentSong?.Data;

        // Helpers
        public int GetCurrentPosition()
        {
            if (currentSong == null) return 0;

            int pos = 1;
            var node = playlist.First;
            while (node != null && node != currentSong)
            {
                pos++;
                node = node.Next;
            }
            return pos;
        }

        public TimeSpan GetTotalDuration()
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var s in playlist)
                total += s.Duration;
            return total;
        }
    }
}
