using UnityEngine;
using OxyPlot;
using OxyPlot.Series;
using System;
using OxyPlot.Axes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Noesis;
using System.ComponentModel;
using System.Linq;

public class PlotVM : MonoBehaviour, INotifyPropertyChanged
{
	/// <summary>
	/// Occurs when a property changes its value.
	/// </summary>
	public event PropertyChangedEventHandler PropertyChanged;

	public PlotModel PlotModel1 {get;set;}
	public PlotModel PlotModel2 {get;set;}
	public PlotModel PlotModel3 {get;set;}
	public PlotModel PlotModel4 {get;set;}

	private float offset1 {get;set;}
	float deltaTime = 0.0f;

	/// <summary>
	/// Animation test. FunctionSeries could probably update a lot more efficiently if implementing IEnumerable and
	/// setting itself as the series ItemsSource.
	/// </summary>
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		offset1 += 1.0f*deltaTime;

		this.PlotModel1 = new PlotModel { Title = "OxyTest" };
		this.PlotModel1.Series.Add(new FunctionSeries((d)=> Math.Cos(d+offset1), 0, 10, 0.1, "cos(x)"));
		this.PlotModel1.Series.Add(new FunctionSeries((d)=> Math.Sin(d+offset1), 0, 10, 0.1, "sin(x)"));

