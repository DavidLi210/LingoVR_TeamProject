using UnityEngine;
using System.Collections;

// Each learnable object has its own display manager which handles the display of the label, audio, and floating menu.
public class DisplayManager : MonoBehaviour {  

	private GameObject label;  
	private GameObject menu;
	public AudioSource src;
	public AudioSource [] menuSrc;
	private bool [] playedMenuAudio;
	private bool playedAudio = false;

	public void createLabel(string text, Vector3 position) {
		label = new GameObject ();
		label.transform.position = position;
		TextMesh textMesh = label.AddComponent<TextMesh> ();
		textMesh.anchor = TextAnchor.MiddleCenter;
		textMesh.text = text;
		textMesh.fontSize = 100;
		textMesh.transform.localScale = new Vector3 (0.015f, 0.015f);
		label.name = "DynamicLabel";
		label.SetActive (false);

		FaceMe f = label.AddComponent<FaceMe>();
		GameObject hmd = GameObject.Find ("Vive Camera");
		f.vive = hmd; 

		//		UNCOMMENT THIS FOR LABEL HEAD TRACKING TO WORK WITH ACTUAL VIVE HEADSET
		f.vive = GameObject.Find ("Vive Camera (eye)");
	}
	public void createMenu(Example[] examples, string language, string env, LearningData data) {		
		print ("Looking for menu with name Menu_" + gameObject.name);
		this.menu = GameObject.Find("Menu_" + gameObject.name);
		playedMenuAudio = new bool[examples.Length];
		menuSrc = new AudioSource[examples.Length];

		if (this.menu != null) {
			Debug.Log ("Populating menu for " + gameObject.name);
			for (int i = 0;i<examples.Length;i++) {
				//1st child of each menu is background, so the example1 is the 2nd. Then use getChild to find the 2nd child(example1) and so on for example2,example3
				Transform tmp = menu.transform.GetChild(i+1);
				examples[i].audio = language + "/" + env + "/audio/" + data.examples[i].audio;
				AudioSource tmpSrc = tmp.gameObject.AddComponent<AudioSource>();
				// save the  tmpsrc in the menusrc array
				menuSrc[i] = tmpSrc;
				AudioClip clip = Resources.Load(examples[i].audio) as AudioClip;
				if (clip) {
					Debug.Log ("Successfully loaded " + clip.name + " clip");
				} else {
					Debug.Log ("FAILED TO LOAD " + clip.name + "CLIP!");
				}
				menuSrc[i].clip = clip;
				menuSrc [i].playOnAwake = false;
			}
			TextMesh[] textMeshes = this.menu.GetComponentsInChildren<TextMesh> ();
			for (int i = 0; i < textMeshes.Length; i++) {
				switch (textMeshes [i].name) {
				case "Example1Text":
					textMeshes [i].text = examples [0].text;
					break;
				case "Translation1Text":
					textMeshes [i].text = examples [0].translation;
					break;
				case "Example2Text":
					textMeshes [i].text = examples [1].text;
					break;
				case "Translation2Text":
					textMeshes [i].text = examples [1].translation;
					break;
				case "Example3Text":
					textMeshes [i].text = examples [2].text;
					break;
				case "Translation3Text":
					textMeshes [i].text = examples [2].translation;
					break;
				}
			}
		}
	}

	public void displayLabel() {
		//Debug.Log("label not null");
		if (label!=null) {			
			label.SetActive(true);
			if (!playedAudio) {
				src.Play ();
				playedAudio = true;
			}
		}
	}

	public void destroyLabel(){
		Debug.Log("deactivate label");
		if (label!=null) {
			label.SetActive(false);
			playedAudio = false;
		}
	}


	public void displayMenu() {
		playedAudio = false;
		if (menu!=null) {
			menu.SetActive(true);
		}

	}

	public void destroyMenu() {
		if (menu!=null) {
			menu.SetActive(false);
		}
	}

	public GameObject getMenu(){
		return 	this.menu;
	}

	public void playExample(int i){
		Debug.Log ("Play example"+ (i+1));
		if (!playedAudio) {
			playedAudio = true;
			menuSrc [i].Play ();
			ResetAudioAfterSeconds (2);
		}
	}

	IEnumerator ResetAudioAfterSeconds(float time)
	{
		yield return new WaitForSeconds(time);
		playedAudio = false;
	}

}
