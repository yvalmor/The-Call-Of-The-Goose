using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
	public class Room : MonoBehaviour
	{
		public bool up, down, left, right;
		
		public GameObject[] rooms;
		public GameObject HWall, VWall, floors, walls;
		public int size;
		
		public void Generate()
		{
			Instantiate(rooms[size], transform.position, Quaternion.identity);
			if (down || right) return;
			if (Random.Range(0, 2) == 0)
				down = true;
			else right = true;
		}

		#region Corridors

		public void GenerateCorridors()
		{
			GenerateUpCorridor();
			GenerateDownCorridor();
			GenerateLeftCorridor();
			GenerateRightCorridor();
		}
		private void GenerateUpCorridor()
		{
			Vector3 position = transform.position;

			int max;
			switch (size)
			{
				case 0:
					max = 7;
					break;
				case 1:
					max = 2;
					break;
				case 2:
				case 3:
					max = 10;
					break;
				case 4:
					max = 5;
					break;
				default:
					max = 0;
					break;
			}
			
			if (up)
			{
				position.y -= max;

				Instantiate(HWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 12.5f;
				position.y -= 1.5f;
				for (int i = 0; i < max; i++)
				{
					if (i != max - 1)
						Instantiate(walls, position, Quaternion.identity);
					position.x++;
					
					for (int j = 0; j < 5; j++)
					{
						Instantiate(floors, position, Quaternion.identity);
						position.x++;
					}

					if (i != max - 1)
					{
						Instantiate(walls, position, Quaternion.identity);
						position.x++;
					}
					position.x -= 7;
					position.y--;
				}
			}
		}
		private void GenerateDownCorridor()
		{
			Vector3 position = transform.position;
            
			int max;
			switch (size)
			{
				case 0:
					max = 7;
					break;
				case 1:
					max = 2;
					break;
				case 2:
				case 3:
					max = 10;
					break;
				case 4:
					max = 5;
					break;
				default:
					max = 0;
					break;
			}
            			
			if (down)
			{
				position.y -= 34 - max;
            
				Instantiate(HWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 12.5f;
				position.y -= 34.5f - max;
				for (int i = 0; i <= max + 1; i++)
				{
					if (i != 0)
						Instantiate(walls, position, Quaternion.identity);
					position.x++;
            					
					for (int j = 0; j < 5; j++)
					{
						Instantiate(floors, position, Quaternion.identity);
						position.x++;
					}
            
					if (i != 0)
					{
						Instantiate(walls, position, Quaternion.identity);
					}
					position.x++;
					position.x -= 7;
					position.y--;
				}
			}
		}
		private void GenerateLeftCorridor()
		{
			Vector3 position = transform.position;
            
			int max;
			switch (size)
			{
				case 0:
				case 4:
					max = 5;
					break;
				case 2:
					max = 8;
					break;
				case 3:
					max = 10;
					break;
				default:
					max = 0;
					break;
			}
			
			if (left)
			{
				position.x += max;
            
				Instantiate(VWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 0.5f;
				position.y -= 20.5f;
				for (int i = 0; i <= max; i++)
				{
					if (i != max)
						Instantiate(walls, position, Quaternion.identity);
					position.y++;
            					
					for (int j = 0; j < 5; j++)
					{
						Instantiate(floors, position, Quaternion.identity);
						position.y++;
					}
            
					if (i != max)
					{
						Instantiate(walls, position, Quaternion.identity);
					}
					position.y -= 6;
					position.x++;
				}
			}
		}
		private void GenerateRightCorridor()
		{
			Vector3 position = transform.position;
            
			int max;
			switch (size)
			{
				case 0:
				case 4:
					max = 5;
					break;
				case 2:
					max = 8;
					break;
				case 3:
					max = 10;
					break;
				default:
					max = 0;
					break;
			}
            			
			if (right)
			{
				position.x += 30 - max;
				Instantiate(VWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 30.5f - max;
				position.y -= 20.5f;
				for (int i = 0; i <= max; i++)
				{
					if (i != 0)
						Instantiate(walls, position, Quaternion.identity);
					position.y++;
            					
					for (int j = 0; j < 5; j++)
					{
						Instantiate(floors, position, Quaternion.identity);
						position.y++;
					}
            
					if (i != 0)
					{
						Instantiate(walls, position, Quaternion.identity);
					}
					position.y -= 6;
					position.x++;
				}
			}
		}

		#endregion
	}
}