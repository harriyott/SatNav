using System;

namespace SatNav
{
    public static class TapeMeasure
    {
        public static decimal GetDistanceBetweenPoints(int startX, int endX, int startY, int endY)
        {
            if (startX == endX) return Math.Abs(startY - endY);
            if (startY == endY) return Math.Abs(startX - endX);

            // Pythagorus
            var width = Math.Abs(startX - endX);
            var height = Math.Abs(startY - endY);
            return (decimal) Math.Sqrt(width*width + height*height);
        }
    }
}