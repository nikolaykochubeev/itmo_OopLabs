namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string prefix, string name)
        {
            Prefix = prefix;
            Name = name;
        }

        public string Prefix { get; }
        public string Name { get; }
    }
}