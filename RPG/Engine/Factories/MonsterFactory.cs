using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";

        public static Monster GetMonster(int monsterID)
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                switch (monsterID)
                {
                    case 1:
                        Monster snake = LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), 1);
                        AddLootItem(snake, 9001, 25);
                        AddLootItem(snake, 9002, 75);

                        return snake;

                    case 2:
                        Monster rat = LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), 2);

                        AddLootItem(rat, 9003, 25);
                        AddLootItem(rat, 9004, 75);

                        return rat;

                    case 3:
                        Monster giantSpider = LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), 3);

                        AddLootItem(giantSpider, 9005, 25);
                        AddLootItem(giantSpider, 9006, 75);

                        return giantSpider;

                    default:
                        throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
                }
            }
            throw new ArgumentException(string.Format("Your Monster XmL File was not Found"));
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.AddItemToInventory(ItemFactory.CreateGameItem(itemID));
            }
        }

        private static Monster LoadMonstersFromNodes(XmlNodeList nodes, int monsterID)
        {
            Monster monster = null;

            foreach (XmlNode node in nodes)
            {
                if (monsterID == node.AttributeAsInt("ID"))
                {
                    monster = new Monster(
                                node.AttributeAsInt("ID"),
                                node.AttributeAsString("Name"),
                                node.AttributeAsString("Pic"),
                                node.AttributeAsInt("MaxHitPoints"),
                                node.AttributeAsInt("CurrentHitPoints"),
                                node.AttributeAsInt("Exp"),
                                node.AttributeAsInt("Gold"))
                        {
                            CurrentWeapon = ItemFactory.CreateGameItem(node.AttributeAsInt("Weapon"))
                        };
                }
                
            }
            if(monster != null )
                return monster;
            throw new ArgumentException($"Monster with ID : '{monsterID}' is not in the Monster Xml");
        }
    }
}