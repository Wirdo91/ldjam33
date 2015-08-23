using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObjectPool
{
    public List<GameObject> ActiveObject { get; set; }
    private Queue<GameObject> DespawnedElements { get; set; }
    private GameObject Prefab { get; set; }
    private Transform Parent { get; set; }

    public ObjectPool(GameObject prefab, Transform parent, int preallocateAmount = 0)
    {
        Prefab = prefab;
        Parent = parent;
        ActiveObject = new List<GameObject>();
        DespawnedElements = new Queue<GameObject>();
    }

    public GameObject Spawn()
    {
        return Spawn(new Vector3(0, 0, 0));
    }

    public GameObject Spawn(Vector3 pos)
    {
        return Spawn(pos, Quaternion.identity);
    }

    public GameObject Spawn(Vector3 pos, Quaternion rot)
    {
        GameObject com = null;
        if (DespawnedElements.Count != 0)
        {
            com = DespawnedElements.Dequeue();
        }
        else
        {
            com = Instantiate();
        }
        com.transform.position = pos;
        com.transform.transform.rotation = rot;
        ActiveObject.Add(com);
        com.gameObject.SetActive(true);
        return com;
    }

    public bool Despawn(GameObject pObject, bool DestroyObject = false)
    {
        if (ActiveObject.Remove(pObject))
        {
            if (DestroyObject)
            {
                Destroy(pObject);
            }
            else
            {
                pObject.SetActive(false);
                DespawnedElements.Enqueue(pObject);
            }
            return true;
        }
        return false;
    }

    private GameObject Instantiate()
    {
        GameObject go = (GameObject)GameObject.Instantiate(Prefab);
        go.transform.parent = Parent;
        return go;
    }

    private void Destroy(GameObject pObject)
    {
        GameObject.Destroy(pObject);
    }

    public void Allocate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject com = (GameObject)GameObject.Instantiate(Prefab);
            com.gameObject.SetActive(false);
            com.transform.parent = Parent;
            DespawnedElements.Enqueue(com);
        }
    }

    public void DeAllocate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (DespawnedElements.Count == 0)
                break;
            
            Destroy(DespawnedElements.Dequeue());
        }
    }
}
