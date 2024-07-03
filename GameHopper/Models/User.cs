using GameHopper.Models;

namespace GameHopper.Models
{
public abstract class User
{
    public string Name { get; set; }
    public int Id { get; set;}

    public User(string name,int Id)
{
            Name = name;
    }

    public override string ToString() {
            return Name;
    }
    }
}

