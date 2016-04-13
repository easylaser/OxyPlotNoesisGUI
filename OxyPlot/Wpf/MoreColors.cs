// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoreColors.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Defines additional <see cref="Colors" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Wpf
{
	using Noesis;
    using System.Windows.Media;

    /// <summary>
    /// Defines additional <see cref="Colors" />.
    /// </summary>
    public static class MoreColors
    {
        /// <summary>
        /// The undefined color.
        /// </summary>
        public static readonly Color Undefined = new Noesis.Color(0, 0, 0, 0);

        /// <summary>
        /// The automatic color.
        /// </summary>
		public static readonly Color Automatic = new Color(0, 0, 0, 1);
    }
}