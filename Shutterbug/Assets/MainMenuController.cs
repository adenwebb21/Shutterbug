using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator book, frontCover, firstPage, bookEdge;
    public GameObject trackButton;
    private bool m_isBookOpen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !m_isBookOpen)
        {
            OpenBook();
            m_isBookOpen = true;
        }

        if(m_isBookOpen)
        {
            trackButton.SetActive(true);
        }
        else
        {
            trackButton.SetActive(false);
        }
    }

    private void OpenBook()
    {
        book.Play("move_book_offcentre");
        frontCover.Play("front_dissapear");
        bookEdge.Play("cover_edge_open");
    }
}
