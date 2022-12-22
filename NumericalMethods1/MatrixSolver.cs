using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	internal class MatrixSolver
	{
		public MatrixSolver(IMatrixProvider matrixProvider)
		{
			_leftColumn = matrixProvider.GetLeftColumn();			// p
			_rightColumn = matrixProvider.GetRightColumn();			// q
			_upperDiagonal = matrixProvider.GetUpperDiagonal();     // a
			_centralDiagonal = matrixProvider.GetCentralDiagonal(); // b
			_lowerDiagonal = matrixProvider.GetLowerDiagonal();     // c

			_freeTerms = matrixProvider.GetFreeTerms();				// f

			_size = matrixProvider.GetSize();

			_x = new decimal[matrixProvider.GetSize()];

		}
		public void Print()
		{
			uint row = _size - 1;
			Console.Write($"{string.Format("{0:f4}",_leftColumn[row]), 10}\t");
			for (int i = 0; i < row - 2; i++)
				Console.Write($"{0, 10}\t");

			Console.WriteLine($"{string.Format("{0:f4}", _upperDiagonal[row - 1]), 10}\t{string.Format("{0:f4}", _centralDiagonal[row]), 10}\t\t{string.Format("{0:f4}", _freeTerms[row]), 10}");


			row = _size - 2;
			Console.Write($"{string.Format("{0:f4}", _leftColumn[row]),10}\t");
			for (int i = 0; i < row - 2; i++)
				Console.Write($"{0, 10}\t");

			Console.WriteLine($"{string.Format("{0:f4}", _upperDiagonal[row - 1]),10}\t{string.Format("{0:f4}", _centralDiagonal[row]),10}\t{string.Format("{0:f4}", _lowerDiagonal[row]),10}\t\t{string.Format("{0:f4}", _freeTerms[row]),10}");


			for (row = _size - 3; row >= 2; row--)
			{
				Console.Write($"{string.Format("{0:f4}", _leftColumn[row]),10}\t");

				for (int i = 0; i < row - 2; i++)
					Console.Write($"{0,10}\t");

				Console.Write($"{string.Format("{0:f4}", _upperDiagonal[row - 1]),10}\t{string.Format("{0:f4}", _centralDiagonal[row]),10}\t{string.Format("{0:f4}", _lowerDiagonal[row]),10}\t");

				for (int i = 0; i < _size - 3 - row; i++)
					Console.Write($"{0,10}\t");

				Console.WriteLine($"{string.Format("{0:f4}", _rightColumn[row]),10}\t\t{string.Format("{0:f4}", _freeTerms[row]),10}");
			}


			row = 1;

			Console.Write($"{string.Format("{0:f4}", _upperDiagonal[row - 1]),10}\t{string.Format("{0:f4}", _centralDiagonal[row]),10}\t{string.Format("{0:f4}", _lowerDiagonal[row]),10}\t");
			for (int i = 0; i < _size - 3 - row; i++)
				Console.Write($"{0,10}\t");
			Console.WriteLine($"{string.Format("{0:f4}", _rightColumn[row]),10}\t\t{string.Format("{0:f4}", _freeTerms[row]),10}");

			row = 0;
			Console.Write($"{string.Format("{0:f4}", _centralDiagonal[row]),10}\t{string.Format("{0:f4}", _lowerDiagonal[row]),10}\t");
			for (int i = 0; i < _size - 3 - row; i++)
				Console.Write($"{0,10}\t");
			Console.WriteLine($"{string.Format("{0:f4}", _rightColumn[row]),10}\t\t{string.Format("{0:f4}", _freeTerms[row]),10}");
		}

		public decimal[] Solve()
		{
			//Console.WriteLine("\nSource\n");
			//Print();
			// STEP 1

			uint row = 0;
			DivideLine(_centralDiagonal[row], row);
			decimal divider = _leftColumn[row + 1];
			_leftColumn[row + 1] = 0;
			_upperDiagonal[row] = 0;
			_centralDiagonal[row + 1] = _centralDiagonal[row + 1] - divider * _lowerDiagonal[row];
			_rightColumn[row + 1] = _rightColumn[row + 1] - divider * _rightColumn[row];
			_freeTerms[row + 1] = _freeTerms[row + 1] - divider * _freeTerms[row];

			for (row = 1; row < _size - 3; row++)
			{
				DivideLine(_centralDiagonal[row], row);
				divider = _upperDiagonal[row];
				_upperDiagonal[row] = 0;
				_centralDiagonal[row + 1] = _centralDiagonal[row + 1] - divider * _lowerDiagonal[row];
				_leftColumn[row + 1] = _leftColumn[row + 1] - divider * _leftColumn[row];
				_rightColumn[row + 1] = _rightColumn[row + 1] - divider * _rightColumn[row];
				_freeTerms[row + 1] = _freeTerms[row + 1] - divider * _freeTerms[row];
			}

			DivideLine(_centralDiagonal[row], row);
			divider = _upperDiagonal[row];
			_upperDiagonal[row] = 0;
			_centralDiagonal[row + 1] = _centralDiagonal[row + 1] - divider * _lowerDiagonal[row];
			_lowerDiagonal[row + 1] = _lowerDiagonal[row + 1] - divider * _rightColumn[row];
			_rightColumn[row + 1] = _lowerDiagonal[row + 1];
			_leftColumn[row + 1] = _leftColumn[row + 1] - divider * _leftColumn[row];
			_freeTerms[row + 1] = _freeTerms[row + 1] - divider * _freeTerms[row];

			row = _size - 2;
			DivideLine(_centralDiagonal[row], row);
			divider = _upperDiagonal[row];
			_upperDiagonal[row] = 0;
			_centralDiagonal[row + 1] = _centralDiagonal[row + 1] - divider * _rightColumn[row];
			_rightColumn[row + 1] = _centralDiagonal[row + 1];
			_leftColumn[row + 1] = _leftColumn[row + 1] - divider * _leftColumn[row];
			_freeTerms[row + 1] = _freeTerms[row + 1] - divider * _freeTerms[row];
			DivideLine(_centralDiagonal[row + 1], row + 1);

			// /STEP 1
#if DEBUG
			Console.WriteLine("\n/STEP 1\n");
			Print();
#endif
			// STEP 2

			row = _size - 2;
			divider = _rightColumn[row];
			_rightColumn[row] = 0;
			_lowerDiagonal[row] = 0;
			_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[_size - 1];
			_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[_size - 1];

			for (row = _size - 3; row >= 2; row--)
			{
				divider = _rightColumn[row];
				_rightColumn[row] = 0;
				_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[_size - 1];
				_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[_size - 1];
			}

			row = 1;
			divider = _rightColumn[row];
			_rightColumn[row] = 0;
			_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[_size - 1];
			_upperDiagonal[row - 1] = _leftColumn[row];
			_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[_size - 1];

			row = 0;
			divider = _rightColumn[row];
			_rightColumn[row] = 0;
			_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[_size - 1];
			_centralDiagonal[row] = _leftColumn[row];
			_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[_size - 1];

			// /STEP 2
#if DEBUG
			Console.WriteLine("\n/STEP 2\n");
			Print();
#endif
			// STEP 3

			for (row = _size - 3; row >= 2; row--)
			{
				divider = _lowerDiagonal[row];
				_lowerDiagonal[row] = 0;
				_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[row + 1];
				_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[row + 1];
			}

			row = 1;
			divider = _lowerDiagonal[row];
			_lowerDiagonal[row] = 0;
			_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[row + 1];
			_upperDiagonal[row - 1] = _leftColumn[row];
			_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[row + 1];

			row = 0;
			divider = _lowerDiagonal[row];
			_lowerDiagonal[row] = 0;
			_leftColumn[row] = _leftColumn[row] - divider * _leftColumn[row + 1];
			_centralDiagonal[row] = _leftColumn[row];
			_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[row + 1];

			// /STEP 3
#if DEBUG
			Console.WriteLine("\n/STEP 3\n");
			Print();
#endif
			// STEP 4

			DivideLine(_centralDiagonal[row], row);
			divider = _leftColumn[row + 1];
			_leftColumn[row + 1] = 0;
			_upperDiagonal[row] = 0;
			_freeTerms[row + 1] = _freeTerms[row + 1] - divider * _freeTerms[row];

			for (row = 2; row < _size; row++)
			{
				divider = _leftColumn[row];
				_leftColumn[row] = 0;
				_freeTerms[row] = _freeTerms[row] - divider * _freeTerms[0];
			}

			// /STEP 4
#if DEBUG
			Console.WriteLine("\n/STEP 4\n");
			Print();
#endif
			// STEP 5

			for (row = 0; row < _size; row++)
			{
				_x[row] = _freeTerms[row];
			}

			// /STEP 5
			return _x;
		}

		private void DivideLine(decimal divider, uint row)
		{
			if (divider == 0)
				throw new DivideByZeroException();

			_freeTerms[row] /= divider;
			_centralDiagonal[row] /= divider;
			_leftColumn[row] /= divider;
			_rightColumn[row] /= divider;
			if (row != 0)
				_upperDiagonal[row - 1] /= divider;
			if (row != _size - 1)
				_lowerDiagonal[row] /= divider;
		}

		private decimal[] _leftColumn;
		private decimal[] _rightColumn;
		private decimal[] _upperDiagonal;
		private decimal[] _centralDiagonal;
		private decimal[] _lowerDiagonal;

		private decimal[] _freeTerms;

		private decimal[] _x;

		private uint _size;
	}
}
