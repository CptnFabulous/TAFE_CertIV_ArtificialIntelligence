using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public struct Tower
{
    public GameObject tower;
    public GameObject hologram;
}
*/

public class Selector : MonoBehaviour
{
    //public Tower towers;
    
    public GameObject[] towers;
    public GameObject[] holograms;

    int currentIndex;

    public float distance = 1000f;
    public LayerMask clickable;
    public QueryTriggerInteraction blah;
    
    void DisableAllHolograms()
    {
        foreach (GameObject hg in holograms)
        {
            hg.SetActive(false);
        }
    }

    void DrawRay(Ray ray)
    {
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000f);
    }

    private void OnDrawGizmos()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray playerRay = new Ray(transform.position, transform.forward);
        //float angle = Vector3.Angle(mouseRay.direction, playerRay.direction);
        //print(angle);
        Gizmos.color = Color.white;
        DrawRay(mouseRay);
        Gizmos.color = Color.red;
        DrawRay(playerRay);
    }

    // Update is called once per frame
    void Update()
    {
        DisableAllHolograms();

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit click;
        //QueryTriggerInteraction ignoreTowers = QueryTriggerInteraction.Ignore;
        if (Physics.Raycast(mouseRay, out click, distance, clickable, blah))
        {
            Placeable p = click.transform.GetComponent<Placeable>();
            if (p)
            {

                if (p.isOccupied == false)
                {
                    Vector3 placeablePoint = p.transform.position;
                    GameObject Hologram = holograms[currentIndex];

                    Hologram.SetActive(true);
                    Hologram.transform.position = placeablePoint;

                    if (Input.GetMouseButtonDown(0))
                    {
                        p.isOccupied = true;
                        Hologram.SetActive(false);
                        Instantiate(towers[currentIndex], placeablePoint, Quaternion.identity);
                    }
                }

                

                
            }
        }
    }

    /// <summary>
    /// Changes currentIndex to selected index 
    /// with filters
    /// </summary>
    /// <param name="index">The index we want to change to</param>
    public void SelectTower(int index)
    {
        
        if (index >= 0 && index < towers.Length)
        {
            currentIndex = index;
        }
        
    }
}
