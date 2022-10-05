using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject pathIndicatorPrefab;
    public GameObject arrowPrefab;
    private List<GameObject> pathIndicators = new List<GameObject>();
    public float gravitationalConstant;
    public float baseShootStrength;
    public int indicatorAmount;
    public float timeStepBetweenIndicators;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject indicator in pathIndicators)
        {
            Destroy(indicator);
        }

        DrawNewIndicators();
        if (Input.GetMouseButtonDown(0))
        {
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        arrowPrefab.GetComponent<Arrow>().transform.position = this.transform.position;
        Vector3 speed = GetShootdirection() * baseShootStrength;
        arrowPrefab.GetComponent<Arrow>().speed = speed;
        arrowPrefab.GetComponent<Arrow>().gravitationalConstant = gravitationalConstant;
        GameObject arrow = Instantiate(arrowPrefab);
    }

    /// <summary>
    /// Draws new indicators to show where the arrow will shoot
    /// </summary>
    void DrawNewIndicators()
    {
        Vector3 shootdirection = GetShootdirection();
        Vector3 CurrentArrowSpeed = shootdirection * baseShootStrength;
        Vector3 CurrentArrowPosition = this.transform.position;
        for (int i = 0; i < indicatorAmount; i++)
        {
            CurrentArrowSpeed = new Vector3(CurrentArrowSpeed.x, CurrentArrowSpeed.y - gravitationalConstant * timeStepBetweenIndicators, CurrentArrowSpeed.z); //speed is affected by gravity
            CurrentArrowPosition = CurrentArrowPosition + CurrentArrowSpeed * timeStepBetweenIndicators;
            GameObject newindicator = GameObject.Instantiate(pathIndicatorPrefab);
            newindicator.transform.position = CurrentArrowPosition;
            float indicatorScaleDown = newindicator.transform.localScale.x * (indicatorAmount - i) / indicatorAmount;
            Vector3 newScale = new Vector3(indicatorScaleDown, indicatorScaleDown, indicatorScaleDown);
            newindicator.transform.localScale = newScale;
            pathIndicators.Add(newindicator);
        }
    }

    /// <summary>
    /// Returns a unit vector in the shoot direction the player is aiming
    /// </summary>
    /// <returns></returns>
    Vector3 GetShootdirection()
    {
        Vector3 relativeMousePos = GetRelativeMousePossition();
        float xDirection = (relativeMousePos.x - 0.5f)*3;
        float yDirection = (relativeMousePos.y - 0.5f)*1.5f;
        Vector3 Direction = new Vector3(xDirection, yDirection, 1);
        Direction = Direction.normalized;
        return Direction;
    }

    /// <summary>
    /// Returns relative mouse position where 0,0 is bottom left corner and 1,1 top right
    /// </summary>
    /// <returns>vector2</returns>
    Vector2 GetRelativeMousePossition()
    {
        Vector2 RelativeMousePossition = new Vector2(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height);
        return RelativeMousePossition;
    }
}
