using System;

namespace RiverFlowCalculation.Models
{
    /// <summary>
    /// Represents a depth and velocity measurement at a specific point.
    /// </summary>
    public class Measurement
    {
        /// <summary>
        /// The unique identifier of the measurement.
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// The depth at a specific point, in meters.
        /// </summary>
        public double Depth { get; }
        /// <summary>
        /// The velocity of the stream at a specific point, in meters per second.
        /// </summary>
        public double Velocity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement"/> class.
        /// </summary>
        public Measurement(int id, double depth, double velocity) {
            Id = id;
            Depth = depth;
            Velocity = velocity;
        }

    }
}

