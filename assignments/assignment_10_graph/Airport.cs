using System;
using System.Collections.Generic;

namespace Assignment10
{
    /// <summary>
    /// Represents an airport in the flight network graph.
    /// Each Airport object serves as a vertex in the graph structure.
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Three-letter IATA airport code (e.g., "SEA", "LAX", "JFK")
        /// This serves as the unique identifier for the airport
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Full name of the airport (e.g., "Seattle-Tacoma International Airport")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// City where the airport is located (e.g., "Seattle")
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country where the airport is located (e.g., "USA")
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Default constructor for creating an empty Airport object
        /// </summary>
        public Airport()
        {
            Code = string.Empty;
            Name = string.Empty;
            City = string.Empty;
            Country = "USA"; // Default to USA
        }

        // 👇 NEW simple constructor goes right here
        // This is the one FlightNetwork uses (code + city only)
        public Airport(string code, string city)
        {
            // store the code in upper-case (SEA, LAX, etc.)
            Code = code?.ToUpperInvariant() ?? string.Empty;

            // basic city info
            City = city ?? string.Empty;

            // simple default values so we always have something
            Name = $"{City} Airport";
            Country = "USA";
        }

        // existing full constructor stays the same
        public Airport(string code, string name, string city, string country = "USA")
        {
            Code = code?.ToUpperInvariant() ?? string.Empty;
            Name = name ?? string.Empty;
            City = city ?? string.Empty;
            Country = country ?? "USA";
        }
    }
}