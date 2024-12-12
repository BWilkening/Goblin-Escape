using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    public float backgroundWidth = 30f; // The width of each background
    public float speedMod = 1.0f;
    public float transitionOffset = 5f; // Additional offset for the transition point

    public Transform[] backgrounds; // Assign the backgrounds in the Inspector

    public GameManager gm;

    // public void Start() {
    //     gm = GetComponent<GameManager>().isPlaying;
    // }
    public void Update() {
        // Access the universal scroll speed from SceneController
        float scrollSpeed = SceneController.instance.universalSpeed;

        if (gm.isPlaying ==true) {
            // Move all backgrounds together
            for (int i = 0; i < backgrounds.Length; i++) {
                backgrounds[i].position += (Vector3.left * scrollSpeed * Time.deltaTime) / speedMod;

                // Check if the background has moved off-screen
                if ((backgrounds[i].position.x) <= -backgroundWidth - transitionOffset) {
                // Reposition the background to the rightmost position
                backgrounds[i].position += new Vector3(backgroundWidth * backgrounds.Length, 0f, 0f);
                }
            }
        }
        
    }
}
