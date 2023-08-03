using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class CSHighlighterController : MonoBehaviour
{
	protected Highlighter h;

	// 
	void Awake()
	{
		h = GetComponent<Highlighter>();
		if (h == null) { h = gameObject.AddComponent<Highlighter>(); }
	}

	// 
	void Start()
	{
		
	}

    public void StopFlashing()
    {
        h.FlashingOff();
    }

    public void StartFlshing()
    {
        h.FlashingOn(new Color(0f, 0f, 1f, 0f), new Color(0f, 0f,1f, 1f), 3f);
    }


}
