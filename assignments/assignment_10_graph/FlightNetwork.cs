using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assignment10
{
    /// <summary>
    /// Main graph class for the flight network.
    /// Uses adjacency lists:
    /// - airports: all airport vertices
    /// - routes: all flights leaving each airport
    /// </summary>
    public class FlightNetwork
    {
        // All airports in the graph (vertex list)
        private readonly Dictionary<string, Airport> airports;

        // All outgoing flights from each airport (adjacency list)
        private readonly Dictionary<string, List<Flight>> routes;

        // Map airport code -> city (used when we auto-create airports)
        private static readonly Dictionary<string, string> airportCities =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "SEA", "Seattle" },
                { "PDX", "Portland" },
                { "SFO", "San Francisco" },
                { "LAX", "Los Angeles" },
                { "LAS", "Las Vegas" },
                { "PHX", "Phoenix" },
                { "DEN", "Denver" },
                { "DFW", "Dallas" },
                { "IAH", "Houston" },
                { "ORD", "Chicago" },
                { "MSP", "Minneapolis" },
                { "DTW", "Detroit" },
                { "ATL", "Atlanta" },
                { "MIA", "Miami" },
                { "JFK", "New York" },
                { "BOS", "Boston" }
            };

        /// <summary>
        /// Start with an empty network.
        /// </summary>
        public FlightNetwork()
        {
            airports = new Dictionary<string, Airport>(StringComparer.OrdinalIgnoreCase);
            routes = new Dictionary<string, List<Flight>>(StringComparer.OrdinalIgnoreCase);
        }

        #region LAB METHODS (already graded)

        /// <summary>
        /// Add an airport vertex to the graph.
        /// </summary>
        public void AddAirport(Airport airport)
        {
            if (airport == null || string.IsNullOrWhiteSpace(airport.Code))
            {
                Console.WriteLine("Cannot add airport: invalid airport object.");
                return;
            }

            string code = airport.Code.ToUpperInvariant();

            // If we already have this airport, do nothing
            if (airports.ContainsKey(code))
            {
                return;
            }

            airports[code] = airport;

            // Make sure this airport has an adjacency list, even if empty
            if (!routes.ContainsKey(code))
            {
                routes[code] = new List<Flight>();
            }
        }

        /// <summary>
        /// Add a directed flight edge to the graph.
        /// </summary>
        public void AddFlight(Flight flight)
        {
            if (flight == null ||
                string.IsNullOrWhiteSpace(flight.Origin) ||
                string.IsNullOrWhiteSpace(flight.Destination))
            {
                Console.WriteLine("Cannot add flight: invalid flight data.");
                return;
            }

            string origin = flight.Origin.ToUpperInvariant();
            string destination = flight.Destination.ToUpperInvariant();

            // Make sure origin airport exists
            if (!airports.ContainsKey(origin))
            {
                string city = airportCities.ContainsKey(origin)
                    ? airportCities[origin]
                    : origin;

                AddAirport(new Airport(origin, city));
            }

            // Make sure destination airport exists
            if (!airports.ContainsKey(destination))
            {
                string city = airportCities.ContainsKey(destination)
                    ? airportCities[destination]
                    : destination;

                AddAirport(new Airport(destination, city));
            }

            // Make sure origin has a list in routes
            if (!routes.ContainsKey(origin))
            {
                routes[origin] = new List<Flight>();
            }

            routes[origin].Add(flight);
        }

        /// <summary>
        /// Load flights from a CSV file and build the graph.
        /// CSV format: Origin,Destination,Airline,Duration,Cost
        /// </summary>
        public void LoadFlightsFromCSV(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("CSV file not found.", filename);
            }

            string[] lines = File.ReadAllLines(filename);

            if (lines.Length <= 1)
            {
                Console.WriteLine("CSV file is empty or only has a header.");
                return;
            }

            int loaded = 0;

            for (int i = 1; i < lines.Length; i++) // skip header
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                try
                {
                    string[] parts = line.Split(',');

                    if (parts.Length < 5)
                    {
                        Console.WriteLine($"Skipping bad line #{i + 1}: not enough columns.");
                        continue;
                    }

                    string origin = parts[0].Trim();
                    string destination = parts[1].Trim();
                    string airline = parts[2].Trim();

                    if (!int.TryParse(parts[3].Trim(), out int duration))
                    {
                        Console.WriteLine($"Skipping bad line #{i + 1}: invalid duration.");
                        continue;
                    }

                    if (!decimal.TryParse(parts[4].Trim(), out decimal cost))
                    {
                        Console.WriteLine($"Skipping bad line #{i + 1}: invalid cost.");
                        continue;
                    }

                    Flight f = new Flight(origin, destination, airline, duration, cost);
                    AddFlight(f);
                    loaded++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line #{i + 1}: {ex.Message}");
                }
            }

            Console.WriteLine($"Loaded {loaded} flights from '{filename}'.");
        }

        /// <summary>
        /// Show all airports in the network with how many outgoing flights each has.
        /// </summary>
        public void DisplayAllAirports()
        {
            if (airports.Count == 0)
            {
                Console.WriteLine("No airports in the network.");
                return;
            }

            Console.WriteLine($"Total Airports: {airports.Count}");
            Console.WriteLine("Code  City                 Outgoing Flights");
            Console.WriteLine("----- -------------------- ----------------");

            foreach (Airport a in airports.Values.OrderBy(a => a.Code))
            {
                string code = a.Code;
                int count = routes.ContainsKey(code) ? routes[code].Count : 0;

                Console.WriteLine($"{code,-5} {a.City,-20} {count,16}");
            }
        }

        /// <summary>
        /// Find an airport vertex by its code.
        /// </summary>
        public Airport? GetAirport(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }

            string upper = code.ToUpperInvariant();
            return airports.ContainsKey(upper) ? airports[upper] : null;
        }

        #endregion

        #region BASIC SEARCH OPERATIONS

        /// <summary>
        /// Return all direct flights from origin to destination.
        /// </summary>
        public List<Flight> FindDirectFlights(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                return new List<Flight>();
            }

            string o = origin.ToUpperInvariant();
            string d = destination.ToUpperInvariant();

            if (!routes.ContainsKey(o))
            {
                return new List<Flight>();
            }

            return routes[o]
                .Where(f => f.Destination.Equals(d, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Return all direct destination codes from an origin (sorted).
        /// </summary>
        public List<string> GetDestinationsFrom(string origin)
        {
            if (string.IsNullOrWhiteSpace(origin))
            {
                return new List<string>();
            }

            string o = origin.ToUpperInvariant();

            if (!routes.ContainsKey(o))
            {
                return new List<string>();
            }

            return routes[o]
                .Select(f => f.Destination.ToUpperInvariant())
                .Distinct()
                .OrderBy(code => code)
                .ToList();
        }

        /// <summary>
        /// Find the cheapest *direct* flight between two airports.
        /// </summary>
        public Flight? FindCheapestDirectFlight(string origin, string destination)
        {
            List<Flight> options = FindDirectFlights(origin, destination);

            if (options.Count == 0)
            {
                return null;
            }

            return options
                .OrderBy(f => f.Cost)
                .First();
        }

        #endregion

        #region BFS PATHFINDING

        /// <summary>
        /// BFS: find any valid route (fewest edges level-by-level).
        /// Returns list of airport codes or null if no route.
        /// </summary>
        public List<string>? FindRoute(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                return null;
            }

            string start = origin.ToUpperInvariant();
            string end = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
            {
                return null;
            }

            if (start == end)
            {
                return new List<string> { start };
            }

            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, string> parents = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                if (!routes.ContainsKey(current))
                {
                    continue;
                }

                foreach (Flight f in routes[current])
                {
                    string neighbor = f.Destination.ToUpperInvariant();

                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    visited.Add(neighbor);
                    parents[neighbor] = current;
                    queue.Enqueue(neighbor);

                    if (neighbor == end)
                    {
                        // Reconstruct and return path
                        return ReconstructPath(parents, start, end);
                    }
                }
            }

            // No route
            return null;
        }

        /// <summary>
        /// Shortest route by number of stops (BFS).
        /// BFS already gives shortest hops, so we just call FindRoute.
        /// </summary>
        public List<string>? FindShortestRoute(string origin, string destination)
        {
            return FindRoute(origin, destination);
        }

        #endregion

        #region DIJKSTRA (CHEAPEST ROUTE)

        /// <summary>
        /// Find the lowest cost route using Dijkstra's algorithm.
        /// </summary>
        public List<string>? FindCheapestRoute(string origin, string destination)
        {
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            {
                return null;
            }

            string start = origin.ToUpperInvariant();
            string end = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
            {
                return null;
            }

            if (start == end)
            {
                return new List<string> { start };
            }

            // distance from start to each airport
            Dictionary<string, decimal> distances =
                new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            // parent map for path reconstruction
            Dictionary<string, string> parents =
                new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            HashSet<string> visited =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string code in airports.Keys)
            {
                distances[code] = decimal.MaxValue;
            }

            distances[start] = 0m;

            PriorityQueue<string, decimal> pq = new PriorityQueue<string, decimal>();
            pq.Enqueue(start, 0m);

            while (pq.Count > 0)
            {
                string current = pq.Dequeue();

                if (visited.Contains(current))
                {
                    continue;
                }

                visited.Add(current);

                if (current == end)
                {
                    // We reached destination with cheapest cost
                    return ReconstructPath(parents, start, end);
                }

                if (!routes.ContainsKey(current))
                {
                    continue;
                }

                foreach (Flight f in routes[current])
                {
                    string neighbor = f.Destination.ToUpperInvariant();

                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    decimal newCost = distances[current] + f.Cost;

                    if (newCost < distances[neighbor])
                    {
                        distances[neighbor] = newCost;
                        parents[neighbor] = current;
                        pq.Enqueue(neighbor, newCost);
                    }
                }
            }

            // No route
            return null;
        }

        #endregion

        #region EXTRA CREDIT: DFS WITH CONSTRAINTS

        /// <summary>
        /// Optional: find all routes that stay under maxStops and maxCost.
        /// Returns list of routes (each route is list of airport codes).
        /// </summary>
        public List<List<string>> FindRoutesByCriteria(
            string origin,
            string destination,
            int maxStops,
            decimal maxCost)
        {
            List<List<string>> results = new List<List<string>>();

            if (string.IsNullOrWhiteSpace(origin) ||
                string.IsNullOrWhiteSpace(destination) ||
                maxStops < 0 ||
                maxCost <= 0)
            {
                return results;
            }

            string start = origin.ToUpperInvariant();
            string end = destination.ToUpperInvariant();

            if (!airports.ContainsKey(start) || !airports.ContainsKey(end))
            {
                return results;
            }

            List<string> currentPath = new List<string> { start };
            HashSet<string> visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                start
            };

            DFSWithConstraints(start, end, maxStops, maxCost,
                0m, currentPath, visited, results);

            return results;
        }

        private void DFSWithConstraints(
            string current,
            string destination,
            int maxStops,
            decimal maxCost,
            decimal currentCost,
            List<string> currentPath,
            HashSet<string> visited,
            List<List<string>> validRoutes)
        {
            // If we reached the destination, store a copy
            if (current.Equals(destination, StringComparison.OrdinalIgnoreCase))
            {
                validRoutes.Add(new List<string>(currentPath));
                return;
            }

            // stops = number of edges = path length - 1
            if (currentPath.Count - 1 >= maxStops)
            {
                return;
            }

            if (!routes.ContainsKey(current))
            {
                return;
            }

            foreach (Flight f in routes[current])
            {
                string neighbor = f.Destination.ToUpperInvariant();
                decimal newCost = currentCost + f.Cost;

                if (newCost > maxCost || visited.Contains(neighbor))
                {
                    continue;
                }

                // choose
                currentPath.Add(neighbor);
                visited.Add(neighbor);

                // explore
                DFSWithConstraints(neighbor, destination, maxStops, maxCost,
                    newCost, currentPath, visited, validRoutes);

                // backtrack
                currentPath.RemoveAt(currentPath.Count - 1);
                visited.Remove(neighbor);
            }
        }

        #endregion

        #region NETWORK ANALYSIS

        /// <summary>
        /// Find the top N hub airports (most outgoing flights).
        /// </summary>
        public List<string> FindHubAirports(int topN)
        {
            if (topN <= 0 || routes.Count == 0)
            {
                return new List<string>();
            }

            return routes
                .OrderByDescending(kvp => kvp.Value.Count)
                .Take(topN)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        /// <summary>
        /// Build a human-readable statistics report about the network.
        /// </summary>
        public string CalculateNetworkStatistics()
        {
            if (airports.Count == 0)
            {
                return "No airports in the network.";
            }

            int totalAirports = airports.Count;
            int totalFlights = routes.Values.Sum(list => list.Count);

            double avgConnections =
                routes.Count == 0 ? 0.0 : (double)totalFlights / routes.Count;

            var allFlights = routes.Values.SelectMany(f => f).ToList();

            decimal avgCost =
                allFlights.Count == 0 ? 0m : allFlights.Average(f => f.Cost);

            double avgDuration =
                allFlights.Count == 0 ? 0.0 : allFlights.Average(f => f.Duration);

            int maxConnections =
                routes.Count == 0 ? 0 : routes.Max(kvp => kvp.Value.Count);

            int minConnections =
                routes.Count == 0 ? 0 : routes.Min(kvp => kvp.Value.Count);

            var mostConnected = routes
                .Where(kvp => kvp.Value.Count == maxConnections)
                .Select(kvp => kvp.Key)
                .OrderBy(code => code)
                .ToList();

            var leastConnected = routes
                .Where(kvp => kvp.Value.Count == minConnections)
                .Select(kvp => kvp.Key)
                .OrderBy(code => code)
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Flight Network Statistics ===");
            sb.AppendLine($"Total Airports: {totalAirports}");
            sb.AppendLine($"Total Flights: {totalFlights}");
            sb.AppendLine($"Average Connections Per Airport: {avgConnections:F2}");
            sb.AppendLine();
            sb.AppendLine($"Average Flight Cost: ${avgCost:F2}");
            sb.AppendLine($"Average Flight Duration: {avgDuration:F2} minutes ({avgDuration / 60:F2} hours)");
            sb.AppendLine();
            sb.AppendLine($"Most Connected Airports ({maxConnections} flights): {string.Join(", ", mostConnected)}");
            sb.AppendLine($"Least Connected Airports ({minConnections} flights): {string.Join(", ", leastConnected)}");

            return sb.ToString();
        }

        /// <summary>
        /// Airports with no incoming AND no outgoing flights.
        /// </summary>
        public List<string> FindIsolatedAirports()
        {
            List<string> isolated = new List<string>();

            if (airports.Count == 0)
            {
                return isolated;
            }

            // Build set of airports that have incoming flights
            HashSet<string> hasIncoming = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (List<Flight> list in routes.Values)
            {
                foreach (Flight f in list)
                {
                    hasIncoming.Add(f.Destination.ToUpperInvariant());
                }
            }

            // Check each airport
            foreach (string code in airports.Keys)
            {
                bool hasOutgoing = routes.ContainsKey(code) && routes[code].Count > 0;
                bool hasIncomingFlights = hasIncoming.Contains(code);

                if (!hasOutgoing && !hasIncomingFlights)
                {
                    isolated.Add(code);
                }
            }

            return isolated.OrderBy(code => code).ToList();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Rebuild a path from parents dictionary.
        /// </summary>
        protected List<string> ReconstructPath(
            Dictionary<string, string> parents,
            string start,
            string end)
        {
            List<string> path = new List<string>();
            string current = end;

            while (!current.Equals(start, StringComparison.OrdinalIgnoreCase))
            {
                path.Add(current);

                if (!parents.ContainsKey(current))
                {
                    // Something went wrong, no full path
                    return new List<string>();
                }

                current = parents[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Sum up the cost of all legs in a route.
        /// </summary>
        public decimal GetRouteCost(List<string> route)
        {
            if (route == null || route.Count < 2)
            {
                return -1;
            }

            decimal totalCost = 0m;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapest = FindCheapestDirectFlight(from, to);

                if (cheapest == null)
                {
                    return -1; // route is broken
                }

                totalCost += cheapest.Cost;
            }

            return totalCost;
        }

        /// <summary>
        /// Print a route and its flight details to the console.
        /// </summary>
        public void DisplayRoute(List<string> route)
        {
            if (route == null || route.Count == 0)
            {
                Console.WriteLine("No route to display.");
                return;
            }

            Console.WriteLine($"\nRoute: {string.Join(" → ", route)}");
            Console.WriteLine($"Total stops: {route.Count - 1}");

            if (route.Count < 2)
            {
                return;
            }

            Console.WriteLine("\nFlight Details:");

            decimal totalCost = 0m;
            int totalDuration = 0;

            for (int i = 0; i < route.Count - 1; i++)
            {
                string from = route[i];
                string to = route[i + 1];

                Flight? cheapest = FindCheapestDirectFlight(from, to);

                if (cheapest != null)
                {
                    Console.WriteLine($"  {i + 1}. {cheapest}");
                    totalCost += cheapest.Cost;
                    totalDuration += cheapest.Duration;
                }
            }

            Console.WriteLine($"\nTotal Cost: ${totalCost:F2}");
            Console.WriteLine($"Total Duration: {totalDuration} minutes ({totalDuration / 60}h {totalDuration % 60}m)");
        }

        #endregion
    }
}
