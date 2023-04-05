using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Copy_paste : MonoBehaviour {

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
    
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
        UpdateUI();
    }

    // Update is called once per frame
    void Update() {
        if (pastes <= 0) {
            if (hideClipboard) return;
            hideClipboard = true;
        }
        DetectObjectWithRaycast();
        emptyClipboard();
    }


    public void DetectObjectWithRaycast() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 1000, ~IgnoreMe)) return;
        copy(); // Copy object
        paste(); // Paste object and show ghost
    }

    public void copy() {
        if (!Input.GetMouseButtonDown(1)) return;
        if (hit.collider.gameObject.tag != "Copyable") return;

        clipboard = hit.collider.gameObject; // Copy object to clipboard
        hideClipboard = false;
        makeGhost(); // Instantiate ghost
    }

    public void makeGhost() {
        Destroy(ghost);

        ghost = Instantiate(clipboard, hit.point, Quaternion.identity);
        ghost.name = clipboard.name + " (Ghost)";
        ghost.layer = LayerMask.NameToLayer("Ghost");

        ghost.GetComponent<Collider>().isTrigger = true;
        ghost.GetComponent<Rigidbody>().isKinematic = true;
        ghostColor = ghost.GetComponent<MeshRenderer>().material.color;
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
            ghostColor.a = 0.0f;
            ghost.GetComponent<MeshRenderer>().material.color = ghostColor;
            return;
        }

        // Render ghost
        ghostColor.a = 0.5f;
        ghost.GetComponent<MeshRenderer>().material.color = ghostColor;

        // Paste ghost
        if (!Input.GetMouseButtonDown(0)) return;
        UpdateUI();
        Instantiate(clipboard, pasteLocation, Quaternion.identity);
    }

    public void emptyClipboard() {
        if (!Input.GetMouseButtonDown(2)) return;
        hideClipboard = !hideClipboard;
    }

    private void UpdateUI() {
        string tmpPasteTxt = pasteTxt;
        if (pastes == 1) tmpPasteTxt += "s";
        pastesUI.text = string.Format("{0}: {1}", tmpPasteTxt, pastes);
    }

}