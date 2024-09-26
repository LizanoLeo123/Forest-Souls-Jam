using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript : MonoBehaviour
{
	public Sprite[] animatedImages;
	public Image animateImageObj ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        animateImageObj.sprite = animatedImages[(int)(Time.time*4)%animatedImages.Length];
    }
}
