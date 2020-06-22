using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IStrategy
{
    void setLevel(float speed);
    void setListNote(List<int> noteList);
    void setMusic(AudioClip clip);
}
