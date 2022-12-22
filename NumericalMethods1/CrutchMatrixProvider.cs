using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	internal class CrutchMatrixProvider : IMatrixProvider
	{
		public decimal[] GetLeftColumn()
		{
			return new decimal[10] { 8, 0, -9, -5, -10, -8, 8, -1, 6, -6 };
		}

		public decimal[] GetRightColumn()
		{
			return new decimal[10] { 0, -3, 4, 5, 9, 1, 8, -3, -6, 6 };
		}

		public decimal[] GetUpperDiagonal()
		{
			return new decimal[9] { 0, 6, -1, 10, 3, 4, -7, -8, 2 };
		}

		public decimal[] GetCentralDiagonal()
		{
			return new decimal[10] { 8, -8, -10, -6, -4, -2, 1, 1, 2, 6 };
		}

		public decimal[] GetLowerDiagonal()
		{
			return new decimal[9] { -5, 6, -3, 3, 1, -2, 4, 6, -6 };
		}

		public decimal[] GetFreeTerms()
		{
			return new decimal[10] { -2, -10, -15, -3, 22, -8, 42, -7, -18, 10 };
		}

		public decimal[] GetX()
		{
			return new decimal[10];
		}
		public uint GetSize()
		{
			return 10;
		}
	}
}
