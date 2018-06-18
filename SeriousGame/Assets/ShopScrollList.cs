﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item {
    public string itemName;
    public Sprite icon;
    public float price = 1;
}

public class ShopScrollList : MonoBehaviour {

    public List<Item> itemList;
    public Transform contentPanel;
    public Text myPointsDisplay;
    public SimpleObjectPool buttonObjectPool;

    public float points = 20f;

    // Use this for initialization
    void Start() {
        RefreshDisplay();
    }

    void RefreshDisplay() {
        myPointsDisplay.text = "Punten: " + points.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons() {
        while (contentPanel.childCount > 0) {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons() {
        for (int i = 0; i < itemList.Count; i++) {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    public void BuyItem(Item item) {
        if (points >= item.price) {
            points -= item.price;

            // Increase Stats

            RefreshDisplay();
        }
    }

    void AddItem(Item itemToAdd, ShopScrollList shopList) {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList) {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--) {
            if (shopList.itemList[i] == itemToRemove) {
                shopList.itemList.RemoveAt(i);
            }
        }
    }
}