using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputType
{
    touch, mouse
}

public class Click : MonoBehaviour {

    private Button button;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            // TODO
        });
	}
	
 
	// Update is called once per frame
	void Update () {
   
    }

    public void ButtonClick() { }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            print("Hit");
    }


}
