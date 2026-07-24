using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // [Header("References")]
    // public GameObject doorObject;

    [Header("Room Settings")]
    public List<Room> randomRooms;
    public List<Room> startingRooms;

    [Header("Settings")]
    public int maxRooms = 5;

    [Header("Info")]
    public Room lastSpawnedRoom;
    public int currentRoomCount;
    public List<Room> currentSpawnedRooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < startingRooms.Count; i++)
        {
            SpawnSpecificRoom(startingRooms[i]);
        }

        for (int i = 0; i < maxRooms; i++)
        {
            SpawnRandomRoom();
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
            
            currentSpawnedRooms.Add(lastSpawnedRoom);
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

            currentSpawnedRooms.Add(lastSpawnedRoom);

            // Instantiate(doorObject, transform.position, transform.rotation);
        }
    }

    void SpawnRandomRoom()
    {
        int randomInteger = Random.Range(0, randomRooms.Count);
        SpawnSpecificRoom(randomRooms[randomInteger]);
    }
}
