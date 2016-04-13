﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OxyColorConverter.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Converts between <see cref="OxyColor" /> and <see cref="Color" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using Noesis;

namespace OxyPlot.Wpf
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between <see cref="OxyColor" /> and <see cref="Color" />.
    /// </summary>
    //[ValueConversion(typeof(OxyColor), typeof(Color))]
    public class OxyColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OxyColor)
            {
                var color = (OxyColor)value;
                if (targetType == typeof(Color))
                {
                    return color.ToColor();
                }

                if (targetType == typeof(Brush))
                {
                    return color.ToBrush();
                }
            }

            return null;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <c>null</c>, the valid <c>null</c> value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(OxyColor))
            {
                if (value is Color)
                {
                    var color = (Color)value;
					return OxyColor.FromArgb(FloatToByte(color.A), FloatToByte(color.R), FloatToByte(color.G), FloatToByte(color.B));
                }

                var brush = value as SolidColorBrush;
                if (brush != null)
                {
					return OxyColor.FromArgb(FloatToByte(brush.Color.A), FloatToByte(brush.Color.R), FloatToByte(brush.Color.G), FloatToByte(brush.Color.B));
                }
            }

            return null;
        }

		private byte FloatToByte(float f)
		{
			float f2 = Math.Max(0.0f, Math.Min(1.0f, f));
			return (byte)Math.Floor(f2 == 1.0 ? 255 : f2 * 256.0);
		}
    }
}