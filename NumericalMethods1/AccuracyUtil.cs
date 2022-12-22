using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	public static class AccuracyUtil
	{
		public static decimal CalculateAccuracy(IReadOnlyList<decimal> expectedSolution, IReadOnlyList<decimal> actualSolution, decimal eps)
		{
			if (expectedSolution is null)
				throw new ArgumentNullException(nameof(expectedSolution));
			if (actualSolution is null)
				throw new ArgumentNullException(nameof(actualSolution));
			if (expectedSolution.Count != actualSolution.Count)
				throw new ArgumentException("List sizes are not equal.");

			eps = Math.Abs(eps);

			decimal accuracy = 0.0M;
			for (var i = 0; i < expectedSolution.Count; ++i)
			{
				accuracy = Math.Max(accuracy, Math.Abs(
					Math.Abs(expectedSolution[i]) > eps
						? (actualSolution[i] - expectedSolution[i]) / expectedSolution[i]
						: actualSolution[i] - expectedSolution[i]
				));
			}

			return accuracy;
		}
	}
}
