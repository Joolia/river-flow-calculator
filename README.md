# River Flow Calculator
This solution contains RiverFlowCalculator - a class that calculates the volume of water flowing through a specific point in a stream based on cross-sectional measurements of depth and velocity.
The `Calculate` method provides an estimation of the flow rate by considering the average depth between measurement points.
Unit tests are added for verifying calculation and checking input validation.

## Description 
The `RiverFlowCalculator` class is designed to calculate the flow rate of a river by taking into account the width of the stream and a series of measurements collected along the cross-sectional area. Each measurement contains the depth and velocity values recorded at specific points across the river.

## Assumptions
- Water flows in one direction, and therefore, the velocity cannot be negative.
- Zero depth is a valid input for the first and the last measurements, indicating that the water level is at ground level in those sections. However, in this case, the velocity cannot be zero.
- Using average depth values between measurement points allows for a more precise estimation of the flow, considering the irregular curve of the river bottom, compared to using only the depth at the boundary points.