		NotifyPropertyChanged("PlotModel1");
	}

	private double GetValue(double d)
	{
		return Math.Cos(d)+offset1;
	}

	public PlotVM()
	{
		this.PlotModel1 = new PlotModel { Title = "OxyTest" };
		this.PlotModel1.Series.Add(new FunctionSeries(GetValue, 0, 10, 0.1, "cos(x)"));
		this.PlotModel1.Series.Add(new FunctionSeries((d)=> Math.Sin(d+offset1), 0, 10, 0.1, "sin(x)"));

		// Create some data
		this.Items = new List<Item>
		{
			new Item {Label = "Apples", Value1 = 37, Value2 = 12, Value3 = 19},
			new Item {Label = "Pears", Value1 = 7, Value2 = 21, Value3 = 9},
			new Item {Label = "Bananas", Value1 = 23, Value2 = 2, Value3 = 29}
		};

		// Create the plot model
		var tmp = new PlotModel { Title = "Bar series", LegendPlacement = LegendPlacement.Outside, LegendPosition = LegendPosition.RightTop, LegendOrientation = LegendOrientation.Vertical };

		// Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
		tmp.Axes.Add(new CategoryAxis { Position = AxisPosition.Left, ItemsSource = this.Items, LabelField = "Label" });
		tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, AbsoluteMinimum = 0 });

		// Add the series, note that the BarSeries are using the same ItemsSource as the CategoryAxis.
		tmp.Series.Add(new BarSeries { Title = "2009", ItemsSource = this.Items, ValueField = "Value1" });
		tmp.Series.Add(new BarSeries { Title = "2010", ItemsSource = this.Items, ValueField = "Value2" });
		tmp.Series.Add(new BarSeries { Title = "2011", ItemsSource = this.Items, ValueField = "Value3" });

		this.PlotModel2 = tmp;

		// Create some data
		this.Items2 = new List<BoxPlotItem>
		{
			new BoxPlotItem(1, 13.0, 15.5, 17.0, 18.5, 19.5) { Mean = 18.0 },
			new BoxPlotItem(2, 13.0, 15.5, 17.0, 18.5, 19.5),
			new BoxPlotItem(3, 12.0, 13.5, 15.5, 18.0, 20.0) { Mean = 14.5 },
			new BoxPlotItem(4, 12.0, 13.5, 15.5, 18.0, 20.0) { Mean = 14.5, Outliers = new List<double> { 11.0, 21.0, 21.5 } },
			new BoxPlotItem(5, 13.5, 14.0, 14.5, 15.5, 16.5) { Outliers = new List<double> { 17.5, 18.0, 19.0 } }
		};


		var model = new PlotModel { Title = "ContourSeries" };
		double x0 = -3.1;
		double x1 = 3.1;
		double y0 = -3;
		double y1 = 3;
		Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1)) - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y) - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
		var xx = ArrayBuilder.CreateVector(x0, x1, 100);
		var yy = ArrayBuilder.CreateVector(y0, y1, 100);
		var peaksData = ArrayBuilder.Evaluate(peaks, xx, yy);

		var cs = new ContourSeries
		{
			StrokeThickness = 4,
			Color = OxyColors.Black,
			LabelBackground = OxyColors.White,
			ColumnCoordinates = yy,
			RowCoordinates = xx,
			Data = peaksData
		};
		model.Series.Add(cs);

		this.PlotModel3 = model;
		this.PlotModel3.InvalidatePlot(true);

		// Create the plot model
		tmp = new PlotModel { Title = "BoxPlot series", LegendPlacement = LegendPlacement.Outside, LegendPosition = LegendPosition.RightTop, LegendOrientation = LegendOrientation.Vertical };

		// Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
		tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0.3, MaximumPadding = 0.3, AbsoluteMinimum = 0 });
		tmp.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0.3, MaximumPadding = 0.3, AbsoluteMinimum = 0 });

		// Add the series, note that the BarSeries are using the same ItemsSource as the CategoryAxis.
		tmp.Series.Add(new BoxPlotSeries { Title = "Values", ItemsSource = this.Items2, Fill = OxyColors.LightBlue });

		this.PlotModel4 = tmp;
		this.PlotModel4.InvalidatePlot(true);
	}

	public List<Item> Items { get; set; }

	public List<BoxPlotItem> Items2 { get; set; }

	public class Item
	{
		public string Label { get; set; }
		public double Value1 { get; set; }
		public double Value2 { get; set; }
		public double Value3 { get; set; }
	}

	/// <summary>
	/// Represents an example.
	/// </summary>
	public class Example
	{
		/// <summary>
		/// The d.
		/// </summary>
		private readonly double[,] D;

		/// <summary>
		/// The x.
		/// </summary>
		private readonly double[] X;

		/// <summary>
		/// The y.
		/// </summary>
		private readonly double[] Y;

		/// <summary>
		/// The z.
		/// </summary>
		private readonly double[] Z;

		/// <summary>
		/// Initializes a new instance of the <see cref="Example" /> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="minx">The minx.</param>
		/// <param name="maxx">The maxx.</param>
		/// <param name="dx">The dx.</param>
		/// <param name="miny">The miny.</param>
		/// <param name="maxy">The maxy.</param>
		/// <param name="dy">The dy.</param>
		/// <param name="minz">The minz.</param>
		/// <param name="maxz">The maxz.</param>
		/// <param name="dz">The dz.</param>
		/// <param name="f">The f.</param>
		/// <param name="formatString">The format string.</param>
		public Example(
			string title,
			double minx,
			double maxx,
			double dx,
			double miny,
			double maxy,
			double dy,
			double minz,
			double maxz,
			double dz,
			Func<double, double, double> f,
			string formatString = null)
		{
			this.Title = title;
			this.X = ArrayBuilder.CreateVector(minx, maxx, dx);
			this.Y = ArrayBuilder.CreateVector(miny, maxy, dy);
			this.Z = ArrayBuilder.CreateVector(minz, maxz, dz);
			this.D = ArrayBuilder.Evaluate(f, this.X, this.Y);
			this.FormatString = formatString;
		}

		/// <summary>
		/// Gets or sets the format string.
		/// </summary>
		/// <value>The format string.</value>
		public string FormatString { get; set; }

		/// <summary>
		/// Gets the plot model.
		/// </summary>
		/// <value>The plot model.</value>
		public PlotModel PlotModel
		{
			get
			{
				var m = new PlotModel { Title = this.Title };
				var cs = new ContourSeries
				{
					ColumnCoordinates = this.X,
					RowCoordinates = this.Y,
					Data = this.D,
					ContourLevels = this.Z,
					LabelFormatString = this.FormatString
				};
				cs.CalculateContours();
				m.Series.Add(cs);
				return m;
			}
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString()
		{
			return this.Title;
		}

	}
		

	/// <summary>
	/// Raise the PropertyChanged event
	/// </summary>
	/// <param name="propertyName">Name of the property that changed value.</param>
	public void NotifyPropertyChanged(string propertyName)
	{
		//Debug.Log(GetType().ToString() +  ".NotifyPropertyChanged(" + propertyName + ")");
		if(PropertyChanged !=null)
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	}


}