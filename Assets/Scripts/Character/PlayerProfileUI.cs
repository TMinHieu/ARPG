using UnityEngine;

public class PlayerProfileUI : MonoBehaviour
{
    public GameObject profilePanel; // Kéo Panel từ Hierarchy vào đây trong Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (profilePanel != null)
            {
                bool isActive = profilePanel.activeSelf;
                profilePanel.SetActive(!isActive);
            }
        }
    }
}
