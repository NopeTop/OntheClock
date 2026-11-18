using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDragNDrop : MonoBehaviour
{
    // Start is called before the first frame update
    private int size;
    public CircleCollider2D hitBox;
    public Camera cam;
    private bool OverNode;
    private Transform NodePosition;
    public Transform GearStartPos;
    public bool onGear;
    [SerializeField] private GearChecker GearNode;
    public GameObject gameBox;
    [SerializeField] private GearBoxButton GearBoxButton;
    private bool isHolding = false;
    private bool canCreate = true;
    private void Awake()
    {
        GearBoxButton.Gears.Add(this);
    }
    // Update is called once per frame
    public void OnMouseDown()
    {
        isHolding = true;
    }
    
    public void Update()
    {
        if (isHolding && OverNode == false && canCreate)
        {
            Instantiate(this.gameObject, this.GearStartPos.position, this.transform.rotation, gameBox.transform);
            canCreate = false;
        }
        if (isHolding)
        {
            this.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +11));

        }
        if (Input.GetMouseButtonUp(0) && isHolding)
        {
            isHolding = false;
            canCreate = true;
            if (OverNode && GearNode.HasGear == false)
            {
                this.transform.position = new Vector3(NodePosition.position.x, NodePosition.position.y, -1);
                onGear = true;
                GearNode.HasGear = true;
                GearNode.isRight(this.tag);
            }
            else
            {
                this.transform.position = GearStartPos.position;
                onGear = false;
                Destroy(this.gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gearNode")
        {
            NodePosition = collision.gameObject.transform;
            OverNode = true;
            GearNode = collision.gameObject.GetComponent<GearChecker>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gearNode")
        {
            OverNode = false;
            if (this.onGear)
            {
                GearNode.HasGear = false;
            }
        }
    }

    public bool wipeGears()
    {
        if (onGear)
        {
            Destroy(this.gameObject);
            return true;
        }
        return false;

    }
}
