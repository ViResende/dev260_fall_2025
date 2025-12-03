using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        // ✅ EXTRA CREDIT – ADVANCED QUEUE ANALYTICS
        private int peakCasual = 0;
        private int peakRanked = 0;
        private int peakQuick = 0;
        private List<double> waitTimes = new List<double>(); // seconds

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Player with username '{username}' already exists");

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        public List<Player> GetAllPlayers() => allPlayers.ToList();
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 ? matchHistory.Average(m => m.SkillDifference) : 0;
            var avgWait = waitTimes.Count > 0 ? waitTimes.Average() : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime:hh\\:mm\\:ss}

                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players
                - QuickPlay: {quickPlayQueue.Count} players

                📊 Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}

                📈 Advanced Analytics (Extra Credit):
                - Peak Casual Queue: {peakCasual}
                - Peak Ranked Queue: {peakRanked}
                - Peak QuickPlay Queue: {peakQuick}
                - Average Wait Time: {avgWait:F1}s
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        public void AddToQueue(Player player, GameMode mode)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            RemoveFromAllQueues(player);

            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    peakCasual = Math.Max(peakCasual, casualQueue.Count);
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    peakRanked = Math.Max(peakRanked, rankedQueue.Count);
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    peakQuick = Math.Max(peakQuick, quickPlayQueue.Count);
                    break;
            }

            player.JoinQueue();
        }

        public Match? TryCreateMatch(GameMode mode)
        {
            // Casual (FIFO)
            if (mode == GameMode.Casual)
            {
                if (casualQueue.Count < 2) return null;
                var p1 = casualQueue.Dequeue();
                var p2 = casualQueue.Dequeue();
                p1.LeaveQueue(); p2.LeaveQueue();
                return new Match(p1, p2, GameMode.Casual);
            }

            // Ranked (skill ±2)
            if (mode == GameMode.Ranked)
            {
                if (rankedQueue.Count < 2) return null;
                var list = rankedQueue.ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (CanMatchInRanked(list[i], list[j]))
                        {
                            var p1 = list[i];
                            var p2 = list[j];
                            rankedQueue.Clear();

                            foreach (var p in list.Where(p => p != p1 && p != p2))
                                rankedQueue.Enqueue(p);

                            p1.LeaveQueue(); p2.LeaveQueue();
                            return new Match(p1, p2, GameMode.Ranked);
                        }
                    }
                }
                return null;
            }

            // QuickPlay
            if (mode == GameMode.QuickPlay)
            {
                if (quickPlayQueue.Count < 2) return null;
                var list = quickPlayQueue.ToList();

                // Try skill match first
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (Math.Abs(list[i].SkillRating - list[j].SkillRating) <= 2)
                        {
                            var p1 = list[i];
                            var p2 = list[j];
                            quickPlayQueue.Clear();
                            foreach (var p in list.Where(p => p != p1 && p != p2))
                                quickPlayQueue.Enqueue(p);

                            p1.LeaveQueue(); p2.LeaveQueue();
                            return new Match(p1, p2, GameMode.QuickPlay);
                        }
                    }
                }

                // If queue > 4, allow any match for speed
                if (quickPlayQueue.Count > 4)
                {
                    var p1 = quickPlayQueue.Dequeue();
                    var p2 = quickPlayQueue.Dequeue();
                    p1.LeaveQueue(); p2.LeaveQueue();
                    return new Match(p1, p2, GameMode.QuickPlay);
                }
            }
            return null;
        }

        public void ProcessMatch(Match match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));

            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;

            // ✅ Record wait time analytics
            if (match.Player1.JoinedQueue != DateTime.MinValue)
                waitTimes.Add((match.MatchTime - match.Player1.JoinedQueue).TotalSeconds);
            if (match.Player2.JoinedQueue != DateTime.MinValue)
                waitTimes.Add((match.MatchTime - match.Player2.JoinedQueue).TotalSeconds);

            Console.WriteLine($"✅ Match Complete: {match}");
        }

        public void DisplayQueueStatus()
        {
            Console.WriteLine("\n📋 Current Queue Status");
            PrintQueue("Casual", casualQueue);
            PrintQueue("Ranked", rankedQueue);
            PrintQueue("QuickPlay", quickPlayQueue);
        }

        public void DisplayPlayerStats(Player player)
        {
            if (player == null) return;
            Console.WriteLine(player.ToDetailedString());

            bool inCasual = casualQueue.Contains(player);
            bool inRanked = rankedQueue.Contains(player);
            bool inQuick = quickPlayQueue.Contains(player);

            if (!inCasual && !inRanked && !inQuick)
                Console.WriteLine("Queue Status: Not in queue");
            else
            {
                string mode = inCasual ? "Casual" : inRanked ? "Ranked" : "QuickPlay";
                Console.WriteLine($"Queue Status: In {mode} (wait {player.GetQueueTime()})");
                Console.WriteLine($"Estimated Wait: {GetQueueEstimate(inCasual ? GameMode.Casual : inRanked ? GameMode.Ranked : GameMode.QuickPlay)}");
            }

            var recent = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .OrderByDescending(m => m.MatchTime)
                .Take(3)
                .ToList();

            if (recent.Count == 0)
                Console.WriteLine("Recent Matches: none");
            else
            {
                Console.WriteLine("Recent Matches:");
                foreach (var m in recent)
                {
                    var opponent = m.Player1 == player ? m.Player2 : m.Player1;
                    string result = (m.Winner == player) ? "Won" :
                                    (m.Loser == player) ? "Lost" : "Played";
                    Console.WriteLine($"- {m.Mode}: vs {opponent.Username} → {result}");
                }
            }
        }

        public string GetQueueEstimate(GameMode mode)
        {
            if (mode == GameMode.Casual)
            {
                if (casualQueue.Count >= 2) return "No wait";
                if (casualQueue.Count == 1) return "Short wait";
                return "Long wait";
            }

            if (mode == GameMode.QuickPlay)
            {
                if (quickPlayQueue.Count >= 2) return "No wait";
                if (quickPlayQueue.Count == 1) return "Short wait";
                return "Long wait";
            }

            if (mode == GameMode.Ranked)
            {
                if (rankedQueue.Count == 0) return "Long wait";
                if (rankedQueue.Count == 1) return "Short wait";

                var list = rankedQueue.ToList();
                for (int i = 0; i < list.Count; i++)
                    for (int j = i + 1; j < list.Count; j++)
                        if (CanMatchInRanked(list[i], list[j])) return "No wait";

                return "Long wait";
            }

            return "Long wait";
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        private bool CanMatchInRanked(Player p1, Player p2)
            => Math.Abs(p1.SkillRating - p2.SkillRating) <= 2;

        private void RemoveFromAllQueues(Player player)
        {
            casualQueue = new Queue<Player>(casualQueue.Where(p => p != player));
            rankedQueue = new Queue<Player>(rankedQueue.Where(p => p != player));
            quickPlayQueue = new Queue<Player>(quickPlayQueue.Where(p => p != player));
            player.LeaveQueue();
        }

        private static void PrintQueue(string name, Queue<Player> q)
        {
            Console.WriteLine($"\n• {name} ({q.Count} waiting)");
            if (q.Count == 0) { Console.WriteLine("  (empty)"); return; }

            int pos = 1;
            foreach (var p in q)
                Console.WriteLine($"  {pos:D2}. {p.Username} (S{p.SkillRating}) — wait {p.GetQueueTime()}");
        }
    }
}
