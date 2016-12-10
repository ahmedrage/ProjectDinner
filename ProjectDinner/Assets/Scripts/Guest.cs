using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void setText () {
		Transform Panel = transform.GetChild (0).transform.GetChild (0);
		Panel.transform.GetChild (0).GetComponent<Text> ().text = guestClass.name;
		Panel.transform.GetChild (1).GetComponent<Text> ().text = guestClass.profession.ToString();
		Panel.transform.GetChild (2).GetComponent<Text> ().text = guestClass.hobby.ToString();
		Panel.transform.GetChild (3).GetComponent<Text> ().text = guestClass.Accessory.ToString();
		Panel.transform.GetChild (4).GetComponent<Text> ().text = guestClass.Blemish.ToString();


	}
}
