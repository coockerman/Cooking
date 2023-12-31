using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerTable : MonoBehaviour
{
    public static DinnerTable Instance;
    public List<Table> Tables;

    List<Table> listTablesEmpty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public bool CheckTable()
    {
        foreach (Table table in Tables)
        {
            if (table.Customer == null)
            {
                return true;
            }
        }
        return false;
    }
    public Table ChoseTable(Customer customer)
    {
        listTablesEmpty = new List<Table>();
        foreach (Table table in Tables)
        {
            if (table.Customer == null)
            {
                listTablesEmpty.Add(table);
            }
        }

        if (listTablesEmpty.Count > 0)
        {
            int randomIndex = Random.Range(0, listTablesEmpty.Count);
            Table randomEmptyTable = listTablesEmpty[randomIndex];

            randomEmptyTable.Customer = customer;

            return randomEmptyTable;
        }

        return null;
    }
    public Table OutTable(Customer customer)
    {
        foreach (Table table in Tables)
        {
            if (table.Customer == customer)
            {
                table.Customer = null;

                return table;
            }
        }

        return null;
    }
}
