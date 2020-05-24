using System;
using Entities.PlayerScripts;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
	public class Room : MonoBehaviourPun
	{
		public bool b_up, b_down, b_left, b_right;
		public bool up, down, left, right;
		public int size;
		
		public GameObject[] rooms;
		public GameObject HWall, VWall, floors, walls;
		public GameObject Ennemy, Boss;

		public void Deserialize(byte[] bytes)
		{
			RoomStats roomStats = RoomStats.Deserialize(bytes);
			
			size = roomStats.size;
			b_up = roomStats.b_up;
			b_down = roomStats.b_down;
			b_left = roomStats.b_left;
			b_right = roomStats.b_right;
			up = roomStats.up;
			down = roomStats.down;
			left = roomStats.left;
			right = roomStats.right;

			Vector3 pos = new Vector3(roomStats.x, roomStats.y, roomStats.z);
			transform.position = pos;
			
			Create();
		}

		public byte[] Serialize()
		{
			Vector3 pos = transform.position;
			
			RoomStats roomStats = new RoomStats
			{
				size = size,
				b_up = b_up,
				b_down = b_down,
				b_left = b_left,
				b_right = b_right,
				up = up,
				down = down,
				left = left,
				right = right,
				x = pos.x,
				y = pos.y,
				z = pos.z
			};

			return RoomStats.Serialize(roomStats);
		}

		private void Create()
		{
			if (PhotonNetwork.IsConnected)
			{
				string prefabName;
				switch (size)
				{
					case 0:
						prefabName = "Spawn lv1";
						break;
					case 1:
						prefabName = "Boss lv1";
						break;
					case 2:
						prefabName = "Shop Room lv1";
						break;
					case 3:
						prefabName = "Room Small lv1";
						break;
					case 4:
						prefabName = "Room Med lv1";
						break;
					default:
						prefabName = "Room Big lv1";
						break;
				}
				
				PhotonNetwork.Instantiate(prefabName, transform.position, Quaternion.identity);
			}
			else Instantiate(rooms[size], transform.position, Quaternion.identity);
			if (down || right) return;
			if (Random.Range(0, 2) == 0)
				down = true;
			else right = true;
		}
		
		public void Generate()
		{
			Create();
			if (size > 2)
				GenerateMobs();
		}

		private void GenerateMobs()
		{
			int nbMobs;
			float minX, maxX, minY, maxY;
			switch (size)
			{
				case 3:
					nbMobs = Random.Range(0, 3);
					minX = 11.5f;
					maxX = 19.5f;
					minY = 11.5f;
					maxY = 23.5f;
					break;
				case 4:
					nbMobs = Random.Range(2, 4);
					minX = 6.5f;
					maxX = 24.5f;
					minY = 6.5f;
					maxY = 28.5f;
					break;
				default:
					nbMobs = Random.Range(3, 5);
					minX = 1.5f;
					maxX = 29.5f;
					minY = 1.5f;
					maxY = 34.5f;
					break;
			}

			for (int i = 0; i < nbMobs; i++)
			{
				Vector3 position = transform.position;

				position.x += Random.Range(minX, maxX);
				position.y -= Random.Range(minY, maxY);
				
				if (PhotonNetwork.IsConnected)
					PhotonNetwork.Instantiate("Ennemy", position, Quaternion.identity);
				else
					Instantiate(Ennemy, position, Quaternion.identity);
			}
		}

		private void GenerateBoss()
		{
			Vector3 pos = transform.position;
			pos.x += 15;
			pos.y += 17.5f;

			Boss = PhotonNetwork.IsConnected ?
				PhotonNetwork.Instantiate(Boss.name, pos, Quaternion.identity) : Instantiate(Boss, pos, Quaternion.identity);

			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
				player.GetComponent<Player>().boss = Boss;
			
			Boss.SetActive(false);
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

				if (PhotonNetwork.IsConnected)
					PhotonNetwork.Instantiate("HWall", position, Quaternion.identity);
				else Instantiate(HWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 12.5f;
				position.y -= 1.5f;
				for (int i = 0; i < max; i++)
				{
					if (i != max - 1)
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
					position.x++;
					
					for (int j = 0; j < 5; j++)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("floors", position, Quaternion.identity);
						else Instantiate(floors, position, Quaternion.identity);
						position.x++;
					}

					if (i != max - 1)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
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
            
				if (PhotonNetwork.IsConnected)
					PhotonNetwork.Instantiate("HWall", position, Quaternion.identity);
				else Instantiate(HWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 12.5f;
				position.y -= 34.5f - max;
				for (int i = 0; i <= max + 1; i++)
				{
					if (i != 0)
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
					position.x++;
            					
					for (int j = 0; j < 5; j++)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("floors", position, Quaternion.identity);
						else Instantiate(floors, position, Quaternion.identity);
						position.x++;
					}
            
					if (i != 0)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
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
            
				if (PhotonNetwork.IsConnected)
					PhotonNetwork.Instantiate("VWall", position, Quaternion.identity);
				else Instantiate(VWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 0.5f;
				position.y -= 20.5f;
				for (int i = 0; i <= max; i++)
				{
					if (i != max)
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
					position.y++;
            					
					for (int j = 0; j < 5; j++)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("floors", position, Quaternion.identity);
						else Instantiate(floors, position, Quaternion.identity);
						position.y++;
					}
            
					if (i != max)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
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
				if (PhotonNetwork.IsConnected)
					PhotonNetwork.Instantiate("VWall", position, Quaternion.identity);
				else Instantiate(VWall, position, Quaternion.identity);
			}
			else
			{
				position.x += 30.5f - max;
				position.y -= 20.5f;
				for (int i = 0; i <= max; i++)
				{
					if (i != 0)
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
					position.y++;
            					
					for (int j = 0; j < 5; j++)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("floors", position, Quaternion.identity);
						else Instantiate(floors, position, Quaternion.identity);
						position.y++;
					}
            
					if (i != 0)
					{
						if (PhotonNetwork.IsConnected)
							PhotonNetwork.Instantiate("walls", position, Quaternion.identity);
						else Instantiate(walls, position, Quaternion.identity);
					}
					position.y -= 6;
					position.x++;
				}
			}
		}

		#endregion

		public void Reset()
		{
			up = b_up;
			down = b_down;
			left = b_left;
			right = b_right;
			size = 0;
		}
	}
}