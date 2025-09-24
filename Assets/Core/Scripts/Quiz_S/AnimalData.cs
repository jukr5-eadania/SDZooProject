using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimal", menuName = "Animal/AnimalData")]
public class AnimalData : ScriptableObject
{

    //Fields
    public string animalName;
    public Sprite animalSprite;
    public AudioClip animalSound;
    [TextArea] public string funFact;

    public QuestionData[] questions;

}
