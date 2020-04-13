using System.Collections;
using System.Collections.Generic;

namespace DeFenceAbstract
{
	public class AbstractManager
	{
		private GameEndDelegate EndGame;

		internal AbstractBoard board;
		internal int playerAmount;
		internal int currentPlayer;
		internal bool lastPassed;

		internal List<AbstractTile> tilesClicked;

		private void TookStep()
		{
			currentPlayer = currentPlayer % playerAmount + 1;
			board.LoadTerritorries();
			tilesClicked = new List<AbstractTile>();
			lastPassed = false;
		}

		//do step functions

		private void Place()
		{
			if (!CanPlace()) return;

			tilesClicked[0].owner = currentPlayer;
			tilesClicked[0].hasFigure = true;

			TookStep();
		}

		private void Group()
		{
			if (!CanGroup()) return;

			tilesClicked[0].GetWallOfNeighbour(tilesClicked[1].ID).active = false;

			TookStep();
		}

		private void Attack()
		{
			if (!CanAttack()) return;

			AbstractTile enemy = null;
			List<AbstractTile> friends = new List<AbstractTile>();

			foreach (AbstractTile tile in tilesClicked)
			{
				if (tile.owner == currentPlayer) friends.Add(tile);
				else enemy = tile;
			}

			foreach (int[] neighbour in enemy.neighbours)
			{
				board.walls[neighbour[1]].active = true;
			}
			foreach (AbstractTile friend in friends)
			{
				enemy.GetWallOfNeighbour(friend.ID).active = false;
			}
			enemy.owner = currentPlayer;
			enemy.hasFigure = false;

			TookStep();
		}

		private void Pass()
		{
			if (lastPassed)
			{
				EndGame(new GameEndArgs(this));
				return;
			}

			TookStep();
			lastPassed = true;
		}

		private void ResetTurn()
		{
			tilesClicked = new List<AbstractTile>();
		}

		// scoring functions

		internal int[] LeadingPlayer
		{
			get
			{
				List<int> leaders = new List<int>();
				int maxSoFar = 0;
				for (int i = 1; i <= playerAmount; i++)
				{
					if (GetPlayerScore(i) > maxSoFar)
					{
						leaders = new List<int>();
						leaders.Add(i);
						maxSoFar = GetPlayerScore(i);
					}
					else if (GetPlayerScore(i) == maxSoFar)
					{
						leaders.Add(i);
					}
				}
				return leaders.ToArray();
			}
		}

		internal int GetPlayerScore(int player)
		{
			if (player < 1 || player > playerAmount) return 0;
			int score = 0;
			foreach (AbstractTerritory territory in board.territories)
			{
				if (territory.Owner == player) score++;
			}
			return score;
		}

		// ------------------------------------------ functions to use for interaction ------------------------------------------

		public delegate void GameEndDelegate(GameEndArgs endArgs);

		// constructors

		public AbstractManager(GameEndDelegate GE)
		{
			EndGame = GE;

			board = null;
			playerAmount = 2;
			currentPlayer = 1;
			lastPassed = false;

			tilesClicked = new List<AbstractTile>();
		}

		public void SetBoard(AbstractBoard _board)
		{
			board = _board;
			board.LoadTerritorries();
		}

		// literal interactions

		public void ClickTile(int tileID)
		{
			board.tiles[tileID].ClickedTile();
		}

		public void TakeStep()
		{
			if (tilesClicked.Count == 0)
			{
				Pass();
			}
			else if (CanPlace())
			{
				Place();
			}
			else if (CanGroup())
			{
				Group();
			}
			else if (CanAttack())
			{
				Attack();
			}
			else
			{
				ResetTurn();
			}
		}

		// can do step functions

		public bool CanPlace()
		{
			if (tilesClicked.Count != 1) return false;

			if (tilesClicked[0].owner == 0) return true;

			return false;
		}

		public bool CanGroup()
		{
			if (tilesClicked.Count != 2) return false;

			foreach (AbstractTile tile in tilesClicked)
			{
				if (!tile.hasFigure) return false;
				if (tile.owner != currentPlayer) return false;
			}

			if (!tilesClicked[0].IsNeighbourOf(tilesClicked[1].ID)) return false;

			return tilesClicked[0].GetWallOfNeighbour(tilesClicked[1].ID).active;
		}

		public bool CanAttack()
		{
			if (tilesClicked.Count < 2) return false;

			AbstractTile enemy = null;
			List<AbstractTile> friends = new List<AbstractTile>();

			foreach (AbstractTile tile in tilesClicked)
			{
				if (!tile.hasFigure) return false;

				if (tile.owner == currentPlayer) friends.Add(tile);
				else if (enemy != null) return false;
				else enemy = tile;
			}
			if (enemy == null) return false;
			int friendStrength = 0;

			foreach (AbstractTile tile in friends)
			{
				if (!tile.IsNeighbourOf(enemy.ID)) return false;
				friendStrength += tile.Strength;
			}


			return (friendStrength > enemy.Strength);
		}

		public int IsValidStep()
		{
			if (tilesClicked.Count == 0) return 0;

			if (CanPlace()) return 1;
			if (CanGroup()) return 3;
			if (CanAttack()) return 8;

			return -1;
		}

		// info loging

		public string LogGame()
		{
			string s = "###########################################\n";

			foreach (AbstractTile t in board.tiles)
			{
				s += t.owner.ToString() + "\n";
			}

			s += "-------------------------\n";

			foreach (AbstractWall w in board.walls)
			{
				s += (w.active ? "1" : "0") + "\n";
			}

			s += "###########################################";
			return s;
		}

		public string LogBoard()
		{
			string s = "";

			s += "no of tiles: " + board.tiles.Count + "\n";
			s += "no of walls: " + board.walls.Count + "\n";

			for (int i = 0; i < board.tiles.Count; i++)
			{
				for (int j = 0; j < board.tiles[i].neighbours.Count; j++)
				{
					s += board.tiles[i].neighbours[j][0] + " " + board.tiles[i].neighbours[j][1] + " | ";
				}
				s += "\n";
			}

			return s;
		}

		public List<int> GameStateAsInt()
		{
			List<int> result = new List<int>();
			foreach (AbstractTile t in board.tiles)
			{
				result.Add(t.owner);
				result.Add(t.hasFigure ? 1 : 0);
			}

			foreach (AbstractWall w in board.walls)
			{
				result.Add(w.active ? 1 : 0);
			}
			return result;
		}
	}

	public class GameEndArgs
	{
		public GameEndArgs(AbstractManager am)
		{
			Winners = am.LeadingPlayer;

			int[] _scores = new int[am.playerAmount];
			for (int i = 0; i < am.playerAmount; i++)
			{
				_scores[i] = am.GetPlayerScore(i + 1);
			}
			Scores = _scores;
		}

		public int[] Winners { get; }
		public int[] Scores { get; } 
	}
}
