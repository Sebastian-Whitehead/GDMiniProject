using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy_paste : MonoBehaviour {
    private Camera camera;
    private GameObject clipboard;
    private GameObject ghost;
    
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        DetectObjectWithRaycast();
    }

    public void DetectObjectWithRaycast() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;

        copy(hit); // Copy object
        paste(hit); // Paste object and show ghost
    }

    public void paste(RaycastHit hit) {
        if (!(clipboard || ghost)) return;

        // Show ghost
        Vector3 pasteLocation = hit.point;
        pasteLocation.y += clipboard.GetComponent<Collider>().bounds.size.y / 2;
        ghost.transform.position = pasteLocation;

        // Paste object
        if (!Input.GetMouseButtonDown(0)) return;
        Instantiate(clipboard, pasteLocation, Quaternion.identity);
    }

    public void copy(RaycastHit hit) {
        if (!Input.GetMouseButtonDown(1)) return;
        if (hit.collider.gameObject.tag != "Copyable") return;

        // Copy object to clipboard
        clipboard = hit.collider.gameObject;

        // Instantiate ghost
        Destroy(ghost);
        ghost = Instantiate(clipboard, hit.point, Quaternion.identity);
        Rigidbody rb = ghost.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = false;
        Color color = ghost.GetComponent<MeshRenderer>().material.color;
        color.a = 0.5f;
        ghost.GetComponent<MeshRenderer>().material.color = color;
    }
}