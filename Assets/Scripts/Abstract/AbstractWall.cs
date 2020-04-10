using System.Collections;
using System.Collections.Generic;

namespace DeFenceAbstract
{
	public class AbstractWall
	{
		internal int ID;
		internal bool active;

		// ------------------------------------------ functions to use for interaction ------------------------------------------

		public AbstractWall(int _ID)
		{
			ID = _ID;
			active = true;
		}
	}
}
