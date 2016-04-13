// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackerControl.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   The tracker control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Wpf
{
	using Noesis;
	using DoubleCollection = System.Collections.Generic.List<double>;
	using PointCollection = System.Collections.Generic.List<Noesis.Point>;

    using System;
    using System.Windows;

    /// <summary>
    /// The tracker control.
    /// </summary>
    public class TrackerControl : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="HorizontalLineVisibility"/> dependency property.
        /// </summary>
        public static DependencyProperty HorizontalLineVisibilityProperty =
            DependencyProperty.Register(
                "HorizontalLineVisibility",
                typeof(Visibility),
                typeof(TrackerControl),
                new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="VerticalLineVisibility"/> dependency property.
        /// </summary>
        public static DependencyProperty VerticalLineVisibilityProperty =
            DependencyProperty.Register(
                "VerticalLineVisibility",
                typeof(Visibility),
                typeof(TrackerControl),
                new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Identifies the <see cref="LineStroke"/> dependency property.
        /// </summary>
        public static DependencyProperty LineStrokeProperty = DependencyProperty.Register(
            "LineStroke", typeof(Brush), typeof(TrackerControl), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="LineExtents"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LineExtentsProperty = DependencyProperty.Register(
            "LineExtents", typeof(OxyRect), typeof(TrackerControl), new PropertyMetadata(new OxyRect()));

        /// <summary>
        /// Identifies the <see cref="LineDashArray"/> dependency property.
        /// </summary>
        public static DependencyProperty LineDashArrayProperty = DependencyProperty.Register(
            "LineDashArray", typeof(DoubleCollection), typeof(TrackerControl), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="BorderEdgeMode"/> dependency property.
        /// </summary>
//        public static DependencyProperty BorderEdgeModeProperty = DependencyProperty.Register(
//            "BorderEdgeMode", typeof(EdgeMode), typeof(TrackerControl));

        /// <summary>
        /// Identifies the <see cref="ShowPointer"/> dependency property.
        /// </summary>
        public static DependencyProperty ShowPointerProperty = DependencyProperty.Register(
            "ShowPointer", typeof(bool), typeof(TrackerControl), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="CornerRadius"/> dependency property.
        /// </summary>
        public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(double), typeof(TrackerControl), new PropertyMetadata(0.0));

        /// <summary>
        /// Identifies the <see cref="Distance"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
            "Distance", typeof(double), typeof(TrackerControl), new PropertyMetadata(7.0));

        /// <summary>
        /// Identifies the <see cref="CanCenterHorizontally"/> dependency property.
        /// </summary>
        public static DependencyProperty CanCenterHorizontallyProperty =
            DependencyProperty.Register(
                "CanCenterHorizontally", typeof(bool), typeof(TrackerControl), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="CanCenterVertically"/> dependency property.
        /// </summary>
        public static DependencyProperty CanCenterVerticallyProperty =
            DependencyProperty.Register(
                "CanCenterVertically", typeof(bool), typeof(TrackerControl), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="Position"/> dependency property.
        /// </summary>
        public static DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position",
            typeof(ScreenPoint),
            typeof(TrackerControl),
            new PropertyMetadata(new ScreenPoint(), PositionChanged));

        /// <summary>
        /// The path part string.
        /// </summary>
        private const string PartPath = "PART_Path";

        /// <summary>
        /// The content part string.
        /// </summary>
        private const string PartContent = "PART_Content";

        /// <summary>
        /// The content container part string.
        /// </summary>
        private const string PartContentcontainer = "PART_ContentContainer";

        /// <summary>
        /// The horizontal line part string.
        /// </summary>
        private const string PartHorizontalline = "PART_HorizontalLine";

        /// <summary>
        /// The vertical line part string.
        /// </summary>
        private const string PartVerticalline = "PART_VerticalLine";

        /// <summary>
        /// The content.
        /// </summary>
        private ContentPresenter content;

        /// <summary>
        /// The horizontal line.
        /// </summary>
        private Line horizontalLine;

        /// <summary>
        /// The path.
        /// </summary>
        private Path path;

        /// <summary>
        /// The content container.
        /// </summary>
        private Grid contentContainer;

        /// <summary>
        /// The vertical line.
        /// </summary>
        private Line verticalLine;

        /// <summary>
        /// Initializes static members of the <see cref = "TrackerControl" /> class.
        /// </summary>
        static TrackerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(TrackerControl), new FrameworkPropertyMetadata(typeof(TrackerControl)));
        }

        /// <summary>
        /// Gets or sets BorderEdgeMode.
        /// </summary>
//        public EdgeMode BorderEdgeMode
//        {
//            get
//            {
//                return (EdgeMode)this.GetValue(BorderEdgeModeProperty);
//            }
//
//            set
//            {
//                this.SetValue(BorderEdgeModeProperty, value);
//            }
//        }

        /// <summary>
        /// Gets or sets HorizontalLineVisibility.
        /// </summary>
        public Visibility HorizontalLineVisibility
        {
            get
            {
                return (Visibility)this.GetValue(HorizontalLineVisibilityProperty);
            }

            set
            {
                this.SetValue(HorizontalLineVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets VerticalLineVisibility.
        /// </summary>
        public Visibility VerticalLineVisibility
        {
            get
            {
                return (Visibility)this.GetValue(VerticalLineVisibilityProperty);
            }

            set
            {
                this.SetValue(VerticalLineVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets LineStroke.
        /// </summary>
        public Brush LineStroke
        {
            get
            {
                return (Brush)this.GetValue(LineStrokeProperty);
            }

            set
            {
                this.SetValue(LineStrokeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets LineExtents.
        /// </summary>
        public OxyRect LineExtents
        {
            get
            {
                return (OxyRect)this.GetValue(LineExtentsProperty);
            }

            set
            {
                this.SetValue(LineExtentsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets LineDashArray.
        /// </summary>
        public DoubleCollection LineDashArray
        {
            get
            {
                return (DoubleCollection)this.GetValue(LineDashArrayProperty);
            }

            set
            {
                this.SetValue(LineDashArrayProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show a 'pointer' on the border.
        /// </summary>
        public bool ShowPointer
        {
            get
            {
                return (bool)this.GetValue(ShowPointerProperty);
            }

            set
            {
                this.SetValue(ShowPointerProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the corner radius (only used when ShowPoint=<c>false</c>).
        /// </summary>
        public double CornerRadius
        {
            get
            {
                return (double)this.GetValue(CornerRadiusProperty);
            }

            set
            {
                this.SetValue(CornerRadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the distance of the content container from the trackers Position.
        /// </summary>
        public double Distance
        {
            get
            {
                return (double)this.GetValue(DistanceProperty);
            }

            set
            {
                this.SetValue(DistanceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tracker can center its content box horizontally.
        /// </summary>
        public bool CanCenterHorizontally
        {
            get
            {
                return (bool)this.GetValue(CanCenterHorizontallyProperty);
            }

            set
            {
                this.SetValue(CanCenterHorizontallyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tracker can center its content box vertically.
        /// </summary>
        public bool CanCenterVertically
        {
            get
            {
                return (bool)this.GetValue(CanCenterVerticallyProperty);
            }

            set
            {
                this.SetValue(CanCenterVerticallyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets Position of the tracker.
        /// </summary>
        public ScreenPoint Position
        {
            get
            {
                return (ScreenPoint)this.GetValue(PositionProperty);
            }

            set
            {
                this.SetValue(PositionProperty, value);
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public void OnApplyTemplate()
        {
         //   base.OnApplyTemplate();
            this.path = this.GetTemplateChild(PartPath) as Path;
            this.content = this.GetTemplateChild(PartContent) as ContentPresenter;
            this.contentContainer = this.GetTemplateChild(PartContentcontainer) as Grid;
            this.horizontalLine = this.GetTemplateChild(PartHorizontalline) as Line;
            this.verticalLine = this.GetTemplateChild(PartVerticalline) as Line;

            if (this.contentContainer == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The TrackerControl template must contain a content container with name +'{0}'",
                        PartContentcontainer));
            }

            if (this.path == null)
            {
                throw new InvalidOperationException(
                    string.Format("The TrackerControl template must contain a Path with name +'{0}'", PartPath));
            }

            if (this.content == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The TrackerControl template must contain a ContentPresenter with name +'{0}'", PartContent));
            }

            this.UpdatePositionAndBorder();
        }

        /// <summary>
        /// Called when the position is changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void PositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((TrackerControl)sender).OnPositionChanged(e);
        }

        /// <summary>
        /// Called when the position is changed.
        /// </summary>
        /// <param name="dependencyPropertyChangedEventArgs">The dependency property changed event args.</param>
        // ReSharper disable once UnusedParameter.Local
        private void OnPositionChanged(DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            this.UpdatePositionAndBorder();
        }

        /// <summary>
        /// Update the position and border of the tracker.
        /// </summary>
        private void UpdatePositionAndBorder()
        {
            if (this.contentContainer == null)
            {
                return;
            }

			Canvas.SetLeft(this.contentContainer, (float)this.Position.X);
			Canvas.SetTop(this.contentContainer, (float)this.Position.Y);
            FrameworkElement parent = this;
            while (!(parent is Canvas) && parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
            }

            if (parent == null)
            {
                return;
            }

            // throw new InvalidOperationException("The TrackerControl must have a Canvas parent.");
            double canvasWidth = parent.ActualWidth;
            double canvasHeight = parent.ActualHeight;

			this.content.Measure(new Noesis.Size((float)canvasWidth, (float)canvasHeight));
            this.content.Arrange(new Noesis.Rect(0, 0, this.content.DesiredSize.Width, this.content.DesiredSize.Height));

            double contentWidth = this.content.DesiredSize.Width;
            double contentHeight = this.content.DesiredSize.Height;

            // Minimum allowed margins around the tracker
            const double MarginLimit = 10;

            var ha = HorizontalAlignment.Center;
            if (this.CanCenterHorizontally)
            {
                if (this.Position.X - (contentWidth / 2) < MarginLimit)
                {
                    ha = HorizontalAlignment.Left;
                }

                if (this.Position.X + (contentWidth / 2) > canvasWidth - MarginLimit)
                {
                    ha = HorizontalAlignment.Right;
                }
            }
            else
            {
                ha = this.Position.X < canvasWidth / 2 ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            }

            var va = VerticalAlignment.Center;
            if (this.CanCenterVertically)
            {
                if (this.Position.Y - (contentHeight / 2) < MarginLimit)
                {
                    va = VerticalAlignment.Top;
                }

                if (ha == HorizontalAlignment.Center)
                {
                    va = VerticalAlignment.Bottom;
                    if (this.Position.Y - contentHeight < MarginLimit)
                    {
                        va = VerticalAlignment.Top;
                    }
                }

                if (va == VerticalAlignment.Center && this.Position.Y + (contentHeight / 2) > canvasHeight - MarginLimit)
                {
                    va = VerticalAlignment.Bottom;
                }

                if (va == VerticalAlignment.Top && this.Position.Y + contentHeight > canvasHeight - MarginLimit)
                {
                    va = VerticalAlignment.Bottom;
                }
            }
            else
            {
                va = this.Position.Y < canvasHeight / 2 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
            }

            double dx = ha == HorizontalAlignment.Center ? -0.5 : ha == HorizontalAlignment.Left ? 0 : -1;
            double dy = va == VerticalAlignment.Center ? -0.5 : va == VerticalAlignment.Top ? 0 : -1;

            Thickness margin;
            this.path.Data = this.ShowPointer
                                 ? this.CreatePointerBorderGeometry(ha, va, contentWidth, contentHeight, out margin)
                                 : this.CreateBorderGeometry(ha, va, contentWidth, contentHeight, out margin);

            this.content.Margin = margin;

			this.contentContainer.Measure(new Noesis.Size((float)canvasWidth, (float)canvasHeight));
            var contentSize = this.contentContainer.DesiredSize;

            this.contentContainer.RenderTransform = new TranslateTransform
                {
				X = (float)dx * contentSize.Width,
				Y = (float)dy * contentSize.Height
                };

            var pos = this.Position;

            if (this.horizontalLine != null)
            {
                if (this.LineExtents.Width > 0)
                {
					this.horizontalLine.X1 = (float)this.LineExtents.Left;
					this.horizontalLine.X2 = (float)this.LineExtents.Right;
                }
                else
                {
                    this.horizontalLine.X1 = 0f;
					this.horizontalLine.X2 = (float)canvasWidth;
                }

				this.horizontalLine.Y1 = (float)pos.Y;
				this.horizontalLine.Y2 = (float)pos.Y;
            }

            if (this.verticalLine != null)
            {
                if (this.LineExtents.Width > 0)
                {
					this.verticalLine.Y1 = (float)this.LineExtents.Top;
					this.verticalLine.Y2 = (float)this.LineExtents.Bottom;
                }
                else
                {
                    this.verticalLine.Y1 = 0f;
					this.verticalLine.Y2 = (float)canvasHeight;
                }

				this.verticalLine.X1 = (float)pos.X;
				this.verticalLine.X2 = (float)pos.X;
            }
        }

        /// <summary>
        /// Create the border geometry.
        /// </summary>
        /// <param name="ha">The horizontal alignment.</param>
        /// <param name="va">The vertical alignment.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="margin">The margin.</param>
        /// <returns>The border geometry.</returns>
        private Geometry CreateBorderGeometry(
            HorizontalAlignment ha, VerticalAlignment va, double width, double height, out Thickness margin)
        {
			float m = (float)this.Distance;
            var rect = new Noesis.Rect(
				ha == HorizontalAlignment.Left ? m : 0f, va == VerticalAlignment.Top ? m : 0f, (float)width, (float)height);
            margin = new Thickness(
                ha == HorizontalAlignment.Left ? m : 0f,
                va == VerticalAlignment.Top ? m : 0f,
                ha == HorizontalAlignment.Right ? m : 0f,
                va == VerticalAlignment.Bottom ? m : 0f);
			return new RectangleGeometry { Rect = rect, RadiusX = (float)this.CornerRadius, RadiusY = (float)this.CornerRadius };
        }

        /// <summary>
        /// Create a border geometry with a 'pointer'.
        /// </summary>
        /// <param name="ha">The horizontal alignment.</param>
        /// <param name="va">The vertical alignment.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="margin">The margin.</param>
        /// <returns>The border geometry.</returns>
        private Geometry CreatePointerBorderGeometry(
            HorizontalAlignment ha, VerticalAlignment va, double width, double height, out Thickness margin)
        {
            Noesis.Point[] points = null;
			float m = (float)this.Distance;
            margin = new Thickness();

            if (ha == HorizontalAlignment.Center && va == VerticalAlignment.Bottom)
            {
                float x0 = 0f;
				float x1 = (float)width;
				float x2 =(float) (x0 + x1) / 2;
				float y0 = 0f;
				float y1 = (float)height;
                margin = new Thickness(0f, 0f, 0f, m);
                points = new[]
                    {
                        new Noesis.Point(x0, y0), new Noesis.Point(x1, y0), new Noesis.Point(x1, y1), new Noesis.Point(x2 + (m / 2), y1),
                        new Noesis.Point(x2, y1 + m), new Noesis.Point(x2 - (m / 2), y1), new Noesis.Point(x0, y1)
                    };
            }

            if (ha == HorizontalAlignment.Center && va == VerticalAlignment.Top)
            {
				float x0 = 0;
				float x1 = (float)width;
				float x2 = (x0 + x1) / 2;
				float y0 = m;
				float y1 = m + (float)height;
                margin = new Thickness(0, m, 0, 0);
                points = new[]
                    {
                        new Noesis.Point(x0, y0), new Noesis.Point(x2 - (m / 2), y0), new Noesis.Point(x2, 0), new Noesis.Point(x2 + (m / 2), y0),
                        new Noesis.Point(x1, y0), new Noesis.Point(x1, y1), new Noesis.Point(x0, y1)
                    };
            }

            if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Center)
            {
				float x0 = m;
				float x1 = m + (float)width;
				float y0 = 0;
				float y1 = (float)height;
				float y2 = (y0 + y1) / 2;
                margin = new Thickness(m, 0, 0, 0);
                points = new[]
                    {
                        new Noesis.Point(0, y2), new Noesis.Point(x0, y2 - (m / 2)), new Noesis.Point(x0, y0), new Noesis.Point(x1, y0),
                        new Noesis.Point(x1, y1), new Noesis.Point(x0, y1), new Noesis.Point(x0, y2 + (m / 2))
                    };
            }

            if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Center)
            {
				float x0 = 0;
				float x1 = (float)width;
				float y0 = 0;
				float y1 = (float)height;
				float y2 = (y0 + y1) / 2;
                margin = new Thickness(0, 0, m, 0);
                points = new[]
                    {
                        new Noesis.Point(x1 + m, y2), new Noesis.Point(x1, y2 + (m / 2)), new Noesis.Point(x1, y1), new Noesis.Point(x0, y1),
                        new Noesis.Point(x0, y0), new Noesis.Point(x1, y0), new Noesis.Point(x1, y2 - (m / 2))
                    };
            }

            if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Top)
            {
                m *= 0.67f;
				float x0 = m;
				float x1 = m + (float)width;
				float y0 = m;
				float y1 = m + (float)height;
                margin = new Thickness(m, m, 0f, 0f);
                points = new[]
                    {
                        new Noesis.Point(0, 0), new Noesis.Point(m * 2, y0), new Noesis.Point(x1, y0), new Noesis.Point(x1, y1), new Noesis.Point(x0, y1),
                        new Noesis.Point(x0, m * 2)
                    };
            }

            if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Top)
            {
                m *= 0.67f;
				float x0 = 0;
				float x1 = (float)width;
				float y0 = m;
				float y1 = m + (float)height;
                margin = new Thickness(0, m, m, 0);
                points = new[]
                    {
                        new Noesis.Point(x1 + m, 0), new Noesis.Point(x1, y0 + m), new Noesis.Point(x1, y1), new Noesis.Point(x0, y1),
                        new Noesis.Point(x0, y0), new Noesis.Point(x1 - m, y0)
                    };
            }

            if (ha == HorizontalAlignment.Left && va == VerticalAlignment.Bottom)
            {
                m *= 0.67f;
				float x0 = m;
				float x1 = m + (float)width;
				float y0 = 0;
				float y1 = (float)height;
                margin = new Thickness(m, 0, 0, m);
                points = new[]
                    {
                        new Noesis.Point(0, y1 + m), new Noesis.Point(x0, y1 - m), new Noesis.Point(x0, y0), new Noesis.Point(x1, y0),
                        new Noesis.Point(x1, y1), new Noesis.Point(x0 + m, y1)
                    };
            }

            if (ha == HorizontalAlignment.Right && va == VerticalAlignment.Bottom)
            {
                m *= 0.67f;
				float x0 = 0;
				float x1 = (float)width;
				float y0 = 0;
				float y1 = (float)height;
                margin = new Thickness(0, 0, m, m);
                points = new[]
                    {
                        new Noesis.Point(x1 + m, y1 + m), new Noesis.Point(x1 - m, y1), new Noesis.Point(x0, y1), new Noesis.Point(x0, y0),
                        new Noesis.Point(x1, y0), new Noesis.Point(x1, y1 - m)
                    };
            }

            if (points == null)
            {
                return null;
            }

			return null;

            var pc = new PointCollection(points.Length);
            foreach (var p in points)
            {
                pc.Add(p);
            }

           /* var segments = new PathSegmentCollection { new PolyLineSegment { Points = pc } };
            var pf = new PathFigure { StartPoint = points[0], Segments = segments, IsClosed = true };
            return new PathGeometry { Figures = new PathFigureCollection { pf } };*/
        }
    }
}