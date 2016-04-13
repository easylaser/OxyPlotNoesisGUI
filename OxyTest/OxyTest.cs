using UnityEngine;
using OxyPlot;
using Noesis;

[UserControlSource("Assets/OxyTest/OxyTest.xaml")]
public class OxyTest : UserControl
{
	public void OnPostInit()
	{

		var go = GameObject.Find("Plot");
	
		this.DataContext = go.GetComponent<PlotVM>();

	}

}
