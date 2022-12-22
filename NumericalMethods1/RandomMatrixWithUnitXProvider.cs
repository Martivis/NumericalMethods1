using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	internal class RandomMatrixWithUnitXProvider : RandomMatrixProvider
	{
		public RandomMatrixWithUnitXProvider(uint size, int minValue, int maxValue) : base(size, minValue, maxValue)
		{
			_x = new decimal[size];
			for (int i = 0; i < size; i++)
			{
				_x[i] = 1;
			}
			CountFreeTerms();
		}

		private void CountFreeTerms()
		{
			_freeTerms[0] = _leftColumn[0] * _x[0] +
				_lowerDiagonal[0] * _x[1] +
				_rightColumn[0] * _x[_size - 1];

			_freeTerms[1] = _upperDiagonal[0] * _x[0] +
				_centralDiagonal[1] * _x[1] +
				_lowerDiagonal[1] * _x[2] +
				_rightColumn[1] * _x[_size - 1];

			for (uint i = 2; i < _size - 2; i++)
			{
				_freeTerms[i] = _leftColumn[i] * _x[0] +
					_upperDiagonal[i - 1] * _x[i - 1] +
					_centralDiagonal[i] * _x[i] +
					_lowerDiagonal[i] * _x[i + 1] +
					_rightColumn[i] * _x[_size - 1];
			}

			_freeTerms[_size - 2] = _leftColumn[_size - 2] * _x[0] +
				_upperDiagonal[_size - 3] * _x[_size - 3] +
				_centralDiagonal[_size - 2] * _x[_size - 2] +
				_lowerDiagonal[_size - 2] * _x[_size - 1];

			_freeTerms[_size - 1] = _leftColumn[_size - 1] * _x[0] +
				_upperDiagonal[_size - 2] * _x[_size - 2] +
				_centralDiagonal[_size - 1] * _x[_size - 1];

		}

	}
}
