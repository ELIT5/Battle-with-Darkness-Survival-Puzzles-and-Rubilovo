using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proxy : MonoBehaviour
{
    private Inventory inv;
    private DataBase data;

    public void Start()
    {
        inv = gameObject.GetComponentInChildren<Inventory>();
        data = gameObject.GetComponentInChildren<DataBase>();
    }

    public void PickUpItem(int id)
    {
        inv.SearchForSameItem(data.items[id], 1);
        inv.UpdateInventory();
    }
}
