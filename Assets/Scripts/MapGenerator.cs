using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Room Settings")]
    public List<Room> rooms;
    public List<Room> startingRooms;

    [Header("Settings")]
    public int maxPrevRooms = 3;
    public int maxNextRooms = 3;
    public int totalRoomsToSpawn = 10;

    private Room lastSpawnedRoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < startingRooms.Count; i++)
        {
            SpawnSpecificRoom(startingRooms[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSpecificRoom(Room targetPrefab)
    {   
        if (targetPrefab.entrancePoint == null)
        {
            lastSpawnedRoom = Instantiate(targetPrefab, transform.position, transform.rotation);
            
            transform.position = lastSpawnedRoom.exitPoint.transform.position;
            transform.rotation = lastSpawnedRoom.exitPoint.transform.rotation;
        } 
        else
        {
            Room newRoom = Instantiate(targetPrefab);

            Quaternion targetRotation = transform.rotation * Quaternion.Inverse(newRoom.entrancePoint.transform.localRotation);
            newRoom.transform.rotation = targetRotation;

            Vector3 positionOffset = transform.position - newRoom.entrancePoint.transform.position;
            newRoom.transform.position += positionOffset;

            lastSpawnedRoom = newRoom;

            transform.position = lastSpawnedRoom.exitPoint.transform.position;
            transform.rotation = lastSpawnedRoom.exitPoint.transform.rotation;
        }
    }
}
