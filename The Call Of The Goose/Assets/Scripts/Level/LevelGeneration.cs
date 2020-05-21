using System;
using System.Collections.Generic;
using System.IO;
using Entities;
using Entities.PlayerScripts;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level
{
	public class LevelGeneration : MonoBehaviourPun
	{
		public GameObject[] positions;
		public Transform _spawnRoom, _bossRoom, _shopRoom;

		private Room[] _rooms;

		public Room[] Rooms => _rooms;

		public Vector3 SpawnRoom => _spawnRoom.position;

		private void Awake()
		{
			if (!PhotonNetwork.IsConnected)
				GenLevel();
			else if (!PhotonNetwork.IsMasterClient)
			{
				GetPositions();
				_rooms = new Room[20];
				GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Room");
				for (int i = 0; i < _rooms.Length; i++)
					_rooms[i] = roomObjects[i].GetComponent<Room>();
			}
		}

		public byte[][] Serialize()
		{
			Debug.Log("Serializing level");
			
			List<byte[]> toSend = new List<byte[]>();

			foreach (Room room in _rooms)
				toSend.Add(room.Serialize());
			
			MemoryStream ms = new MemoryStream(12);
			ms.Write(BitConverter.GetBytes(_spawnRoom.position.x), 0, 4);
			ms.Write(BitConverter.GetBytes(_spawnRoom.position.y), 0, 4);
			ms.Write(BitConverter.GetBytes(_spawnRoom.position.z), 0, 4);
			toSend.Add(ms.ToArray());
			ms = new MemoryStream(12);
			ms.Write(BitConverter.GetBytes(_bossRoom.position.x), 0, 4);
			ms.Write(BitConverter.GetBytes(_bossRoom.position.y), 0, 4);
			ms.Write(BitConverter.GetBytes(_bossRoom.position.z), 0, 4);
			toSend.Add(ms.ToArray());
			ms = new MemoryStream(12);
			ms.Write(BitConverter.GetBytes(_shopRoom.position.x), 0, 4);
			ms.Write(BitConverter.GetBytes(_shopRoom.position.y), 0, 4);
			ms.Write(BitConverter.GetBytes(_shopRoom.position.z), 0, 4);
			toSend.Add(ms.ToArray());

			return toSend.ToArray();
		}

		public void Deserialize(byte[][] bytes)
		{
			Debug.Log("Deserializing level");
			
			for (int i = 0; i < bytes.Length - 3; i++)
				_rooms[i].Deserialize(bytes[i]);

			Vector3 spawn, boss, shop;
			
			spawn.x = BitConverter.ToSingle(bytes[bytes.Length - 3], 0);
			spawn.y = BitConverter.ToSingle(bytes[bytes.Length - 3], 4);
			spawn.z = BitConverter.ToSingle(bytes[bytes.Length - 3], 8);
			
			boss.x = BitConverter.ToSingle(bytes[bytes.Length - 2], 0);
			boss.y = BitConverter.ToSingle(bytes[bytes.Length - 2], 4);
			boss.z = BitConverter.ToSingle(bytes[bytes.Length - 2], 8);
			
			shop.x = BitConverter.ToSingle(bytes[bytes.Length - 1], 0);
			shop.y = BitConverter.ToSingle(bytes[bytes.Length - 1], 4);
			shop.z = BitConverter.ToSingle(bytes[bytes.Length - 1], 8);

			_spawnRoom.position = spawn;
			_shopRoom.position = shop;
			_bossRoom.position = boss;
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
			
			if (PhotonNetwork.IsConnected) return;

			Vector3 playerSpawn = _spawnRoom.position;
			playerSpawn.x += 15;
			playerSpawn.y -= 17.5f;

			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
				player.transform.position = playerSpawn;
		}

		public void DestroyLevel()
		{
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Generation"))
				Destroy(obj);
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Ennemy"))
				Destroy(obj);
			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Minimap"))
				Destroy(obj);
			foreach (Room room in _rooms)
			{
				room.Reset();
			}
		}
	}
}