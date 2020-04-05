using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Animator book, frontCover, firstPage;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OpenBook();
        }
    }

    private void OpenBook()
    {
        book.Play("move_book_offcentre");
        frontCover.Play("front_dissapear");
    }
}
