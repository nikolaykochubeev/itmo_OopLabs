namespace IsuExtra.Entities
{
    public class Audience
    {
        public Audience(uint number, string universityBuilding)
        {
            Number = number;
            UniversityBuilding = universityBuilding;
        }

        public uint Number { get; }
        public string UniversityBuilding { get; }
    }
}