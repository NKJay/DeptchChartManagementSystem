namespace WebApplication2.Models
{
    public class Player
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int PositionDepth { get; set; }

        public Player(string number, string name)
        {
            Number = number;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Player other = (Player)obj;
            return Number == other.Number && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Name);
        }
    }
}
