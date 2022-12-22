using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalMethods1
{
	internal interface IMatrixProvider
	{
		decimal[] GetLeftColumn();
		decimal[] GetRightColumn();
		decimal[] GetUpperDiagonal();
		decimal[] GetCentralDiagonal();
		decimal[] GetLowerDiagonal();
		decimal[] GetFreeTerms();
		decimal[] GetX();
		uint GetSize();
	}
}
