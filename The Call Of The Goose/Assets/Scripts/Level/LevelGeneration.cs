using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
	public class LevelGeneration : MonoBehaviour
	{
		public GameObject[] positions;
		public GameObject player;
		private Transform _spawnRoom, _bossRoom, _shopRoom;

		private Room[] _rooms;

		private void Start()
		{
			GenLevel();
		}

		private void GetPositions()
		{
			_spawnRoom = positions[Random.Range(0, positions.Length)].transform; 

			do _shopRoom = positions[Random.Range(0, positions.Length)].transform;
			while (_spawnRoom == _shopRoom);

			do _bossRoom = positions[Random.Range(0, positions.Length)].transform;
			while (_spawnRoom == _bossRoom || _shopRoom == _bossRoom);
		}

		public void GenLevel()
		{
			GetPositions();

			_rooms = new Room[positions.Length];
			for (int i = 0; i < positions.Length; i++)
				_rooms[i] = positions[i].GetComponent<Room>();

			for (int i = 0; i < _rooms.Length; i++)
			{
				if (positions[i].transform == _spawnRoom)
					_rooms[i].size = 0;
				else if (positions[i].transform == _bossRoom)
					_rooms[i].size = 1;
				else if (positions[i].transform == _shopRoom)
					_rooms[i].size = 2;
				else _rooms[i].size = Random.Range(3, 6);
				_rooms[i].Generate();
				
				if (i > 0 && i % 4 != 0 && _rooms[i - 1].right)
					_rooms[i].left = true;
				if (i > 3 && _rooms[i - 4].down)
					_rooms[i].up = true;
			}

			foreach (Room room in _rooms)
				room.GenerateCorridors();

			Vector3 playerSpawn = _spawnRoom.position;
			playerSpawn.x += 15;
			playerSpawn.y -= 17.5f;
			player.transform.position = playerSpawn;
		}

		public void DestroyLevel()
		{
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Generation"))
				Destroy(obj);
		}
	}
}