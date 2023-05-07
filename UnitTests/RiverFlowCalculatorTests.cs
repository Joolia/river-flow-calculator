using RiverFlowCalculation.Services;
using RiverFlowCalculation.Models;
namespace UnitTests;

[TestClass]
public class RiverFlowCalculatorTests
{
    [TestMethod]
    public void GetRiverFLow_ValidInputIrregularShape_ReturnsRiverFlow()
    {
        // Arrange
        var riverWidthInMeters = 10.156;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.1, 0.65),
            new Measurement(2, 0.25, 0.67),
            new Measurement(3, 0.34, 0.68),
            new Measurement(4, 0.5, 0.6),
            new Measurement(5, 0.2, 0.6),
            new Measurement(6, 0.08, 0.3),
            new Measurement(7, 0, 0)
        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        var result = calculator.GetRiverFLow();

        // Assert
        Assert.AreEqual(1.2748319000000001, result);
    }

    [TestMethod]
    public void GetRiverFLow_ValidInputTriangleShape_ReturnsRiverFlow()
    {
        // Arrange
        var riverWidthInMeters = 10;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 0.65),
            new Measurement(2, 0, 0)
        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        var result = calculator.GetRiverFLow();

        // Assert
        Assert.AreEqual(0.8125, result);
    }

    [TestMethod]
    public void GetRiverFLow_ValidInputRectangular_ReturnsRiverFlow()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 1, 3),
            new Measurement(2, 1, 3),
            new Measurement(3, 0, 0)
        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        var result = calculator.GetRiverFLow();

        // Assert
        Assert.AreEqual(4.5, result);
    }

    #region Validation exceptions
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    [DataRow(0)]
    [DataRow(-1)]
    public void GetRiverFlow_InvalidWidth_ThrowsException(double width)
    {
        // Arrange
        var riverWidthInMeters = width;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 1, 3),
            new Measurement(2, 1, 3),
            new Measurement(3, 0, 0)
        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch(ArgumentException ex)
        {
            Assert.AreEqual("The width of the stream must be greater than zero.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_NoMeasurements_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;

        var calculator = new RiverFlowCalculator(riverWidthInMeters, new List<Measurement>());

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("At least two measurements are required.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_NullMeasurement_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, 0.6, 1),
            null

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement cannot be null.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_NegativeDepth_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, -0.6, 1),
            new Measurement(3, 0, 0),

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement #2: Stream depth cannot be negative.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_NegativeVelocity_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, 0.6, -1),
            new Measurement(3, 0, 0),

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement #2: Velocity cannot be negative.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_ZeroDepthInTheMiddleOfRiver_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, 0, 1),
            new Measurement(3, 0, 0),

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement #2: only first and last measurements can contain zero stream depth.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_NonZeroVelocityAtInitialMeasurement_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 1),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, 0.6, 1),
            new Measurement(3, 0, 0),

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement #0: Velocity cannot be other than zero if depth is zero.", ex.Message);
            throw;
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetRiverFlow_ZeroVelocityInTheMiddleOfRiver_ThrowsException()
    {
        // Arrange
        var riverWidthInMeters = 3;
        var measurements = new List<Measurement>
        {
            new Measurement(0, 0, 0),
            new Measurement(1, 0.5, 1.5),
            new Measurement(2, 0.6, 0),
            new Measurement(3, 0, 0),

        };

        var calculator = new RiverFlowCalculator(riverWidthInMeters, measurements);

        // Act
        try
        {
            var result = calculator.GetRiverFLow();
        }

        // Assert
        catch (ArgumentException ex)
        {
            Assert.AreEqual("Measurement #2: Velocity cannot be negative or zero when depth is more than zero.", ex.Message);
            throw;
        }
    }
    #endregion Validation exceptions
}
