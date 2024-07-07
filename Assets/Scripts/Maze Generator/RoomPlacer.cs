using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class RoomPlacer : MonoBehaviour
{

    [Header("Amount of Rooms")]
    [SerializeField] int NumRooms;

    [Header("Rooms")]
    public Room[] RoomPrefabs;

    [Header("Starting Room")]
    public Room StartingRoom;

    private Room[,] spawnedRooms;

    [Header("FinnishRoom")]
    public Room FinnishRoom;

    private /*IEnumerator */void Start()
    {
        NumRooms = PlayerPrefs.GetInt("playerDiff");
        spawnedRooms = new Room[20, 20];
        spawnedRooms[5, 5] = StartingRoom;

        for (int i = 0; i < NumRooms-1; i++)
        {
            PlaceOneRoom(false);
            //yield return new WaitForSecondsRealtime(1f);
        }
        PlaceOneRoom(true);

    }

    private void PlaceOneRoom(bool finnish)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom;
        if (finnish)
        {
            newRoom = Instantiate(FinnishRoom);

        }
        else
        {
            newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        } 

        //Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        //spawnedRooms[position.x,position.y] = newRoom;

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            //
            //newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 10;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }

        }

        Destroy(newRoom.gameObject);
    }
    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        //Vector2Int selectedDirection; // = neighbours[Random.Range(0, neighbours.Count)];
        //Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];
        if (room.tag == "connector")
        {
            for(int i = 0; i < neighbours.Count; i++)
            {
                Vector2Int selectedDirection = neighbours[i];
                Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];
                OpenRoom(room, selectedRoom, selectedDirection);
            }
        }
        else
        {
            Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
            Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];
            OpenRoom(room, selectedRoom, selectedDirection);
        }

        return true;
    }

    private void OpenRoom(Room room, Room selectedRoom, Vector2Int selectedDirection)
    {

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
        }
    }

}