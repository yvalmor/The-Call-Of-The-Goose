using UnityEngine;

namespace Level
{
	public class LevelGeneration : MonoBehaviour
	{
		public Transform[] positions;
		public GameObject[] rooms;
		public GameObject hWall, vWall;
		public GameObject player;
		private Transform _spawnRoom, _bossRoom, _shopRoom;

		private void Start()
		{
			GetPositions();
			for (int i = 0; i < positions.Length; i++)
			{
				Transform transfo = positions[i];
				GameObject room;
				if (transfo == _spawnRoom)
					room = rooms[0];
				else if (transfo == _bossRoom)
					room = rooms[1];
				else if (transfo == _shopRoom)
					room = rooms[2];
				else
				{
					int index = Random.Range(0, 6);
					room = index == 0 ? rooms[5] :
						index > 2 ? rooms[3] :
						rooms[4];
				}

				Instantiate(room, transfo.position, Quaternion.identity);
			
				if (i % 4 == 0)
				{
					Vector3 position = transfo.position;
					if (room == rooms[0] || room == rooms[4])
						position.x += 5;
					else if (room == rooms[1])
						position.x += 8;
					else if (room == rooms[3])
						position.x += 10;
					Instantiate(vWall, position, Quaternion.identity);
				}
			
				if (i % 4 == 3)
				{
					Vector3 position = transfo.position;
					if (room == rooms[0] || room == rooms[4])
						position.x += 25;
					else if (room == rooms[1])
						position.x += 22;
					else if (room == rooms[3])
						position.x += 20;
					else position.x += 30;
					Instantiate(vWall, position, Quaternion.identity);
				}
			
				if (i / 4 == 0)
				{
					Vector3 position = transfo.position;
					if (room == rooms[0])
						position.y -= 7;
					else if (room == rooms[2])
						position.y -= 2;
					else if (room == rooms[1] || room == rooms[3])
						position.y -= 10;
					else if (room == rooms[4])
						position.y -= 5;
					Instantiate(hWall, position, Quaternion.identity);
				}

				if (i / 4 != 4) continue;
				{
					Vector3 position = transfo.position;
					if (room == rooms[0])
						position.y -= 27;
					else if (room == rooms[2])
						position.y -= 32;
					else if (room == rooms[1] || room == rooms[3])
						position.y -= 24;
					else if (room == rooms[4])
						position.y -= 29;
					else position.y -= 34;
					Instantiate(hWall, position, Quaternion.identity);
				}
			}

			Vector3 playerSpawn = _spawnRoom.position;
			playerSpawn.x += 15;
			playerSpawn.y -= 17.5f;
			Instantiate(player, playerSpawn, Quaternion.identity);
		}

		private void GetPositions()
		{
			_spawnRoom = positions[Random.Range(0, positions.Length)]; 

			do _shopRoom = positions[Random.Range(0, positions.Length)];
			while (_spawnRoom == _shopRoom);

			do _bossRoom = positions[Random.Range(0, positions.Length)];
			while (_spawnRoom == _bossRoom || _shopRoom == _bossRoom);
		}
	}
}
