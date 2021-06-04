using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides;
    int currentSlide = 0;
    // Start is called before the first frame update
    void Start()
    {
        slides[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Attack") > 0)
        {
            GetNextSlide();
        }
    }
    private void GetNextSlide()
    {
        slides[currentSlide].SetActive(false);
        currentSlide++;
        if (currentSlide == slides.Length) SceneManager.LoadScene(2);
        slides[currentSlide].SetActive(true);
    }
}
