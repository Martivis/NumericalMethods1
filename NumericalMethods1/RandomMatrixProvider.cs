using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	internal class RandomMatrixProvider : IMatrixProvider
	{
		private readonly Random _random;
		protected decimal[] _leftColumn;
		protected decimal[] _rightColumn;
		protected decimal[] _upperDiagonal;
		protected decimal[] _centralDiagonal;
		protected decimal[] _lowerDiagonal;

		protected decimal[] _freeTerms;
		protected decimal[] _x;

		protected uint _size;
		private int _minValue;
		private int _maxValue;
		public RandomMatrixProvider(uint size, int minValue, int maxValue)
		{
			_random = new Random();
			_leftColumn = new decimal[size];
			_rightColumn = new decimal[size];
			_upperDiagonal = new decimal[size - 1];
			_centralDiagonal = new decimal[size];
			_lowerDiagonal = new decimal[size - 1];

			_freeTerms = new decimal[size];
			_x = new decimal[size];

			_size = size;
			_minValue = minValue;
			_maxValue = maxValue;

			for (int i = 0; i < size; i++)
			{
				_leftColumn[i] = NextRandomNumber();
				_rightColumn[i] = NextRandomNumber();
				_centralDiagonal[i] = NextRandomNumber();
				_freeTerms[i] = NextRandomNumber();
				_x[i] = NextRandomNumber();
				if (i < size - 1)
				{
					_upperDiagonal[i] = NextRandomNumber();
					_lowerDiagonal[i] = NextRandomNumber();
				}
			}

			_leftColumn[0] = _centralDiagonal[0];
			_leftColumn[1] = _upperDiagonal[0];
			_rightColumn[size - 1] = _centralDiagonal[size - 1];
			_rightColumn[size - 2] = _lowerDiagonal[size - 2];
		}
		public decimal[] GetCentralDiagonal()
		{
			return _centralDiagonal;
		}

		public virtual decimal[] GetFreeTerms()
		{
			return _freeTerms;
		}

		public decimal[] GetLeftColumn()
		{
			return _leftColumn;
		}

		public decimal[] GetLowerDiagonal()
		{
			return _lowerDiagonal;
		}

		public decimal[] GetRightColumn()
		{
			return _rightColumn;
		}

		public uint GetSize()
		{
			return _size;
		}

		public virtual decimal[] GetX()
		{
			return _x;
		}

		public decimal[] GetUpperDiagonal()
		{
			return _upperDiagonal;
		}

		private decimal NextRandomNumber()
		{
			int randint = 0;
			while (randint == 0)
				randint = _random.Next(_minValue, _maxValue);
			return (decimal)(_random.NextDouble() * randint);
		}
	}
}
