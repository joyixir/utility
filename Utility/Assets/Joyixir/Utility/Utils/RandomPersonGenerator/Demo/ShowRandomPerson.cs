using UnityEngine;
using TMPro;
using Joyixir.Utility.Utils.RandomNameAndFlagGenerator;

public class ShowRandomPerson : MonoBehaviour
{
    public TextMeshProUGUI personName;
    public UnityEngine.UI.Image personCountry;

    public void FillPersonWithANewRandomGeneratedOne()
    {
        var randomPerson = Person.CreateRandomPerson();
        personName.text = randomPerson.Name;
        // convert texture 2d to unity image
        personCountry.sprite = Sprite.Create(randomPerson.CountryFlag, new Rect(0, 0, randomPerson.CountryFlag.width, randomPerson.CountryFlag.height), Vector2.zero);
    }
}
