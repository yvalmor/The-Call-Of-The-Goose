using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
	public Transform[] positions;
	public GameObject[] rooms;
	public GameObject player;
	private Transform _spawnRoom, _bossRoom, _shopRoom;

	private void Start()
	{
		GetPositions();
		foreach (Transform transfo in positions)
		{
			GameObject room;
			if (transfo == _spawnRoom)
				room = rooms[0];
			else if (transfo == _bossRoom)
				room = rooms[1];
			else if (transfo == _shopRoom)
				room = rooms[2];
			else room = rooms[Random.Range(3, rooms.Length)];
			Instantiate(room, transfo.position, Quaternion.identity);
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
