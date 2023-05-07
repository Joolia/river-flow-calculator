using System;
using RiverFlowCalculation.Models;

namespace RiverFlowCalculation.Services
{
    /// <summary>
    /// Calculates the volume of water that moves through a specific point in a stream.
    /// </summary>
    public class RiverFlowCalculator
	{
        /// <summary>
        /// The width of the stream, in meters.
        /// </summary>
        public double Width { get; }
        /// <summary>
        /// The list of depth and velocity measurements taken in multiple points of a cross-section of a river stream.
        /// </summary>
        public List<Measurement> Measurements { get; }

        /// <summary>
        /// Constructs a new RiverFlowCalculator with the given width and list of depth and velocity measurements.
        /// </summary>
        /// <param name="width">The width of the stream, in meters.</param>
        /// <param name="measurements">The list of depth and velocity measurements.</param>
        /// <exception cref="ArgumentException">Thrown if the width is less than or equal to zero,
        /// or if there are less than two depth and velocity measurements,
        /// or if the measurements are not valid.</exception>
        public RiverFlowCalculator(double width, List<Measurement> measurements)
        {
            ValidateInput(width, measurements);

            Width = width;
            Measurements = measurements;
        }

        /// <summary>
        /// Calculates the volume of water that moves through a specific point in a stream.
        /// </summary>
        /// <returns>The volume of water that moves through the stream, in cubic meters per second.</returns>
        public double GetRiverFLow()
        {
            double sectionWidth = Width / (Measurements.Count - 1);
            double totalFlow = 0;

            for (int i = 0; i < Measurements.Count - 1; i++)
            {
                double sectionDepth = (Measurements[i].Depth + Measurements[i + 1].Depth) / 2;
                double sectionArea = sectionWidth * sectionDepth;
                double sectionVelocity = (Measurements[i].Velocity + Measurements[i + 1].Velocity) / 2;
                double sectionFlow = sectionArea * sectionVelocity;
                totalFlow += sectionFlow;
            }

            return totalFlow;
        }

		private static void ValidateInput(double width, List<Measurement> measurements)
		{
            if (width <= 0)
            {
                throw new ArgumentException("The width of the stream must be greater than zero.");
            }

            if (measurements == null || measurements.Count < 2)
			{
                throw new ArgumentException("At least two measurements are required.");
            }

            if (measurements.Any(x => x == null))
            {
                throw new ArgumentException($"Measurement cannot be null.");
            }

            var firstMeasurementId = measurements.First().Id;
            var lastMeasurementId = measurements.Last().Id;

            foreach (var m in measurements)
            {
                if (m.Depth < 0)
                {
                    throw new ArgumentException($"Measurement #{m.Id}: Stream depth cannot be negative.");
                }

                if (m.Velocity < 0)
                {
                    throw new ArgumentException($"Measurement #{m.Id}: Velocity cannot be negative.");
                }

                if (m.Depth == 0)
                {
                    if (m.Id != firstMeasurementId && m.Id != lastMeasurementId)
                    {
                        throw new ArgumentException($"Measurement #{m.Id}: only first and last measurements can contain zero stream depth.");
                    }

                    if (m.Velocity != 0)
                    {
                        throw new ArgumentException($"Measurement #{m.Id}: Velocity cannot be other than zero if depth is zero.");
                    }
                }

                if (m.Depth != 0 && m.Velocity == 0)
                {
                    throw new ArgumentException($"Measurement #{m.Id}: Velocity cannot be zero when depth is more than zero.");
                }
            }
		}
	}
}

