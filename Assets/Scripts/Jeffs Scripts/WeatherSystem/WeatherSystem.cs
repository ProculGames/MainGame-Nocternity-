using UnityEngine;
using System.Collections;

public class WeatherSystem : MonoBehaviour {

	//Public Variables
	[Header("Enter in the percent chance of rain (no percent sign) here.")]
	public float ChanceOfRain;

	[Header("Seasons")]
	public bool Winter;
	public bool Spring;
	public bool Fall;
	public bool Summer;

	[Header("Whether it is currently Snowing.")]
	public bool Snow;

	[Header("Whether it is currently Raining.")]
	public bool Rain;

	[Header("Whether it is currently Sunny.")]
	public bool Sunny;

	[Header("Whether it is currently Cloudy.")]
	public bool Cloudy;

	[Header("Whether it is currently Sleeting.")]
	public bool Sleet;

	[Header("What the Temperature outside is.")]
	public float Temperature;

	[Header("The time of day it is going to rain")]
	public float[] RainTime;

	[Header("How many times it will rain")]
	public int RainCount;

	[Header("Maximum times it will rain in a day")]
	public int MaxRainCount;

	[Header("Minimum times it will rain in a day")]
	public int MinRainCount;

	[Header("Current time")]
	public float TimeOfDay;

	[Header("Clear skies skybox Material")]
	public Material ClearSkybox;

	[Header("Rainy skies skybox Material")]
	public Material RainySkybox;

	[Header("How many times it has rained so far in a day")]
	public int HowManyTimesItHasRained;

	[Header("Particle system that controls the raining")]
	public ParticleSystem RainSnowSystem;

	[Header("Material for snow")]
	public Material SnowMat;

	[Header("Material for rain")]
	public Material RainMat;

	[Header("Maximum time it will rain")]
	public float MaxRainTime;

	[Header("Minimum time it will rain")]
	public float MinRainTime;

	[Header ("It will rain for this long")]
	public float WillRainFor;

	[Header ("It has been raining for this long")]
	public float HasBeenRaining;
	//Private Variables
	float WeatherNum;


	// Use this for initialization
	void Start () {
		//Set up the rain for the day.
		SeeIfItWillRain ();

		//If it is raining that day
		if (Rain) {
			//Check the time to see if it should start raining every second.
			InvokeRepeating ("CheckIfItIsTimeToRain", 0, 1);
			//otherwise
		} else {
			//make the skybox clear
			RenderSettings.skybox = ClearSkybox;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Making the engine count by real time
		TimeOfDay += Time.deltaTime;

		if (Rain) {
			HasBeenRaining += Time.deltaTime;
		}
		if (RainSnowSystem.isPlaying) {
			if (HasBeenRaining >= WillRainFor) {
				StopRaining ();
			}
		}


	}

	void SeeIfItWillRain(){
		//Make the Weather Number, which reads whether or not it will rain be equal to a random number between 0 and 100.
		WeatherNum = Random.Range (0, 100);
		//If the weather number is greater than Chance of Rain,
		if (WeatherNum > ChanceOfRain) {
			//Make it sunny.
			Sunny = true;
			//otherwise,
		} else {
			//Make it rain.
			Rain = true;
			//If it will rain, calculate how many times it will rain
			RainCount = Random.Range (MinRainCount, MaxRainCount);
			RainTime = new float[RainCount];
			//For however many times it will rain in a day, loop a random integer for times it will rain
			for(int i = 0; i < RainCount; i++){
				RainTime[i] = Random.Range(0, 24);
			}
		}
	}

	void CheckIfItIsTimeToRain(){
		//If the amount it has rained is less than the amount of rain times in the RainTime array
		if (HowManyTimesItHasRained < RainTime.Length) {
			//Loop through the RainTimes
			for(int i = 0; i < RainTime.Length; i++){
				//If the Time of day equals a rain time
				if(Mathf.Round (TimeOfDay) == RainTime[i]){
					//Make it rain.
					RainNow();
				}
				//If i equals the length of RainTime,
				if(i == RainTime.Length){
					//Set i back to zero so it can loop through them again.
					i = 0;
				}
			}
		}
	}

	void RainNow(){
		WillRainFor = Random.Range (MinRainTime, MaxRainTime);
		//Add one to the times it has rained.
		HowManyTimesItHasRained++;
		ChangeSkyBox ();
		HasBeenRaining = 0;
		if (Winter) {
			Rain = true;
			RainSnowSystem.Play ();
			RainSnowSystem.GetComponent<Renderer> ().material = SnowMat;
		} else {
			Rain = true;
			Sunny = false;
			RainSnowSystem.Play ();
			RainSnowSystem.GetComponent<Renderer>().material = RainMat;
		}
	}

	void StopRaining(){
		Rain = false;
		HasBeenRaining = 0;
		Sunny = true;
		RainSnowSystem.Stop ();
		ChangeSkyBox ();
	}

	void ChangeSkyBox(){
		if (Rain || Snow) {
			RenderSettings.skybox = RainySkybox;
			Sunny = false;
		} else if (Sunny) {
			RenderSettings.skybox = ClearSkybox;
			Rain = false;
			Snow = false;
		}
	}
}