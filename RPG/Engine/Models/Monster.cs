namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; }

        public int RewardExperiencePoints { get; }

        public int ID { get; }

        public Monster(int id, string name, string imageName,
                       int maximumHitPoints, int currentHitPoints,
                       int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";
            RewardExperiencePoints = rewardExperiencePoints;
            ID = id;
        }
    }
}