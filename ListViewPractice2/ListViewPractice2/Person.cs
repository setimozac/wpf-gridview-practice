public class Person {
    private string _name;
    public string Name {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    private int _age;
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
        }
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return string.Format($"{Name} is {Age} years old");
    }
}

