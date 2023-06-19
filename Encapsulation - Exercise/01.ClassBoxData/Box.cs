using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
{
    public class Box
    {
		private double length;
		private double width;
		private double height;

		public Box(double length, double width, double height)
		{
			Height = height;
			Width = width;
			Length = length;
		}

		public double Height
        {
			get { return height; }
			set 
			{
				ValidateNumber(value, "Height");
				height = value;
			}
		}


		public double Width
        {
			get { return width; }
			set
			{
				ValidateNumber(value, "Width");
				width = value;
			}
		}


		public double Length
        {
			get { return length; }
			set
			{
				ValidateNumber(value, "Length");
				length = value;
			}
		}

		public double SurfaceArea()
		{
			double result = (2 * Length * Width) + (2 * Length * Height) + (2 * Width * Height);
			return result;
		}

		public double Volume()
		{
			double result = Length * Width * Height;
			return result;
		}

		public double LateralSurfaceArea()
		{
			double result = (2 * Length * Height) + (2 * Width * Height);
			return result;
		}

		private void ValidateNumber(double value, string propertyName)
		{
			if (value <= 0)
			{
				 ArgumentException ex = new ArgumentException($"{propertyName} cannot be zero or negative.");
				throw ex;
			}
		}

	}
}
