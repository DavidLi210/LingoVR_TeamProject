using UnityEngine;
using System.Collections;
using System;

public class LanguageManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Load ("cn", "bedroom");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load (string language, string env) {
		var path = language + "/" + env + "/data";
		try {
			TextAsset json = Resources.Load (path) as TextAsset;
			Language lang = JsonUtility.FromJson<Language>(json.text);
			foreach (LearningData data in lang.learningData) {
				Debug.Log("Looking for " + data.model + " model");
				GameObject obj = GameObject.Find(data.model);
				if (obj) {
					Debug.Log("Found " + data.model + " model");
					DisplayManager display = obj.AddComponent<DisplayManager>();
					// Pass the examples to DisplayManager where it can populate the object's menu
					display.createMenu(data.examples, language, env, data);
					display.getMenu().SetActive(false);
					var center = GameObject.Find("RoomCenter");
					switch (data.position) {
						case "center":
							display.createLabel(data.translation, obj.transform.position);
							break;
						case "above":
							display.createLabel(data.translation, obj.transform.position + new Vector3(0,transform.lossyScale.y));
							break;
						case "front":
							Vector3 towardsCenter = center.transform.position - obj.transform.position;
							display.createLabel(data.translation, obj.transform.position + new Vector3(towardsCenter.x * 0.2f, towardsCenter.y * 0.2f, 0));
							break;
					}
					string audioPath = language + "/" + env + "/audio/" + data.audio;
					display.src = obj.AddComponent<AudioSource>();
					AudioClip clip = Resources.Load(audioPath) as AudioClip;  
					display.src.clip = clip;
				} else {
					Debug.Log("Object " + data.model + " wasn't found.");
				}
			}
		} 
		catch (NullReferenceException e) {
			Debug.Log (e);
		}
	}
}
