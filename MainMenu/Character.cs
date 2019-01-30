using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainMenu
{
    [Serializable]
    public class Character
    {
        protected string _cName, _cClass;
        protected int _cStrength, _cInteligence, _cHitPoints, _cManaPoints, _cLuck, _cExperience, _cLevel, _cMoney;
        protected int _cCoordinateX, _cCoordinateY;

        public Character()
        {

        }
        public Character(string cName, string cClass, int cStrength, int cInteligence, int cLuck, int cMoney, int cExp, int cLevel)
        {
            _cName = cName;
            _cClass = cClass;
            _cStrength = cStrength;
            _cInteligence = cInteligence;
            _cHitPoints = _cStrength * 10;
            _cManaPoints = _cInteligence * 5;
            _cLuck = cLuck;
            _cExperience = cExp;
            _cLevel = cLevel;
            _cMoney = cMoney;
        }
        public static Character LoadCharacter()
        {
            Character cPlayer = new Character();
            XmlSerializer serializer = new XmlSerializer(typeof(Character));
            using (var fs = new FileStream("savePlayer.xml", FileMode.Open, FileAccess.Read))
            {
                cPlayer = (Character)serializer.Deserialize(fs);
                fs.Flush();
                fs.Close();
            }
            return cPlayer;
        }
        public static void SaveCharacter(Character cPlayer)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream("savePlayer.xml", FileMode.Create))
            {
                formatter.Serialize(fs, cPlayer);
            }
        }
        public string Name
        {
            get
            {
                return _cName;
            }
            set
            {
                _cName = value;
            }
        }
        public string CharacterClass
        {
            get
            {
                return _cClass;
            }
            set
            {
                _cClass = value;
            }
        }
        public int Strength
        {
            get
            {
                return _cStrength;
            }
            set
            {
                _cStrength = value;
            }
        }
        public int Inteligence
        {
            get
            {
                return _cInteligence;
            }
            set
            {
                _cInteligence = value;
            }
        }
        public int HitPoints
        {
            get
            {
                return _cHitPoints;
            }
            set
            {
                if (value <= 0) value = 0;
                if (value >= _cStrength * 10) value = _cStrength * 10;
                _cHitPoints = value;
            }
        }
        public int ManaPoints
        {
            get
            {
                return _cManaPoints;
            }
            set
            {
                if (value <= 0) value = 0;
                if (value >= _cInteligence * 5) value = _cInteligence * 5;
                _cManaPoints = value;
            }
        }
        public int Luck
        {
            get
            {
                return _cLuck;
            }
            set
            {
                _cLuck = value;
            }
        }
        public int Experience
        {
            get
            {
                return _cExperience;
            }
            set
            {
                _cExperience = value;
            }
        }
        public int Level
        {
            get
            {
                if (Experience >= ExpToNextLevel())
                {
                    _cLevel++;
                    statPointsCount += 2 * _cLevel;
                }
                return _cLevel;
            }
            set
            {
                _cLevel = value;
            }
        }
        public double ExpToNextLevel()
        {
            double buffer = Math.Pow(10, (_cLevel / 3));
            switch (_cLevel % 3)
            {
                case 1:
                    {
                        buffer *= 1000;
                        break;
                    }
                case 2:
                    {
                        buffer *= 2500;
                        break;
                    }
                case 0:
                    {
                        buffer *= 500;
                        break;
                    }
                default:
                    break;
            }
            return buffer;
        }       
        public int Money
        {
            get
            {
                return _cMoney;
            }
            set
            {
                _cMoney = value;
            }
        }
        public int CoordinateX
        {
            get
            {
                return _cCoordinateX;
            }
            set
            {
                _cCoordinateX = value;
            }
        }
        public int CoordinateY
        {
            get
            {
                return _cCoordinateY;
            }
            set
            {
                _cCoordinateY = value;
            }
        }
        public int statPointsCount { get; set; }


    }
}
