namespace NumericalMethods1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool run = true;
			while (run)
			{
				//try
				//{
					uint size = 10;
					int minValue = -10;
					int maxValue = 10;

					Console.Write("Size >> ");
					size = uint.Parse(Console.ReadLine());
					Console.Write("Min value >> ");
					minValue = int.Parse(Console.ReadLine());
					Console.Write("Max value >> ");
					maxValue = int.Parse(Console.ReadLine());

					var provider = new RandomMatrixWithUnitXProvider(size, minValue, maxValue);
					MatrixSolver solver = new MatrixSolver(provider);
						
					var actualXValues = solver.Solve();
					var expectedXValues = provider.GetX();

					decimal accuracy = AccuracyUtil.CalculateAccuracy(expectedXValues, actualXValues, 0.0000006M);

					Console.WriteLine("==============================================");
					Console.WriteLine($"{"Expected values",16}|{"Actual values",16}");
					for (int i = 0; i < size; i++)
					{
						Console.WriteLine($"{expectedXValues[i],15} | {actualXValues[i],-15}");
					}
					Console.WriteLine($"Accuracy: {accuracy}");
				//}
				//catch(Exception ex)
				//{
				//	Console.ForegroundColor = ConsoleColor.Red;
				//	Console.WriteLine(ex.ToString());
				//	Console.ResetColor();
				//}

				Console.Write("Continue? (y/n) >> ");
				if (Console.ReadLine() != "y")
					run = false;
			}
		}
	}
}