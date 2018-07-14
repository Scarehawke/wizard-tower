using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingIn : MonoBehaviour {

    [SerializeField]
    public static List<CraftItem> items = new List<CraftItem>();
    public static List<float> container = new List<float>();

    private void Start()
    {
        var allSomeEnumValues = (CraftingMaterial[])CraftingMaterial.GetValues(typeof(CraftingMaterial));

        // Prefill our list so we can add values to it
        foreach (var item in allSomeEnumValues)
        {
            container.Add(0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        MaterialScript[] materials = collision.gameObject.GetComponents<MaterialScript>();

        if(materials != null)
        {
            foreach (var material in materials)
            {
                items.Add(new CraftItem() { material = material.material, weight = material.weight });
            }
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            foreach (CraftItem item in items)
            {
                container[(int)item.material] += item.weight;
            }

            int i = 0;
            foreach (var item in container)
            {
                Debug.Log((CraftingMaterial)i + " has " + item + " weight");
                i++;
            }
        }
    }
}
