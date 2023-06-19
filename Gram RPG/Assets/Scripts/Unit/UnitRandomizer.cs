using UnityEngine;

namespace Unit
{
    public class UnitRandomizer
    {
        private static UnitRandomizer instance;
        public static UnitRandomizer Instance
        {
            get
            {
                if(instance == null) 
                    instance = new UnitRandomizer();
                return instance;
            }
        }

        private string[] ValidNames = {"Carl", "Robert", "Ghorm", "Hemlock", "Julian","Chantho", "Cloud", "Mackingson", "Shubert","Talbot","Firesoul","Jhonson","Einheart","Guldrich","Lorem","Ipsum","Arepo","Rotas","Fugit","Greyck","Morn","Yorn","Bheng"};

        public UnitData GenerateRandomUnitData()
        {
            return GenerateUnitData();
        }

        private UnitData GenerateUnitData() 
            //TWIST -> Randomize priority between health and Attack power, assign "power points" so the range is bigger on the priority and lower on other.
            //This way Generated characters can be more diverse and balanced instead of just random.
        {
            var Generated = new UnitData();
            Generated.Name = ValidNames[UnityEngine.Random.Range(0, ValidNames.Length)];
            Generated.Health += Random.Range(0, 5);
            Generated.MaxHealth = Generated.Health;
            Generated.AttackPower += Random.Range(0, 3);
            Generated.UnitColor = new Color(Random.Range(0.2f,0.8f),Random.Range(0.2f,0.8f),Random.Range(0.2f,0.8f),1);
            return Generated;
        }
    }
}
