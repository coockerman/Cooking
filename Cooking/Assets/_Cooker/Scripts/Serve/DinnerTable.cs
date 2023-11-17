using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerTable : MonoBehaviour
{
    public static DinnerTable Instance;

    public List<Table> Tables;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Table ChoseTable()
    {
        List<Table> emptyTables = new List<Table>();

        // Tìm tất cả các bàn trống
        foreach (Table table in Tables)
        {
            if (table.Empty)
            {
                emptyTables.Add(table);
            }
        }

        // Nếu có ít nhất một bàn trống, chọn một bàn ngẫu nhiên từ danh sách
        if (emptyTables.Count > 0)
        {
            int randomIndex = Random.Range(0, emptyTables.Count);
            Table randomEmptyTable = emptyTables[randomIndex];

            // Đánh dấu bàn đã được sử dụng
            randomEmptyTable.Empty = false;

            // Trả về bàn đã chọn
            return randomEmptyTable;
        }

        // Nếu không có bàn trống, trả về null
        return null;
    }

}
