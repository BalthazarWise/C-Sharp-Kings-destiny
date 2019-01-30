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
    public class Creatures : Character
    {
        const int mapCoordsX = 38;
        const int mapCoordsY = 18;
        Character chrtr = Character.LoadCharacter();
        public string _imageName;
        public string _imageNameWithoutBackground;

        public Creatures(bool[,] mapTielsReserved)
        {
            Random rnd = new Random();
            _cName = "Creature";
            _cClass = "Creature";
            int creatureLevel = chrtr.Level + rnd.Next(5) - 2;
            if (creatureLevel < 1) creatureLevel = 1;
            _cLevel = creatureLevel;
            _cStrength = 1;
            _cInteligence = 1;
            _cHitPoints = 1;
            _cManaPoints = 1;
            _cLuck = 1;
            for (int i = 0; i < _cLevel * 6; i++)
            {
                switch (rnd.Next(3))
                {
                    case 0:
                        {
                            _cStrength++;
                            break;
                        }
                    case 1:
                        {
                            _cInteligence++;
                            break;
                        }
                    case 3:
                        {
                            _cLuck++;
                            break;
                        }
                }
                _cHitPoints = _cStrength * 5;
                _cManaPoints = _cInteligence * 2;
                _cMoney = (rnd.Next(50) + 25) * _cLevel;
            }
            // Set coords
            int x, y;
            do
            {
                x = rnd.Next(mapCoordsX - 2) + 1;
                y = rnd.Next(mapCoordsY - 2) + 1;
            } while (mapTielsReserved[x, y]);
            _cCoordinateX = x;
            _cCoordinateY = y;

            // Set image
            int imgNumber = rnd.Next(8);
            switch (imgNumber)
            {
                case 0:
                    {
                        _imageName = @"Units\BarbarianOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Варвар.png";
                        break;
                    }
                case 1:
                    {
                        _imageName = @"Units\DemonOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Бес.png";
                        break;
                    }
                case 2:
                    {
                        _imageName = @"Units\GoblinOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Гоблин.png";
                        break;
                    }
                case 3:
                    {
                        _imageName = @"Units\HameleonOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Людоящер.png";
                        break;
                    }
                case 4:
                    {
                        _imageName = @"Units\OrcOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Орк.png";
                        break;
                    }
                case 5:
                    {
                        _imageName = @"Units\ShamanOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Шаман.png";
                        break;
                    }
                case 6:
                    {
                        _imageName = @"Units\SkeletonOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Скелет.png";
                        break;
                    }
                case 7:
                    {
                        _imageName = @"Units\ZombiOnTheGround.png";
                        _imageNameWithoutBackground = @"Units\Зомби.png";
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
