using System;
using System.Collections.Generic;
using UnityEngine;

namespace Joyixir.Utility.Utils.RandomNameAndFlagGenerator
{
    // Many thanks to https://github.com/Pimeko/AI-Name-Generator---Unity
    public static class RandomPersonGenerator
    {
        [Serializable]
        private class PersonList
        {
            public List<string> names = new List<string>();
            public List<Texture2D> flags = new List<Texture2D>();
        }

        static PersonList PersonsList;
        static PersonList CurrentPersonsList
        {
            get
            {
                if (PersonsList == null)
                {
                    var file = Resources.Load<TextAsset>("Texts/NamesList");
                    PersonsList = new PersonList();
                    PersonsList = JsonUtility.FromJson<PersonList>(file.text);
                    var flagsObjects = Resources.LoadAll("Flags/", typeof(Texture2D));
                    foreach (var flag in flagsObjects)
                        PersonsList.flags.Add(flag as Texture2D);
                }
                return PersonsList;
            }
        }

        public static string GetRandomName()
        {
            return CurrentPersonsList.names[UnityEngine.Random.Range(0, CurrentPersonsList.names.Count)];
        }

        public static Texture2D GetRandomFlag()
        {
            return CurrentPersonsList.flags[UnityEngine.Random.Range(0, CurrentPersonsList.flags.Count)];
        }

        public static List<string> GetRandomNames(int nbNames)
        {
            if (nbNames > CurrentPersonsList.names.Count)
                throw new Exception("Asking for more random names than there actually are!");
            
            PersonList copy = new PersonList();
            copy.names = new List<string>(CurrentPersonsList.names);

            List<string> result = new List<string>();

            for (int i = 0; i < nbNames; i++)
            {
                int rnd = UnityEngine.Random.Range(0, copy.names.Count);
                result.Add(copy.names[rnd]);
                copy.names.RemoveAt(rnd);
            }

            return result;
        }
    }

    public class Person
    {
        public string Name;
        public Texture2D CountryFlag;

        public static Person CreateRandomPerson()
        {
            Person result = new Person();
            result.Name = RandomPersonGenerator.GetRandomName();
            result.CountryFlag = RandomPersonGenerator.GetRandomFlag();
            return result;
        }

        public override string ToString()
        {
            return Name + " - " + CountryFlag.name;
        }
    }
}