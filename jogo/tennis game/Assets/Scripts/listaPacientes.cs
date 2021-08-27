using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listaPacientes : MonoBehaviour
{
    void Start()
    {
        dadosJogo.Leitura();
        var dropdown = transform.GetComponent<Dropdown>();

        // dropdown.options.Clear();

        // List<string> items = new List<string>();
        // items.Add("Selecione o seu nome");
        // items.Add("Item 1");
        // items.Add("Item 2");
        // items.Add("Item 3");
        // items.Add("Item 4");
        // items.Add("Item 5");
        // items.Add("Item 6");
        // items.Add("Item 7");
        // items.Add("Item 8");
        // items.Add("Item 9");
        // items.Add("Item 10");
        // items.Add("Item 11");

        foreach(var item in dadosJogo.pacientes){
            dropdown.options.Add(new Dropdown.OptionData(){text = item});
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});

    }

    void DropdownItemSelected(Dropdown dropdown){
        int index = dropdown.value;
        Debug.Log(dropdown.options[index].text);
    //     TextBox.text = dropdown.options[index].text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
