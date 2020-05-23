using System;
using bcycle_backend.Models.Entities;

namespace bcycle_backend.Models.Responses.Views
{
    public class Measures
    {
        private const int SecondsInHour = 60 * 60;
        public double Time { get; }
        public double Distance { get; }
        public double Pace { get; }
        public double AvgSpeed { get; }
        public DateTime Started { get; }
        public DateTime Finished { get; }

        public Measures(Trip trip)
        {
            Distance = trip.Distance;
            Time = trip.Time;
            Pace = trip.Time / Distance;
            AvgSpeed = Distance * SecondsInHour / trip.Time;
            Started = trip.Started;
            Finished = trip.Finished;
        }
    }
}
