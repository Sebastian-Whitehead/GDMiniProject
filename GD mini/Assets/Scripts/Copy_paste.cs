using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Copy_paste : MonoBehaviour {

    public bool active = true;
    public Material original; 
    public Material hovered;
    public string pasteTxt = "Paste";
    public int pastes;
    public TextMeshProUGUI pastesUI;
    public LayerMask IgnoreMe;
    private Camera camera;
    private GameObject clipboard;
    private bool hideClipboard = false;
    private GameObject ghost;
    private Color ghostColor;
    private RaycastHit hit;
    private GameObject Target = null;

    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
    }

    private void Awake() {
        UpdateUI();
    }

    // Update is called once per frame
    void Update() {
        if (!active) return;
        Disable();
        DetectObjectWithRaycast();
        emptyClipboard();
    }

    public void Disable() {
        if (pastes > 0) return;
        ghost.GetComponent<MeshRenderer>().enabled = false;
        hideClipboard = true;
        active = false;
        if (Target == null) return;
        Target.GetComponent<Renderer>().material = original;
        Target = null;
    }


    public void DetectObjectWithRaycast() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 1000, ~IgnoreMe) || hideClipboard) return;
        hover(); // Hover copyable objects
        copy(); // Copy object
        paste(); // Paste object and show ghost
    }

    private void hover() {
        if (hit.collider.gameObject.tag == "Copyable") {
            Target = hit.collider.gameObject;
            Target.GetComponent<Renderer>().material = hovered;
        } else {
            if (Target == null) return; 
            Target.GetComponent<Renderer>().material = original;
            Target = null;
        }
    }

    public void copy() {
        if (!Input.GetMouseButtonDown(1)) return;
        if (hit.collider.gameObject.tag != "Copyable") return;

        clipboard = hit.collider.gameObject; // Copy object to clipboard
        hideClipboard = false;
        
        Destroy(ghost);
        
        ghost = Instantiate(clipboard, hit.point, Quaternion.identity);
        ghost.name = "Ghost";
        ghost.layer = LayerMask.NameToLayer("Ghost");

        ghost.GetComponent<Collider>().isTrigger = true;
        ghost.GetComponent<Rigidbody>().isKinematic = true;
        ghostColor = ghost.GetComponent<MeshRenderer>().material.color;
        ghost.GetComponent<Renderer>().material = original;
    }

    public void paste() {
        if (!(clipboard || ghost)) return;

        // Place ghost
        Vector3 pasteLocation = hit.point;
        pasteLocation.y += clipboard.GetComponent<Collider>().bounds.size.y / 2;
        pasteLocation.y += 0.05f;
        ghost.transform.position = pasteLocation;
        
        // Disable ghost
        if (ghost.GetComponent<CheckCollision>().colliding || hideClipboard) {
            ghostColor.a = 0.1f;
            ghost.GetComponent<MeshRenderer>().material.color = ghostColor;
            return;
        }
        ghost.GetComponent<MeshRenderer>().enabled = true;

        // Render ghost
        ghostColor.a = 0.5f;
        ghost.GetComponent<MeshRenderer>().material.color = ghostColor;

        // Paste ghost
        if (!Input.GetMouseButtonDown(0)) return;
        pastes--;
        UpdateUI();
        GameObject pastedObject = Instantiate(clipboard, pasteLocation, Quaternion.identity);
        pastedObject.GetComponent<Renderer>().material = original;
    }

    public void emptyClipboard() {
        if (!Input.GetMouseButtonDown(2)) return;
        hideClipboard = false;
    }

    private void UpdateUI() {
        string tmpPasteTxt = pasteTxt;
        if (pastes == 1) tmpPasteTxt += "s";
        pastesUI.text = string.Format("{0}: {1}", tmpPasteTxt, pastes);
        if (pastes == 0) pastesUI.color = new Color(150, 0, 0, 255);
    }

}