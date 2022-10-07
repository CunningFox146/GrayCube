﻿using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace GrayCube.Slots
{
    public class SlotsSystem : MonoBehaviour
    {
        [SerializeField] private float _slotSize = 50f;
        private List<Slot> _slots = new();

        public void RegisterSlot(Slot slot)
        {
            _slots.Add(slot);
        }

        public void UnregisterSlot(Slot slot)
        {
            _slots.Remove(slot);
        }

        public Slot GetSlotAtPoint(Vector2 pos)
        {
            foreach (Slot slot in _slots)
            {
                var distance = ((RectTransform)slot.transform).anchoredPosition - pos;

                if (Mathf.Abs(distance.x) <= _slotSize
                    && Mathf.Abs(distance.y) <= _slotSize)
                {
                    return slot;
                }
            }
            return null;
        }
    }
}
