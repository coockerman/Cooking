using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;
    protected virtual void Start()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updateSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updateSlot)
            {
                slot.Key.UpdateUISlot(updateSlot);
            }
        }
    }
    public void SlotClick(InventorySlot_UI clickUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        if (clickUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignInventorySlot.ItemData == null)
        {
            if (isShiftPressed && clickUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickUISlot.AssignedInventorySlot);
                clickUISlot.ClearSlot();
                return;
            }
        }

        if (clickUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignInventorySlot.ItemData != null)
        {
            clickUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignInventorySlot);
            clickUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        if (clickUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignInventorySlot.ItemData != null)
        {
            bool isSameItem = clickUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignInventorySlot.ItemData;

            if (isSameItem && clickUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignInventorySlot.StackSize))
            {
                clickUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignInventorySlot);
                clickUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem && !clickUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickUISlot);
                else
                {
                    int remainingOnMouse = mouseInventoryItem.AssignInventorySlot.StackSize - leftInStack;
                    clickUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }
            else if (!isSameItem)
            {
                SwapSlots(clickUISlot);
                return;
            }
        }
    }
    void SwapSlots(InventorySlot_UI clikedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignInventorySlot.ItemData, mouseInventoryItem.AssignInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clikedUISlot.AssignedInventorySlot);

        clikedUISlot.ClearSlot();
        clikedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clikedUISlot.UpdateUISlot();
    }
}
